using System.Collections.Generic;

namespace BL
{
    public interface IBL
    {
        void AddCustomer();
        void AddDrone(int _Id, string _Model, int _MaxWeight, int _StationId);
        void AddParcel();
        void AddStation();
        void ChargeDrone(int _Id);
        void DeliverParcel(int DroneId);
        Customer DisplayCustomer(int Id);
        List<CustomerForList> DisplayCustomers();
        Drone DisplayDrone(int Id);
        List<DroneForList> DisplayDrones();
        List<ParcelForList> DisplayFreeParcels();
        List<StationForList> DisplayFreeStations();
        Parcel DisplayParcel(int Id);
        List<ParcelForList> DisplayParcels();
        Station DisplayStation(int Id);
        List<StationForList> DisplayStations();
        void LinkDroneToParcel(int DroneId);
        void PickUpParcel(int DroneId);
        void UnChargeDrone(int Id, double time);
        void UpdateCustomer(int Id, string name, string Phone);
        void UpdateDrone(int _Id, string _Model);
        void UpdateStation(int Id, string name, int? NumOfSlots);
    }
}