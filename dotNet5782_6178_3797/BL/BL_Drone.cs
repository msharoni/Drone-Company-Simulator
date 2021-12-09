using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        List<DroneForList> Drones;
        List<ChargingDrone> ChargingDrones;
        IDAL.IDAL dalObject = new DalObject.DalObject();
        Random r = new Random();
       
        public  void AddDrone(int _Id, string _Model, int _MaxWeight, int _StationId)
        {
            dalObject.AddDrone(_Id, _Model, (IDAL.DO.WeightCategories)_MaxWeight);
            Location StationLocation =  new Location { Longitude = dalObject.GetStation(_StationId).Longitude , Lattitude = dalObject.GetStation(_StationId).Lattitude };
            Drones.Add(new DroneForList { Id = _Id, Model = _Model, MaxWeight = (WeightCategories)_MaxWeight, Battery = r.Next(20, 41), Status = DroneStatuses.maintenance, CurrentLocation = StationLocation, ParcelId = -1, batteryPerKM = 1/r.Next(1,11) });
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
            if (Drones[DroneIndex].Battery < smallest * Drones[DroneIndex].batteryPerKM)
                throw new NotEnoughBattery(_Id);
            Drones[DroneIndex].Battery -= smallest * Drones[DroneIndex].batteryPerKM;
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
            Drones[DroneIndex].Battery += time * Drones[DroneIndex].batteryPerKM;
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
            int StationId = 0;
            double smallest = 999999999;
            Location ClosestStationLocation = new Location();
            //throwing exception 
            if (Drones[DroneIndex].Status != DroneStatuses.Available)
                throw new UnAvailabe(DroneId);
            //running on each free parcel to see which is best
            foreach (IDAL.DO.Parcel parcel in dalObject.GetFreeParcels())
            {
                //finding nearest station to parcels target
                foreach (IDAL.DO.Station station in dalObject.GetStations())
                {
                    ClosestStationLocation = new Location { Longitude = station.Longitude, Lattitude = station.Lattitude };
                    if (Distance(ClosestStationLocation, Drones[DroneIndex].CurrentLocation) <= smallest)
                    {
                        smallest = Distance(ClosestStationLocation, Drones[DroneIndex].CurrentLocation);
                        StationId = station.Id;
                    }
                }
                //adding up the total distance
                double TotalDistance = Distance(Drones[DroneIndex].CurrentLocation, SenderLocation(parcel.Id)) + Distance(SenderLocation(parcel.Id), ReciverLocation(parcel.Id)) + Distance(ReciverLocation(parcel.Id),ClosestStationLocation);
                //making sure the parcel is even relevant
                if (parcel.Weight <= (IDAL.DO.WeightCategories)Drones[DroneIndex].MaxWeight &&  TotalDistance < Drones[DroneIndex].Battery / Drones[DroneIndex].batteryPerKM)
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
            int DroneIndex = Drones.FindIndex(drone => drone.Id == Id);
            Drone drone = new Drone();
            drone.Id = Drones[DroneIndex].Id;
            drone.Model = Drones[DroneIndex].Model;
            drone.Weight = Drones[DroneIndex].Weight;
            drone.Battery = Drones[DroneIndex].Battery;
            drone.Status = Drones[DroneIndex].Status;
            drone.CurrentLocation = Drones[DroneIndex].CurrentLocation;
            //creating a parcel of type moved parcel to put in drone
            MovedParcel mParcel = new MovedParcel();
            IDal.DO.Parcel parcel = dalObject.GetParcel(Drones[DroneIndex].parcelId);
            mParcel.Id = parcel.Id;
            mParcel.Status = (parcel.PickedUp == null) ? false : true; 
            mParcel.Priority = parcel.Priority;
            mParcel.Weight = parcel.Weight;
            //creating 2 customers of type CustomerInParcel for our movedParcel
            CustomerInParcel Sender = new CustomerInParcel();
            CustomerInParcel Reciver = new CustomerInParcel();
            IDal.DO.Customer sCustomer = dalObject.GetCustomer(parcel.SenderId);
            IDal.DO.Customer tCustomer = dalObject.GetCustomer(parcel.TargetId)
            Sender.Id = sCustomer.Id;
            Sender.name = sCustomer.Name;
            Reciver.Id = tCustomer.Id;
            Reciver.name = tCustomer.Name;
            //putting the customers in the movedParcel
            mParcel.Sender = Sender;
            mParcel.Reciver = Reciver;
            mParcel.PickUp = SenderLocation(parcel.Id);
            mParcel.DropOff = ReciverLocation(parcel.Id);
            mParcel.Distance = Distance(SenderLocation(parcel.Id),ReciverLocation(parcel.Id));
            drone.Parcel = mParcel;
            return drone;
        }
        public List<DroneForList> DisplayDrones()
        {
            
            return drones;
        }
    }
}
