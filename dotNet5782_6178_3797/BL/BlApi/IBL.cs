using System;
using System.Collections.Generic;
using BO;
namespace BlApi
{
    public interface IBL
    {
        double[] GetBatteryUsages();
        void AddCustomer(int Id, string Name, string Phone, Location location);
        void AddDrone(int _Id, string _Model, WeightCategories _MaxWeight, int _StationId);
        void AddParcel(int Id, int SenderId, int TargetId, WeightCategories Weight, Priorities Priority);
        void AddStation(int Id, string Name, Location location, int ChargeSlots);
        void ChargeDrone(int _Id);
        void DeliverParcel(int DroneId);
        Customer DisplayCustomer(int Id);
        IEnumerable<CustomerForList> DisplayCustomers();
        Drone DisplayDrone(int Id);
        public IEnumerable<DroneForList> DisplayDrones(DroneStatuses? DS, WeightCategories? DC);
        IEnumerable<ParcelForList> DisplayFreeParcels();
        IEnumerable<StationForList> DisplayFreeStations();
        Parcel DisplayParcel(int Id);
        IEnumerable<ParcelForList> DisplayParcels();
        Station DisplayStation(int Id);
        IEnumerable<StationForList> DisplayStations();
        void LinkDroneToParcel(int DroneId);
        void PickUpParcel(int DroneId);
        void UnChargeDrone(int Id, double time);
        void UpdateCustomer(int Id, string name, string Phone);
        void UpdateDrone(int _Id, string _Model);
        void UpdateStation(int Id, string name, int? NumOfSlots);
        public IEnumerable<ParcelForList> GetFilterdParcels(Customer Customer, DateTime? startDate, DateTime? endDate, Priorities? Priority, WeightCategories? Weight, ParcelStatus? Status);
        void ActivateSimulator(int DroneId, Action Update, Func<bool> stop);
        public double Distance(Location Location1, Location Location2);
        public void UpdateDroneLocation(int Id, double Battery, Location newLocation);
        public IEnumerable<Drone> DronesChargingInStation(int StationId);
    }
}