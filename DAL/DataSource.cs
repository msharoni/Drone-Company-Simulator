using System;
using System.Collections.Generic;
using IDAL.DO;
namespace DalObject
{
    class DataSource
    {
        internal static List<Drone> Drones = new List<Drone>();
        internal static List<Station> Stations = new List<Station>();
        internal static List<Customer> Customers = new List<Customer>();
        internal static List<Parcel> Parcels = new List<Parcel>();
        internal static List<DroneCharge> ChargingDrones = new List<DroneCharge>();
        public static void Intalize()
        {
            Random r = new Random();
            string[] Names = { "Rufus", "Bear", "Dakota", "Fido", "Vanya", "Samuel", "Koani", "Volodya", "Prince", "Yiska" };
            for (int i = 0; i < 5; ++i)
                Drones.Add(new Drone { Id = r.Next(100), Model = "Model" + r.Next(100).ToString(), MaxWeight = (WeightCategories)r.Next(3) });
            for (int i = 0; i < 2; ++i)
                Stations.Add(new Station { Id = r.Next(1000), Name = "Station" + r.Next(100).ToString(), Longitude = r.Next(-180, 181), Lattitude = r.Next(-90, 91), ChargeSlots = r.Next(100) });
            for (int i = 0; i < 10; ++i)
                Customers.Add(new Customer { Id = r.Next(99999990, 1000000000), Name = Names[r.Next(Names.Length)], Phone = r.Next(99999999, 1000000000).ToString(), Longitude = r.Next(-180, 181), Lattitude = r.Next(-90, 91) });
            for (int i = 0; i < 10; ++i)
                Parcels.Add(new Parcel { Id = r.Next(99999999, 1000000000), SenderId = r.Next(99999999, 1000000000), TargetId = r.Next(99999999, 1000000000), Weight = (WeightCategories)r.Next(3), Priority = (Priorities)r.Next(3), Requested = DateTime.Now, DroneId = r.Next(100), Scheduled = new DateTime(), PickedUp = new DateTime(), Delivered = new DateTime() });
        }
        internal class Config
        {
            static double HeavyDroneBatteryKM;
            static double MediumDroneBatteryKM;
            static double LightDroneBatteryKM;



        }

    }
}