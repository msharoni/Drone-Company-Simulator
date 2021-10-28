using System;
using System.Collections.Generic;
using IDAL.DO;
namespace DalObject{
    public class DalObject{
        public DalObject()
        {
            DataSource.Intalize();
        }
        //adding each type using List.Add
        public void AddDrone(int Id, string Model, WeightCategories MaxWeight, DroneStatuses Status, double Battery){
            DataSource.Drones.Add(new Drone(Id,Model,MaxWeight,Status,Battery));
        }
        public void AddStation(int Id, string Name, double Longitude, double Lattitude, int ChargeSlots){
            DataSource.Stations.Add(new Station(Id,Name,Longitude,Lattitude,ChargeSlots));
        }
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude){
            DataSource.Customers.Add(new Customer(Id,Name,Phone,Longitude,Lattitude));
        }
        public void AddParcel(int Id, int SenderId, int TargetId, WeightCategories Weight, Priorities Priority, DateTime Requested, int? DroneId, DateTime? Scheduled, DateTime? PickedUp, DateTime? Delivered){
            DataSource.Parcels.Add(new Parcel(Id,SenderId,TargetId,Weight,Priority,Requested,DroneId,Scheduled,PickedUp,Delivered));
        }

        //Linking the parcel to a drone
        public void LinkParcel(int DroneId, int ParcelId)
        {
            //finding the relevant drone/parcel
            Drone drone = DataSource.Drones.Find(drone => drone.Id == DroneId);
            Parcel parcel = DataSource.Parcels.Find(parcel => parcel.Id == ParcelId);
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Pindex = DataSource.Parcels.IndexOf(parcel);
            //making the needed changes
            drone.Status = DroneStatuses.Delivery;
            parcel.DroneId = DroneId;
            parcel.Scheduled = DateTime.Now;
            //copying them back into the List
            DataSource.Parcels[Pindex] = parcel;
            DataSource.Drones[Dindex] = drone;
        }
        public void DronePickUp(int ParcelId)
        {
            //finding the relevant drone/parcel
            Parcel parcel = DataSource.Parcels.Find(parcel => parcel.Id == ParcelId);
            Drone drone = DataSource.Drones.Find(drone=> drone.Id == parcel.DroneId);
            //saving their index for later
            int Pindex = DataSource.Parcels.IndexOf(parcel);
            //making the needed changes
            parcel.PickedUp = DateTime.Now;
            //copying them back into the list
            DataSource.Parcels[Pindex] = parcel;
        }
        public void DroneDropOff(int ParcelId)
        {
            //finding the relevant drone/parcel
            Parcel parcel = DataSource.Parcels.Find(parcel => parcel.Id == ParcelId);
            Drone drone = DataSource.Drones.Find(drone=> drone.Id == parcel.DroneId);
            //saving their index for later
            int Pindex = DataSource.Parcels.IndexOf(parcel);
            int Dindex = DataSource.Drones.IndexOf(drone);
            //making the needed changes
            parcel.Delivered = DateTime.Now;
            drone.Status = DroneStatuses.Available;
             //copying them back into the list
            DataSource.Parcels[Pindex] = parcel;
            DataSource.Drones[Dindex] = drone;
        }
        public void ChargeDrone(int DroneId, int StationId)
        {
            //finding the relevant drone/station
            Drone drone = DataSource.Drones.Find(drone => drone.Id == DroneId);
            Station station = DataSource.Stations.Find(station => station.Id == StationId);
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Sindex = DataSource.Stations.IndexOf(station);
            //making the needed changes
            station.ChargeSlots -= 1;
            drone.Status = DroneStatuses.maintenance;
            DataSource.ChargingDrones.Add(new DroneCharge(DroneId,StationId));
             //copying them back into the list    
            DataSource.Drones[Dindex] = drone;
            DataSource.Stations[Sindex] = station;            
        }
        public void UnChargeDrone(int DroneId, int StationId)
        {
            //finding the relevant drone/station/ChargingDrone
            Drone drone = DataSource.Drones.Find(drone => drone.Id == DroneId);
            Station station = DataSource.Stations.Find(station => station.Id == StationId);
            DroneCharge charging = DataSource.ChargingDrones.Find(charging => charging.DroneId == DroneId);
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Sindex = DataSource.Stations.IndexOf(station);
            int Cindex = DataSource.ChargingDrones.IndexOf(charging);
            //making the needed changes
            station.ChargeSlots += 1;
            drone.Status = DroneStatuses.Available;
            DataSource.ChargingDrones.RemoveAt(Cindex);
             //copying them back into the list    
            DataSource.Drones[Dindex] = drone;
            DataSource.Stations[Sindex] = station;            
        }
        public Drone GetDrone(int DroneId){
            return DataSource.Drones.Find(drone=> drone.Id == DroneId);
        }
        public Parcel GetParcel(int ParcelId){
            return DataSource.Parcels.Find(parcel=> parcel.Id == ParcelId);

        }
        public Customer GetCustomer(int CustomerId){
            return DataSource.Customers.Find(customer => customer.Id == CustomerId);
        }
        public Station GetStation(int StationId)
        {
            return DataSource.Stations.Find(station => station.Id == StationId);
        }

        //returning each list 
        public List<Station> GetStations(){
            return DataSource.Stations;
        }
        public List<Drone> GetDrones(){
            return DataSource.Drones;
        }
        public List<Parcel> GetParcels(){
            return DataSource.Parcels;
        }
        public List<Customer> GetCustomers(){
            return DataSource.Customers;
        }

        //making sure that the relevant fields exsist
        public List<Parcel> GetFreeParcels(){
            return DataSource.Parcels.FindAll(parcel => parcel.DroneId == 0);
        }
        public List<Station> GetFreeStations(){
            return DataSource.Stations.FindAll(station => station.ChargeSlots != 0);
        }
        

    }

    class DataSource{ 
            internal static List<Drone> Drones = new List<Drone>();
            internal static List<Station> Stations = new List<Station>();
            internal static List<Customer> Customers = new List<Customer>();
            internal static List<Parcel> Parcels = new List<Parcel>();
            internal static List<DroneCharge> ChargingDrones = new List<DroneCharge>();
            public static void Intalize(){
                Random r = new Random();
                string[] Names = { "Rufus", "Bear", "Dakota", "Fido","Vanya", "Samuel", "Koani", "Volodya", "Prince", "Yiska" };
                for(int i=0; i<5; ++i)
                    Drones.Add(new Drone {Id = r.Next(100), Model= "Model"+r.Next(100).ToString(), MaxWeight = ( WeightCategories)r.Next(3)  , Status= (DroneStatuses)r.Next(3), Battery = r.Next(101)});
                for (int i = 0; i < 2; ++i)
                    Stations.Add(new Station { Id = r.Next(1000), Name = "Station" + r.Next(100).ToString(), Longitude = r.Next(-180, 181), Lattitude = r.Next(-90, 91), ChargeSlots = r.Next(100) });
                for(int i=0; i<10; ++i)
                    Customers.Add(new Customer {Id = r.Next(99999990,1000000000), Name= Names[r.Next(Names.Length)], Phone= r.Next(99999999,1000000000).ToString(), Longitude= r.Next(-180,181), Lattitude= r.Next(-90,91)});
                for(int i=0; i<10; ++i)
                    Parcels.Add(new Parcel {Id = r.Next(99999999,1000000000), SenderId = r.Next(99999999,1000000000) , TargetId = r.Next(99999999,1000000000),Weight = (WeightCategories)r.Next(3),Priority = (Priorities)r.Next(3), Requested = DateTime.Now, DroneId = r.Next(100), Scheduled = new DateTime(), PickedUp= new DateTime(),Delivered = new DateTime() });
            }
            internal class Config{

                
                

            }
        }

        
}