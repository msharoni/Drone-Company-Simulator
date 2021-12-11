using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL
    {
        List<DroneForList> Drones = new List<DroneForList>();
        List<ChargingDrone> ChargingDrones = new List<ChargingDrone>();
        IDAL.IDAL dalObject = new DalObject.DalObject();
        Random r = new Random();

        public BL()
        {
            foreach (IDAL.DO.Drone drone in dalObject.GetDrones())
            {
                DroneForList addDrone = new DroneForList();
                addDrone.Id = drone.Id;
                addDrone.Model = drone.Model;
                addDrone.MaxWeight = (WeightCategories)drone.MaxWeight;
                if (dalObject.GetParcels().Any(parcel => parcel.DroneId == drone.Id) == true)
                {
                    IDAL.DO.Parcel parcel = dalObject.GetParcels().Where(parcel => parcel.DroneId == drone.Id).ToList()[0];
                    addDrone.ParcelId = parcel.Id;
                    addDrone.Status = DroneStatuses.Delivery;

                    Location senderLocation = SenderLocation(parcel.Id);

                    if (parcel.Scheduled != new DateTime() && parcel.PickedUp == new DateTime())
                    {
                        //finding closest stations location
                        addDrone.CurrentLocation = ClosestStation(senderLocation);
                        double TotalDistance = Distance(ClosestStation(senderLocation), SenderLocation(parcel.Id)) + Distance(senderLocation, ReciverLocation(parcel.Id)) + Distance(ReciverLocation(parcel.Id), ClosestStation(ReciverLocation(parcel.Id)));
                        addDrone.Battery = r.Next((int)((TotalDistance) * dalObject.GetBatteryUsage()[(int)parcel.Weight])+1,100);
                    }
                    else if (parcel.PickedUp != new DateTime() && parcel.Delivered == new DateTime())
                    {
                        double TotalDistance = Distance(senderLocation, ReciverLocation(parcel.Id)) + Distance(ReciverLocation(parcel.Id), ClosestStation(ReciverLocation(parcel.Id)));
                        addDrone.Battery = r.Next((int)(TotalDistance * dalObject.GetBatteryUsage()[(int)parcel.Weight]) + 1, 100);
                        addDrone.CurrentLocation = senderLocation;
                    }
                }
                else
                {
                    addDrone.ParcelId = -1;
                    addDrone.Status = (DroneStatuses)r.Next(1, 3);
                    if (addDrone.Status == DroneStatuses.maintenance)
                    {
                        int index = r.Next(0, dalObject.GetStations().Count());
                        addDrone.CurrentLocation = new Location { Lattitude = dalObject.GetStations().ToList()[index].Lattitude, Longitude = dalObject.GetStations().ToList()[index].Longitude };
                        addDrone.Battery = r.Next(0, 21);
                    }
                    else
                    {
                        List<CustomerForList> CustomerList = new List<CustomerForList>();
                        foreach(CustomerForList customer in DisplayCustomers())
                        {
                            if (customer.NumRecived > 0)
                                CustomerList.Add(customer);
                        }
                        int Id = CustomerList[r.Next(0, CustomerList.Count())].Id;
                        addDrone.CurrentLocation = DisplayCustomer(Id).Location;
                        addDrone.Battery = r.Next((int)(Distance(addDrone.CurrentLocation, ClosestStation(addDrone.CurrentLocation)) * dalObject.GetBatteryUsage()[1]) +1,100);
                    }
                }
                Drones.Add(addDrone);
            }

        }

        public  void AddDrone(int _Id, string _Model, int _MaxWeight, int _StationId)
        {
            try
            {
                dalObject.AddDrone(_Id, _Model, (IDAL.DO.WeightCategories)_MaxWeight);
            }
            catch (DalObject.IdExcistsException)
            {
                throw new IdExcistsException(_Id);
            }
            Location StationLocation =  new Location { Longitude = dalObject.GetStation(_StationId).Longitude , Lattitude = dalObject.GetStation(_StationId).Lattitude };
            Drones.Add(new DroneForList { Id = _Id, Model = _Model, MaxWeight = (WeightCategories)_MaxWeight, Battery = r.Next(20, 41), Status = DroneStatuses.maintenance, CurrentLocation = StationLocation, ParcelId = -1});
        }
        public void UpdateDrone(int _Id, string _Model)
        {
            Drones[Drones.FindIndex(drone => drone.Id == _Id)].Model = _Model;
            dalObject.UpdateDrone(_Id,_Model);
        }
        Location SenderLocation(int ParcelId)
        {
            IDAL.DO.Parcel p = dalObject.GetParcel(ParcelId);
            double _Longitude = dalObject.GetCustomers().FirstOrDefault(customer => customer.Id == p.SenderId).Longitude;
            double _Lattitude = dalObject.GetCustomers().FirstOrDefault(customer => customer.Id == p.SenderId).Lattitude;
            return new Location { Longitude = _Longitude, Lattitude = _Lattitude };
        }
        Location ReciverLocation(int ParcelId)
        {
            IDAL.DO.Parcel p = dalObject.GetParcel(ParcelId);
            double _Longitude = dalObject.GetCustomers().FirstOrDefault(customer => customer.Id == p.TargetId).Longitude;
            double _Lattitude = dalObject.GetCustomers().FirstOrDefault(customer => customer.Id == p.TargetId).Lattitude;
            return new Location { Longitude = _Longitude, Lattitude = _Lattitude };
        }
        Location ClosestStation(Location location)
        {
            Location ClosestStationLocation = new Location();
            double smallest = 9999999999;
            foreach (IDAL.DO.Station station in dalObject.GetStations())
            {
                Location StationLocation = new Location { Longitude = station.Longitude, Lattitude = station.Lattitude };

                if (Distance(ClosestStationLocation, location) <= smallest)
                {
                    smallest = Distance(StationLocation, location);
                    ClosestStationLocation = StationLocation;

                }
            }
            return ClosestStationLocation;
        }
        double Distance(Location Location1,Location Location2)
        {
            double LongitudeRadian1 = Location1.Longitude * (Math.PI / 180);
            double LattitudeRadian1 = Location1.Lattitude * (Math.PI / 180);
            double LongitudeRadian2 = Location2.Longitude * (Math.PI / 180);
            double LattitudeRadian2 = Location2.Lattitude * (Math.PI / 180);

            //Haversine formula
            double dist_long = LongitudeRadian2 - LongitudeRadian1;
            double dist_lat = LattitudeRadian2 - LattitudeRadian1;
            double a = Math.Pow(Math.Sin(dist_lat / 2), 2) + Math.Cos(LattitudeRadian1) * Math.Cos(LattitudeRadian2) * Math.Pow(Math.Sin(dist_long / 2), 2);
            double c = 2 * Math.Asin(Math.Sqrt(a));
            //Radius of the earth is 6371 KM
            double r = 6371;
            return c * r;
        }
        public void ChargeDrone(int _Id)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == _Id);
            int StationId = 0;
            double smallest = 999999999;
            Location StationLocation = new Location();
            if (Drones[DroneIndex].Status != DroneStatuses.Available)
                throw new UnAvailabe(_Id);
            foreach(IDAL.DO.Station station in dalObject.GetStations())
            {
                StationLocation = new Location { Longitude = station.Longitude, Lattitude = station.Lattitude };
                if (Distance(StationLocation,Drones[DroneIndex].CurrentLocation) <= smallest) 
                {
                    smallest = Distance(StationLocation, Drones[DroneIndex].CurrentLocation);
                    StationId = station.Id;
                }
            }
            if (Drones[DroneIndex].Battery < (smallest * dalObject.GetBatteryUsage()[1]))
                throw new NotEnoughBattery(_Id);
            Drones[DroneIndex].Battery -= (smallest * dalObject.GetBatteryUsage()[1]);
            Drones[DroneIndex].CurrentLocation = StationLocation;
            Drones[DroneIndex].Status = DroneStatuses.maintenance;
            dalObject.ChargeDrone(_Id , StationId);
            ChargingDrones.Add(new ChargingDrone { Id = Drones[DroneIndex].Id, Battery = Drones[Drones.FindIndex(drone => drone.Id == _Id)].Battery });
        }

        public void UnChargeDrone(int Id, double time)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == Id);
            int StationId = 0;
            if (Drones[DroneIndex].Status != DroneStatuses.maintenance)
                throw new NotInMaintenance(Id);
            Drones[DroneIndex].Battery += time * dalObject.GetBatteryUsage()[0];
            Drones[DroneIndex].Status = DroneStatuses.Available;
            foreach (IDAL.DO.Station station in dalObject.GetStations())
            {
                if (station.Lattitude == Drones[DroneIndex].CurrentLocation.Lattitude && station.Longitude == Drones[DroneIndex].CurrentLocation.Longitude)
                    StationId = station.Id;
            }
            dalObject.UnChargeDrone(Id , StationId);
        }

        public void LinkDroneToParcel(int DroneId)
        {
            //creating needed variables
            IDAL.DO.Parcel BestParcel = new IDAL.DO.Parcel();
            int DroneIndex = Drones.FindIndex(drone => drone.Id == DroneId);
            //throwing exception 
            if (Drones[DroneIndex].Status != DroneStatuses.Available)
                throw new UnAvailabe(DroneId);
            //running on each free parcel to see which is best
            foreach (IDAL.DO.Parcel parcel in dalObject.GetFreeParcels())
            {
                //finding nearest station to parcels target
                Location ClosestStationLocation = ClosestStation(Drones[DroneIndex].CurrentLocation);
                //adding up the total distance
                double TotalDistance = Distance(Drones[DroneIndex].CurrentLocation, SenderLocation(parcel.Id)) + Distance(SenderLocation(parcel.Id), ReciverLocation(parcel.Id)) + Distance(ReciverLocation(parcel.Id),ClosestStationLocation);
                //making sure the parcel is even relevant
                if (parcel.Weight <= (IDAL.DO.WeightCategories)Drones[DroneIndex].MaxWeight &&  TotalDistance < Drones[DroneIndex].Battery / dalObject.GetBatteryUsage()[(int)parcel.Weight])
                {
                    //finding best parcel
                    if (parcel.Priority > BestParcel.Priority)
                        BestParcel = parcel;
                    else if (parcel.Priority == BestParcel.Priority)
                        if (parcel.Weight > BestParcel.Weight)
                            BestParcel = parcel;
                        else if (parcel.Weight == BestParcel.Weight)
                            if (Distance(Drones[DroneIndex].CurrentLocation , SenderLocation(parcel.Id)) < Distance(Drones[DroneIndex].CurrentLocation , SenderLocation(BestParcel.Id)))
                                BestParcel = parcel;
                }
            }
            //updating the parcel
            BestParcel.Scheduled = DateTime.Now;
            BestParcel.DroneId = DroneId;
            dalObject.UpdateParcel(BestParcel);
            //updating the drone
            Drones[DroneIndex].Status = DroneStatuses.Delivery;
        }
        public Drone DisplayDrone(int Id)
        {
            try
            {
                dalObject.GetDrone(Id);
            }
            catch (DalObject.IdNotExistException EX)
            {
                throw new IdNotExistException(Id);
            }
            int DroneIndex = Drones.FindIndex(drone => drone.Id == Id);
            Drone drone = new Drone();
            drone.Id = Drones[DroneIndex].Id;
            drone.Model = Drones[DroneIndex].Model;
            drone.Weight = Drones[DroneIndex].MaxWeight;
            drone.Battery = Drones[DroneIndex].Battery;
            drone.Status = Drones[DroneIndex].Status;
            drone.CurrentLocation = Drones[DroneIndex].CurrentLocation;
            //creating a parcel of type moved parcel to put in drone
            if (Drones[DroneIndex].ParcelId != -1)
            {
                MovedParcel mParcel = new MovedParcel();
                IDAL.DO.Parcel parcel = dalObject.GetParcel(Drones[DroneIndex].ParcelId);

                mParcel.Id = parcel.Id;
                mParcel.Status = (parcel.PickedUp == null) ? false : true;
                mParcel.Priority = (Priorities)parcel.Priority;
                mParcel.Weight = (WeightCategories)parcel.Weight;
                //creating 2 customers of type CustomerInParcel for our movedParcel
                CustomerInParcel Sender = new CustomerInParcel();
                CustomerInParcel Reciver = new CustomerInParcel();
                IDAL.DO.Customer sCustomer = dalObject.GetCustomer(parcel.SenderId);
                IDAL.DO.Customer tCustomer = dalObject.GetCustomer(parcel.TargetId);
                Sender.Id = sCustomer.Id;
                Sender.name = sCustomer.Name;
                Reciver.Id = tCustomer.Id;
                Reciver.name = tCustomer.Name;
                //putting the customers in the movedParcel
                mParcel.Sender = Sender;
                mParcel.Reciver = Reciver;
                mParcel.PickUp = SenderLocation(parcel.Id);
                mParcel.DropOff = ReciverLocation(parcel.Id);
                mParcel.Distance = Distance(SenderLocation(parcel.Id), ReciverLocation(parcel.Id));
                drone.Parcel = mParcel;
            }
            return drone;
        }
        public IEnumerable<DroneForList> DisplayDrones()
        {
            return Drones;
        }
    }
}
