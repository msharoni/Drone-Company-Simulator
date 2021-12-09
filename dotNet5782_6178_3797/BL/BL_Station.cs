using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        List<StationForList> Stations;
        public void UpdateStation(int Id, string? name, int? NumOfSlots)
        {
            if (name != null)
                dalObject.UpdateStation(Id, name, null);
            if (NumOfSlots != null)
                dalObject.UpdateStation(Id, null, NumOfSlots);
        }
        public void AddStation()
        {
            IDAL.DO.Station station = new IDAL.DO.Station();
            Console.WriteLine("Enter Id: ");
            station.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Name: ");
            station.Name = Console.ReadLine();
            Console.WriteLine("Enter Longitude: ");
            station.Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Lattitude: ");
            station.Lattitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter amount of Charging Slots: ");
            station.ChargeSlots = Convert.ToInt32(Console.ReadLine());
            dalObject.AddStation(station.Id, station.Name, station.Longitude, station.Lattitude, station.ChargeSlots);
            //list of charging drones?
        }
        public Station DisplayStation(int Id)
        {
            Station tmpStation = new Station();
            IDAL.DO.Station station = dalObject.GetStation(Id);
            //putting the ready variables into tmpStation 
            tmpStation.Id = Id;
            tmpStation.Name = station.Name;
            tmpStation.Location = new Location { Lattitude = station.Lattitude, Longitude = station.Longitude };
            tmpStation.NumOfVacantChargers = station.ChargeSlots;
            //creating list of all drones charging at current station
            List<ChargingDrone> drones = new List<ChargingDrone>();
            foreach(DroneForList drone in Drones)
                if (drone.CurrentLocation == tmpStation.Location)
                    drones.Add(new ChargingDrone { Id = drone.Id, Battery = drone.Battery});
            tmpStation.DronesCharging = drones;
            return tmpStation;
        }
        public List<StationForList> DisplayStations()
        {
            List<StationForList> stations = new List<StationForList>();
            StationForList CurrentStation = new StationForList();
            foreach(IDAL.DO.Station station in dalObject.GetStations())
            {
                CurrentStation.Id = station.Id;
                CurrentStation.Name = station.Name;
                CurrentStation.NumOfVacantChargers = station.ChargeSlots;
                CurrentStation.NumOfOccupiedChargers = DisplayStation(station.Id).DronesCharging.Count();
                stations.Add(CurrentStation);
            }
            return stations;
        }
        public List<StationForList> DisplayFreeStations()
        {
            return DisplayStations().FindAll(station => station.NumOfVacantChargers > 0);
        }


    }
}
