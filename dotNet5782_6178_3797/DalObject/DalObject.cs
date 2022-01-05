﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using DalApi;
using DO;

namespace Dal
{
    sealed class DalObject : DalApi.IDal
    {
        static DalObject() {}
        static IDal instance;
        static readonly object padlock = new object();
        public static IDal Instance {
            get { 
                if(instance == null)
                    lock(padlock)
                    {
                        if (instance == null)
                            instance = new DalObject();
                    }
                return instance;
            }
        }
        DalObject()
        {
            DataSource.Intalize();
        }
        //adding each type using List.Add
        public void AddDrone(int Id, string Model, WeightCategories MaxWeight)
        {
            if (DataSource.Drones.FindIndex(drone => drone.Id == Id) != -1)
                throw new IdExcistsException(Id);
            DataSource.Drones.Add(new Drone(Id, Model, MaxWeight));
        }
        public void AddStation(int Id, string Name, double Longitude, double Lattitude, int ChargeSlots)
        {
            if (DataSource.Stations.FindIndex(station => station.Id == Id) != -1)
                throw new IdExcistsException(Id);
            DataSource.Stations.Add(new Station(Id, Name, Longitude, Lattitude, ChargeSlots));
        }
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude)
        {
            if (DataSource.Customers.FindIndex(customer => customer.Id == Id) != -1)
                throw new IdExcistsException(Id);
            DataSource.Customers.Add(new Customer(Id, Name, Phone, Longitude, Lattitude));
        }
        public void AddParcel(int Id, int SenderId, int TargetId, WeightCategories Weight, Priorities Priority, DateTime? Requested, int? DroneId, DateTime? Scheduled, DateTime? PickedUp, DateTime? Delivered)
        {
            if (DataSource.Parcels.FindIndex(parcel => parcel.Id == Id) != -1)
                throw new IdExcistsException(Id);
            DataSource.Parcels.Add(new Parcel(Id, SenderId, TargetId, Weight, Priority, Requested, DroneId, Scheduled, PickedUp, Delivered));
        }

        //Linking the parcel to a drone
        public void LinkParcel(int DroneId, int ParcelId)
        {
            //finding the relevant drone/parcel
            if (DataSource.Drones.FindIndex(drone => drone.Id == DroneId) == -1)
                throw new IdNotExistException(DroneId);
            Drone drone = DataSource.Drones.Find(drone => drone.Id == DroneId);
            if (DataSource.Parcels.FindIndex(parcel => parcel.Id == ParcelId) == -1)
                throw new IdNotExistException(ParcelId);
            Parcel parcel = DataSource.Parcels.Find(parcel => parcel.Id == ParcelId);
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Pindex = DataSource.Parcels.IndexOf(parcel);
            //making the needed changes
            parcel.DroneId = DroneId;
            parcel.Scheduled = DateTime.Now;
            //copying them back into the List
            DataSource.Parcels[Pindex] = parcel;
            DataSource.Drones[Dindex] = drone;
        }
        public void DronePickUp(int ParcelId)
        {
            //finding the relevant drone/parcel
            if (DataSource.Parcels.FindIndex(parcel => parcel.Id == ParcelId) == -1)
                throw new IdNotExistException(ParcelId);
            Parcel parcel = DataSource.Parcels.Find(parcel => parcel.Id == ParcelId);
            if (DataSource.Drones.FindIndex(drone => parcel.Id == ParcelId) != -1)
                throw new IdNotExistException(parcel.DroneId);
            Drone drone = DataSource.Drones.Find(drone => drone.Id == parcel.DroneId);
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
            if (DataSource.Parcels.FindIndex(parcel => parcel.Id == ParcelId) == -1)
                throw new IdNotExistException(ParcelId);
            Parcel parcel = DataSource.Parcels.Find(parcel => parcel.Id == ParcelId);
            if (DataSource.Drones.FindIndex(drone => parcel.Id == ParcelId) != -1)
                throw new IdNotExistException(parcel.DroneId);
            Drone drone = DataSource.Drones.Find(drone => drone.Id == parcel.DroneId);
            //saving their index for later
            int Pindex = DataSource.Parcels.IndexOf(parcel);
            int Dindex = DataSource.Drones.IndexOf(drone);
            //making the needed changes
            parcel.Delivered = DateTime.Now;
            //copying them back into the list
            DataSource.Parcels[Pindex] = parcel;
            DataSource.Drones[Dindex] = drone;
        }
        public void ChargeDrone(int DroneId, int StationId)
        {
            //finding the relevant drone/station
            if (DataSource.Drones.FindIndex(drone => drone.Id == DroneId) == -1)
                throw new IdNotExistException(DroneId);
            Drone drone = DataSource.Drones.Find(drone => drone.Id == DroneId);
            if (DataSource.Stations.FindIndex(station => station.Id == StationId) == -1)
                throw new IdNotExistException(DroneId);
            Station station = DataSource.Stations.Find(station => station.Id == StationId);
            if (station.ChargeSlots == 0)
                throw new NoChargeSlotsException();
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Sindex = DataSource.Stations.IndexOf(station);
            //making the needed changes
            station.ChargeSlots -= 1;
            DataSource.ChargingDrones.Add(new DroneCharge(DroneId, StationId));
            //copying them back into the list    
            DataSource.Drones[Dindex] = drone;
            DataSource.Stations[Sindex] = station;
        }
        public void UnChargeDrone(int DroneId, int StationId)
        {
            //finding the relevant drone/station/ChargingDrone
            if (DataSource.Drones.FindIndex(drone => drone.Id == DroneId) == -1)
                throw new IdNotExistException(DroneId);
            Drone drone = DataSource.Drones.Find(drone => drone.Id == DroneId);
            if (DataSource.Stations.FindIndex(station => station.Id == StationId) == -1)
                throw new IdNotExistException(DroneId);
            Station station = DataSource.Stations.Find(station => station.Id == StationId);
            DroneCharge charging = DataSource.ChargingDrones.Find(charging => charging.DroneId == DroneId);
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Sindex = DataSource.Stations.IndexOf(station);
            int Cindex = DataSource.ChargingDrones.IndexOf(charging);
            //making the needed changes
            station.ChargeSlots += 1;
            DataSource.ChargingDrones.RemoveAt(Cindex);
            //copying them back into the list    
            DataSource.Drones[Dindex] = drone;
            DataSource.Stations[Sindex] = station;
        }
        public Drone GetDrone(int DroneId)
        {
            if (DataSource.Drones.FindIndex(drone => drone.Id == DroneId) == -1)
                throw new IdNotExistException(DroneId);
            return DataSource.Drones.Find(drone => drone.Id == DroneId);
        }
        public Parcel GetParcel(int ParcelId)
        {
            if (DataSource.Parcels.FindIndex(parcel => parcel.Id == ParcelId) == -1)
                throw new IdNotExistException(ParcelId);
            return DataSource.Parcels.Find(parcel => parcel.Id == ParcelId);
        }
        public Customer GetCustomer(int CustomerId)
        {
            if (DataSource.Customers.FindIndex(customer => customer.Id == CustomerId) == -1)
                throw new IdNotExistException(CustomerId);
            return DataSource.Customers.Find(customer => customer.Id == CustomerId);
        }
        public Station GetStation(int StationId)
        {
            if (DataSource.Stations.FindIndex(station => station.Id == StationId) == -1)
                throw new IdNotExistException(StationId);
            return DataSource.Stations.Find(station => station.Id == StationId);
        }

        //returning each list 
        public IEnumerable<Station> GetStations()
        {
            return DataSource.Stations;
        }
        public IEnumerable<Drone> GetDrones()
        {
            return DataSource.Drones;
        }
        public IEnumerable<Parcel> GetParcels()
        {
            return DataSource.Parcels;
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return DataSource.Customers;
        }
        //filtered lists
        public IEnumerable<Parcel> GetFilteredParcels(Predicate<Parcel> filter)
        {
            return DataSource.Parcels.FindAll(p => filter(p));
        }
        //making sure that the relevant fields exsist
        public IEnumerable<Parcel> GetFreeParcels()
        {
            return DataSource.Parcels.FindAll(parcel => parcel.DroneId == 0);
        }
        public IEnumerable<Station> GetVacantStations()
        {
            return DataSource.Stations.FindAll(station => station.ChargeSlots != 0);
        }
        public IEnumerable<DroneCharge> GetChargingDrones()
        {
            return DataSource.ChargingDrones;
        }
        public void UpdateDrone(int _Id, string _Model)
        {
            int index = DataSource.Drones.FindIndex(drone => drone.Id == _Id);
            if (index == -1)
                throw new IdNotExistException(_Id);
            Drone drone = DataSource.Drones[index];
            drone.Model = _Model;
            DataSource.Drones[index] = drone;
        }
        public void UpdateCustomer(int Id, string? name, string? Phone)
        {
            int index = DataSource.Customers.FindIndex(customer => customer.Id == Id);
            Customer customer = DataSource.Customers[index];
            if (name != null)
                customer.Name = name;
            if (Phone != null)
                customer.Phone = Phone;
            DataSource.Customers[index] = customer;

        }
        public void UpdateParcel(Parcel _parcel)
        {
            int index = DataSource.Parcels.FindIndex(parcel => parcel.Id == _parcel.Id);
            DataSource.Parcels[index] = _parcel;
        }
        public void UpdateStation(int Id, string? name, int? NumOfSlots)
        {
            int index = DataSource.Stations.FindIndex(station => station.Id == Id);
            Station station = DataSource.Stations[index];
            if (name != null)
                station.Name = name;
            if (NumOfSlots != null)
                station.ChargeSlots = (int)NumOfSlots;
            DataSource.Stations[index] = station;
        }
        public double[] GetBatteryUsage()
        {
            return DataSource.Config.batteryPerKm;
        }
    }
}