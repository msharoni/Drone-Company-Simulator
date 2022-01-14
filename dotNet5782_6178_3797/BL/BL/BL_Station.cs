using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
namespace BL
{
    sealed partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(int Id, string? name, int? NumOfSlots)
        {
            lock (Idal)
            {
                if (name != null)
                    try
                    {
                        Idal.UpdateStation(Id, name, null);
                    }
                    catch (DO.IdNotExistException)
                    {
                        throw new IdNotExistException(Id);
                    }
                if (NumOfSlots != null)
                    try
                    {
                        Idal.UpdateStation(Id, null, NumOfSlots);
                    }
                    catch (DO.IdNotExistException)
                    {
                        throw new IdNotExistException(Id);
                    }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(int Id,string Name,Location location,int ChargeSlots)
        {
            lock (Idal)
            {
                try
                {
                    Idal.AddStation(Id, Name, location.Longitude, location.Lattitude, ChargeSlots);
                }
                catch (DO.IdExcistsException)
                {
                    throw new IdExcistsException(Id);
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station DisplayStation(int Id)
        {
            lock (Idal)
            {
                Station tmpStation = new Station();
                try
                {
                    Idal.GetStation(Id);
                }
                catch (DO.IdNotExistException EX)
                {
                    throw new IdNotExistException(Id);
                }
                DO.Station station = Idal.GetStation(Id);
                //putting the ready variables into tmpStation 
                tmpStation.Id = Id;
                tmpStation.Name = station.Name;
                tmpStation.Location = new Location { Lattitude = station.Lattitude, Longitude = station.Longitude };
                tmpStation.NumOfVacantChargers = station.ChargeSlots;
                //creating list of all drones charging at current station
                List<ChargingDrone> drones = new List<ChargingDrone>();
                foreach (DO.DroneCharge drone in Idal.GetChargingDrones())
                    if (drone.StationId == Id)
                        drones.Add(new ChargingDrone { Id = drone.DroneId, Battery = DisplayDrone(drone.DroneId).Battery });
                tmpStation.DronesCharging = drones;
                return tmpStation;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationForList> DisplayStations()
        {
            lock (Idal)
            {
                List<StationForList> stations = new List<StationForList>();
                foreach (DO.Station station in Idal.GetStations())
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
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<StationForList> DisplayFreeStations()
        {
            return DisplayStations().Where(station => station.NumOfVacantChargers > 0);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> DronesChargingInStation(int StationId)
        {
            lock (Idal)
            {
                Station station = DisplayStation(StationId);
                List<Drone> ChargingDroneList = new List<Drone>();
                foreach (ChargingDrone drone in station.DronesCharging)
                {
                    ChargingDroneList.Add(DisplayDrone(drone.Id));
                }
                return ChargingDroneList;
            }
        }
    }
}



