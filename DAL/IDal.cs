using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;

namespace IDAL
{
    interface IDal
    {
        public void AddDrone(int Id, string Model, DO.WeightCategories MaxWeight, DO.DroneStatuses Status, double Battery);
        public void AddStation(int Id, string Name, double Longitude, double Lattitude, int ChargeSlots);
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude);
        public void AddParcel(int Id, int SenderId, int TargetId, DO.WeightCategories Weight, DO.Priorities Priority, DateTime Requested, int? DroneId, DateTime? Scheduled, DateTime? PickedUp, DateTime? Delivered);
        public void LinkParcel(int DroneId, int ParcelId);
        public void DronePickUp(int ParcelId);
        public void DroneDropOff(int ParcelId);
        public void ChargeDrone(int DroneId, int StationId);
        public Drone GetDrone(int DroneId);
        public Parcel GetParcel(int ParcelId);
        public Customer GetCustomer(int CustomerId);
        public Station GetStation(int StationId);
        public List<Station> GetStations();
        public List<Drone> GetDrones();
        public List<Parcel> GetParcels();
        public List<Customer> GetCustomers();
        public List<Parcel> GetFreeParcels();
        public List<Station> GetFreeStations();
        public String sexagisamel(double Longitude, double Lattitude);
    }
}
