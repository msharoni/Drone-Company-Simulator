using System;
using DO;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DalApi
{
    public interface IDal
    {
        void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude);
        void AddDrone(int Id, string Model, WeightCategories MaxWeight);
        void AddParcel(int Id, int SenderId, int TargetId, WeightCategories Weight, Priorities Priority, DateTime? Requested, int? DroneId, DateTime? Scheduled, DateTime? PickedUp, DateTime? Delivered);
        void AddStation(int Id, string Name, double Longitude, double Lattitude, int ChargeSlots);
        void ChargeDrone(int DroneId, int StationId);
        void DroneDropOff(int ParcelId);
        void DronePickUp(int ParcelId);
        Customer GetCustomer(int CustomerId);
        IEnumerable<Customer> GetCustomers();
        Drone GetDrone(int DroneId);
        IEnumerable<Drone> GetDrones();
        IEnumerable<Parcel> GetFreeParcels();
        Parcel GetParcel(int ParcelId);
        IEnumerable<Parcel> GetParcels();
        Station GetStation(int StationId);
        IEnumerable<Station> GetStations();
        IEnumerable<Station> GetVacantStations();
        void LinkParcel(int DroneId, int ParcelId);
        void UnChargeDrone(int DroneId, int StationId);
        void UpdateDrone(int _Id, string _Model);
        void UpdateCustomer(int Id, string? name, string? Phone);
        void UpdateParcel(Parcel _parcel);
        void UpdateStation(int Id, string? name, int? NumOfSlots);
        public double[] GetBatteryUsage();
        public IEnumerable<DroneCharge> GetChargingDrones();
    }
}