using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
namespace BL
{
    sealed partial class BL : IBL
    {
        public void UpdateStation(int Id, string? name, int? NumOfSlots)
        {
            if (name != null)
                dalObject.UpdateStation(Id, name, null);
            if (NumOfSlots != null)
                dalObject.UpdateStation(Id, null, NumOfSlots);
        }
        public void AddStation(int Id,string Name,Location location,int ChargeSlots)
        {
            try
            {
                dalObject.AddStation(Id, Name, location.Longitude, location.Lattitude, ChargeSlots);
            }
            catch ( DO.IdExcistsException)
            {
                throw new IdExcistsException(Id);
            }
        }
        public Station DisplayStation(int Id)
        {
            Station tmpStation = new Station();
            try { 
                dalObject.GetStation(Id);
            }
            catch(DO.IdNotExistException EX)
            {
                throw new IdNotExistException(Id);
            }
            DO.Station station = dalObject.GetStation(Id);
            //putting the ready variables into tmpStation 
            tmpStation.Id = Id;
            tmpStation.Name = station.Name;
            tmpStation.Location = new Location { Lattitude = station.Lattitude, Longitude = station.Longitude };
            tmpStation.NumOfVacantChargers = station.ChargeSlots;
            //creating list of all drones charging at current station
            List<ChargingDrone> drones = new List<ChargingDrone>();
            foreach(DO.DroneCharge drone in dalObject.GetChargingDrones())
                if (drone.StationId == Id)
                    drones.Add(new ChargingDrone { Id = drone.DroneId, Battery = DisplayDrone(drone.DroneId).Battery});
            tmpStation.DronesCharging = drones;
            return tmpStation;
        }
        public IEnumerable<StationForList> DisplayStations()
        {
            List<StationForList> stations = new List<StationForList>();
            foreach(DO.Station station in dalObject.GetStations())
            {
                StationForList CurrentStation = new StationForList();
                CurrentStation.Id = station.Id;
                CurrentStation.Name = station.Name;
                CurrentStation.NumOfVacantChargers = station.ChargeSlots;
                CurrentStation.NumOfOccupiedChargers = DisplayStation(station.Id).DronesCharging.Count();
                stations.Add(CurrentStation);
            }
            return stations;
        }
        public IEnumerable<StationForList> DisplayFreeStations()
        {
            return DisplayStations().Where(station => station.NumOfVacantChargers > 0);
        }


    }
}



