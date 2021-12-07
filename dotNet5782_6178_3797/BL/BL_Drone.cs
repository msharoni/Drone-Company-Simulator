using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    partial class BL : IBL.IBL
    {
        List<DroneForList> Drones;
        List<ChargingDrone> ChargingDrones;
        DalObject.DalObject dalObject;
        Random r = new Random();
       
        public void AddDrone(int _Id, string _Model, int _MaxWeight, int _StationId)
        {
            dalObject.AddDrone(_Id, _Model, (IDAL.DO.WeightCategories)_MaxWeight);
            Location StationLocation =  new Location { Longitude = dalObject.GetStation(_StationId).Longitude , Lattitude = dalObject.GetStation(_StationId).Lattitude };
            Drones.Add(new DroneForList { Id = _Id, Model = _Model, Weight = (WeightCategories)_MaxWeight, Battery = r.Next(20, 41), Status = DroneStatuses.maintenance, CurrentLocation = StationLocation, ParcelId = -1, batteryPerKM = 1/r.Next(1,11) });
        }
        void UpdateDrone(int _Id, string _Model)
        {
            Drones[Drones.FindIndex(drone => drone.Id == _Id)].Model = _Model;
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
        void ChargeDrone(int _Id)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == _Id);
            int StationId = 0;
            double smallest = 2147483647;
            Location StationLocation = new Location();
            if (Drones[DroneIndex].Status != DroneStatuses.Available)
                throw new UnAvailabe(_Id);
            for (int i = 0; i < Stations.Count; i++)
            {
                StationLocation = new Location { Longitude = dalObject.GetStation(Stations[i].Id).Longitude, Lattitude = dalObject.GetStation(Stations[i].Id).Lattitude };
                if (Distance(StationLocation,Drones[DroneIndex].CurrentLocation) <= smallest) 
                {
                    smallest = Distance(StationLocation, Drones[DroneIndex].CurrentLocation);
                    StationId = Stations[i].Id;
                }
            }
            if (Drones[DroneIndex].Battery < smallest * Drones[DroneIndex].batteryPerKM)
                throw new NotEnoughBattery(_Id);
            Drones[DroneIndex].Battery -= smallest * Drones[DroneIndex].batteryPerKM;
            Drones[DroneIndex].CurrentLocation = StationLocation;
            Drones[DroneIndex].Status = DroneStatuses.maintenance;
            Stations[Stations.FindIndex(station => station.Id == StationId)].NumOfVacantChargers--;
            ChargingDrones.Add(new ChargingDrone { Id = Drones[DroneIndex].Id, Battery = Drones[Drones.FindIndex(drone => drone.Id == _Id)].Battery });
        }

        void UnChargeDrone(int Id, double time)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == Id);
            int StationId = 0;
            if (Drones[DroneIndex].Status != DroneStatuses.maintenance)
                throw new NotInMaintenance(Id);
            Drones[DroneIndex].Battery += time * Drones[DroneIndex].batteryPerKM;
            Drones[DroneIndex].Status = DroneStatuses.Available;
            for (int i = 0; i < dalObject.GetStations().Count(); i++)
            {
                if (dalObject.GetStations().ElementAt(i).Lattitude == Drones[DroneIndex].CurrentLocation.Lattitude && dalObject.GetStations().ElementAt(i).Longitude == Drones[DroneIndex].CurrentLocation.Longitude)
                    StationId = dalObject.GetStations().ElementAt(i).Id;
            }
            Stations[Stations.FindIndex(station => station.Id == StationId)].NumOfVacantChargers--;
        }

        void LinkDroneToParcel(int DroneId)
        {
            List<ParcelForList> tmp = Parcels;
            int DroneIndex = Drones.FindIndex(drone => drone.Id == DroneId);
            Location SenderLocation = new Location;
            if (Drones[DroneIndex].Status != DroneStatuses.Available)
                throw new UnAvailabe(DroneId);
            ParcelForList BestParcel = new ParcelForList();
            for (int i = 0; i < Parcels.Count(); i++)
            {
                if (Parcels[i].Weight <= Drones[DroneIndex].MaxWeight && Distance(Drones[DroneIndex].CurrentLocation , CustomerLocation(Parcels[i].Id)) + Distance(, CustomerLocation(Parcels[i].Id))  < Drones[DroneIndex].Battery / Drones[DroneIndex].batteryPerKM)
                {

                    if (Parcels[i].Priority > BestParcel.Priority)
                        BestParcel = Parcels[i];
                    else if (Parcels[i].Priority == BestParcel.Priority)
                        if (Parcels[i].Weight > BestParcel.Weight)
                            BestParcel = Parcels[i];
                        else if (Parcels[i].Weight == BestParcel.Weight)
                            if (Distance(Drones[DroneIndex].CurrentLocation , CustomerLocation(Parcels[i].Id)) < Distance(Drones[DroneIndex].CurrentLocation , CustomerLocation(BestParcel.Id))
                                BestParcel = Parcels[i];
                }
            }
            
        }
        Location CustomerLocation(int ParcelId,)
        {
            return new Location { Longitude = dalObject.GetCustomers().FirstOrDefault(customer => customer.Id == dalObject.GetParcel(ParcelId).SenderId).Longitude, Lattitude = dalObject.GetCustomers().FirstOrDefault(customer => customer.Id == dalObject.GetParcel(ParcelId).SenderId).Lattitude }
        }
        void PickUpParcel(int DroneId)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == DroneId);
            if (Drones[DroneIndex].Status != DroneStatuses.Delivery)
                throw new NotLinkedYet(DroneId);
            if (dalObject.GetParcel(Drones[DroneIndex].ParcelId).PickedUp != null)
                throw new ParcelHasAlreadyBeenPickedUp(Drones[DroneIndex].ParcelId);
            Drones[DroneIndex].Battery -= Drones[DroneIndex].batteryPerKM * Distance(Drones[DroneIndex].CurrentLocation, CustomerLocation(Drones[DroneIndex].ParcelId));
            //might have a error here
            Drones[DroneIndex].CurrentLocation = CustomerLocation(Drones[DroneIndex].ParcelId);
            Parcels.ForEach(parcel => parcel.Id == Drones[DroneIndex].ParcelId).PickUp = DateTime.Now;
        }
        void DeliverParcel(int DroneId)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == DroneId);
            //need to create exception
            if (dalObject.GetParcel(Drones[DroneIndex].Status.ParcelId).Delivered != null && dalObject.GetParcel(Drones[DroneIndex].Status.ParcelId).PickedUp != null)
                throw new NotLinkedOrAlreadyDelivered(DroneId);
            Drones[DroneIndex].Status = DroneStatuses.Available;
            Drones[DroneIndex].Battery -= Drones[DroneIndex].batteryPerKM * Distance(Drones[DroneIndex].CurrentLocation, CustomerLocation(Drones[DroneIndex].ParcelId)); 


        }
    }
}
