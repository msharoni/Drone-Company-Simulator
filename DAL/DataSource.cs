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
            int[] DroneIds = new int[10];
            for (int i = 0; i < 10; ++i)
                Drones.Add(new Drone { Id = r.Next(100), Model = "Model" + r.Next(100).ToString(), MaxWeight = (WeightCategories)r.Next(1,4) });
            for (int i = 0; i < 2; ++i)
                Stations.Add(new Station { Id = r.Next(1000), Name = "Station" + r.Next(100).ToString(), Longitude = r.Next(-180, 181), Lattitude = r.Next(-90, 91), ChargeSlots = r.Next(100) });
            for (int i = 0; i < 10; ++i)
                Customers.Add(new Customer { Id = r.Next(100000000, 1000000000), Name = Names[r.Next(Names.Length)], Phone = r.Next(100000000, 1000000000).ToString(), Longitude = r.Next(-180, 181), Lattitude = r.Next(-90, 91) });
            for (int i = 0; i < 10; i++)
                DroneIds[i] = r.Next(0, 2) == 1 ? -1 : Drones[i].Id;
            for (int i = 0; i < 10; ++i)
            {
                Drone drone = Drones.Find(drone => drone.Id == DroneIds[i]);
                Parcels.Add(new Parcel { 
                    Id = r.Next(100000000, 1000000000),
                    SenderId = Customers[r.Next(0, Customers.Count)].Id,
                    TargetId = Customers[r.Next(0, Customers.Count)].Id,
                    Weight = drone.Id != -1 ? (WeightCategories)r.Next(1,(int)drone.MaxWeight + 1) : (WeightCategories)r.Next(1,4),
                    Priority = (Priorities)r.Next(1, 4),
                    Requested = DateTime.Now,
                    DroneId = drone.Id,
                    Scheduled = drone.Id == -1 ? null : DateTime.Now,
                    PickedUp = drone.Id == -1 ? null : r.Next(0,2) ==1 ? null : DateTime.Now,
                    Delivered = drone.Id == -1 ? null : r.Next(0, 2) == 1 ? null : DateTime.Now
                });
            }
        }
        internal class Config
        {
            //the first place is charge speed second is no weight flight third is light weight...
            internal static double[] batteryPerKm = { 0.1, 0.001, 0.0015, 0.002, 0.0025 };

        }

    }
}