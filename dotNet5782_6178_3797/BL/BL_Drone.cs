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
            int index = Stations.FindIndex(station => station.Id == _StationId);
            Drones.Add(new DroneForList { Id = _Id, Model = _Model, Weight = (WeightCategories)_MaxWeight, Battery = r.Next(20, 41), Status = DroneStatuses.maintenance, CurrentLocation = Stations[index].Location, ParcelId = -1, batteryPerKM = 1/r.Next(1,11) });
        }
        void UpdateDrone(int _Id, string _Model)
        {
            Drones[Drones.FindIndex(drone => drone.Id == _Id)].Model = _Model;
        }
        double Distance(int StationId,int DroneId)
        {
            DroneForList tmpD = Drones[Drones.FindIndex(drone => drone.Id == DroneId)];
            StationForList tmpS = Stations[Stations.FindIndex(station => station.Id == StationId)];
            double LongitudeRadian1 = tmpD.CurrentLocation.Longitude * (Math.PI / 180);
            double LattitudeRadian1 = tmpD.CurrentLocation.Lattitude * (Math.PI / 180);
            double LongitudeRadian2 = tmpS.Location.Longitude * (Math.PI / 180);
            double LattitudeRadian2 = tmpS.Location.Lattitude * (Math.PI / 180);

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
            DroneForList tmpD = Drones[Drones.FindIndex(drone => drone.Id == _Id)];
            StationForList tmpS = Stations[0];
            double smallest = 2147483647;
            if (tmpD.Status != DroneStatuses.Available)
                throw new UnAvailabe(_Id);
            for (int i = 0; i < Stations.Count; i++)
            {
                if(Distance(Stations[i].Id,_Id) <= smallest) 
                {
                    smallest = Distance(Stations[i].Id, _Id);
                    tmpS = Stations[i];
                }
            }
            if (tmpD.Battery < smallest * tmpD.batteryPerKM)
                throw new NotEnoughBattery(_Id);
            Drones[Drones.FindIndex(drone => drone.Id == _Id)].Battery -= smallest * tmpD.batteryPerKM;
            Drones[Drones.FindIndex(drone => drone.Id == _Id)].CurrentLocation = tmpS.Location;
            Drones[Drones.FindIndex(drone => drone.Id == _Id)].Status = DroneStatuses.maintenance;
            Stations[Stations.FindIndex(station => station.Id == tmpS.Id)].NumOfVacantChargers++;
            ChargingDrones.Add(new ChargingDrone { Id = tmpD.Id, Battery = Drones[Drones.FindIndex(drone => drone.Id == _Id)].Battery });
        }

    }
}
