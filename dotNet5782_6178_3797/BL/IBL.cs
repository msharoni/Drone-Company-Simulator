using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        void AddCustomer(int Id, string Name, string Phone, Location location);
        void AddDrone(int _Id, string _Model, int _MaxWeight, int _StationId);
        void AddParcel(int Id, int SenderId, int TargetId, int Weight, int Priority);
        void AddStation(int Id, string Name, Location location, int ChargeSlots);
        void ChargeDrone(int _Id);
        void DeliverParcel(int DroneId);
        Customer DisplayCustomer(int Id);
        IEnumerable<CustomerForList> DisplayCustomers();
        Drone DisplayDrone(int Id);
        public IEnumerable<DroneForList> DisplayDrones(DroneStatuses? DS, WeightCategories? DC)
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
        public IEnumerable<ParcelToList> GetFilterdParcels(Customer Customer, DateTime? startDate, DateTime? endDate, Priorities? Priority, WeightCategories? Weight, ParcelStatus? Status);

    }
}