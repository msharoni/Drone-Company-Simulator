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
            return station;
        }
        public List<StationForList> DisplayStations()
        {
            return stations;
        }
        public List<StationForList> DisplayFreeStations()
        {
            return freeStations;
        }


    }
}
