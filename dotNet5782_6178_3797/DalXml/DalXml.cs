using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using System.IO;
using System;
using System.Xml.Linq;

namespace Dal
{
    sealed class DalXml:IDal
    {
        static readonly IDal instance = new DalXml();
        public static IDal Instance { get => instance; }
        DalXml() {}

        //gets for each list from xml file
        public IEnumerable<DO.Customer> GetCustomers()
        {
            XElement CustomerData = XElement.Load(@"Data\Customers.xml");
            return from Customer in CustomerData.Elements()
                   select new DO.Customer()
                   {
                       Id = int.Parse(Customer.Element("Id").Value),
                       Lattitude = double.Parse(Customer.Element("Lattitude").Value),
                       Longitude = double.Parse(Customer.Element("Longitude").Value),
                       Name = Customer.Element("Name").Value,
                       Phone = Customer.Element("Phone").Value
                   };
        }
        public IEnumerable<DO.Drone> GetDrones()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Drone>));
            XmlReader reader = new XmlTextReader(@"Data\Drones.xml");
            List<DO.Drone> data = (List<DO.Drone>)ser.Deserialize(reader);
            reader.Close();
            return data;
        }
        public IEnumerable<DO.Station> GetStations()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Station>));
            XmlReader reader = new XmlTextReader(@"Data\Stations.xml");
            List<DO.Station> data = (List<DO.Station>)ser.Deserialize(reader);
            reader.Close();
            return data;
        }
        public IEnumerable<DO.Parcel> GetParcels()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\Parcels.xml");
            List<DO.Parcel> data = (List<DO.Parcel>)ser.Deserialize(reader);
            reader.Close();
            return data;
        }
        public IEnumerable<DO.DroneCharge> GetChargingDrones()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Parcel>));
            XmlReader reader = new XmlTextReader(@"Data\ChargingDrones.xml");
            List<DO.DroneCharge> data = (List<DO.DroneCharge>)ser.Deserialize(reader);
            reader.Close();
            return data;
        }
        public IEnumerable<DO.Parcel> GetFreeParcels()
        {
            return GetParcels().Where(parcel => parcel.DroneId == -1);
        }
        public IEnumerable<DO.Station> GetVacantStations()
        {
            return GetStations().Where(station => station.ChargeSlots > 0);
        }
        //gets for single type
        public DO.Customer GetCustomer(int Id)
        {
            
            XElement CustomerData = XElement.Load(@"Data\Customers.xml");
            try{
                return (from Customer in CustomerData.Elements()
                        where int.Parse(Customer.Element("Id").Value) == Id
                        select new DO.Customer()
                        {
                            Id = int.Parse(Customer.Element("Id").Value),
                            Lattitude = double.Parse(Customer.Element("Lattitude").Value),
                            Longitude = double.Parse(Customer.Element("Longitude").Value),
                            Name = Customer.Element("Name").Value,
                            Phone = Customer.Element("Phone").Value
                        }).First();
            }
            catch(InvalidOperationException)
            {
                throw new DO.IdNotExistException(Id);
            }
        }
        public DO.Drone GetDrone(int Id)
        {
            try
            {
                return GetDrones().Where(drone => drone.Id == Id).First();
            }
            catch (InvalidOperationException)
            {
                throw new DO.IdNotExistException(Id);
            }
        }
        public DO.Station GetStation(int Id)
        {
            try
            {
                return GetStations().Where(station => station.Id == Id).First();
            }
            catch(InvalidOperationException)
            {
                throw new DO.IdNotExistException(Id);
            }
        }
        public DO.Parcel GetParcel(int Id)
        {
            try
            {
                return GetParcels().ToList().Find(parcel => parcel.Id == Id);
            }
            catch(InvalidOperationException)
            {
                throw new DO.IdNotExistException(Id);
            }
        }
        public IEnumerable<DO.Parcel> GetFilteredParcels(Predicate<DO.Parcel> filter)
        {
            return GetParcels().Where(p => filter(p));
        }
        //add functions
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude)
        {
            IEnumerable<DO.Customer> Customers = GetCustomers();
            if (Customers.Any(customer => customer.Id == Id) == true)
                throw new DO.IdExcistsException(Id);
            XElement CustomerData = XElement.Load(@"Data\Customers.xml");
            CustomerData.Nodes().Append(new XElement("Customer", new XElement[] 
            { 
                new XElement("Id", "" + Id),
                new XElement("Name", Name),
                new XElement("Phone", Phone),
                new XElement("Longitude", ""+Longitude),
                new XElement("Lattitude", ""+Lattitude)
            }));
        }
        public void AddDrone(int Id, string Model, DO.WeightCategories MaxWeight)
        {
            IEnumerable<DO.Drone> Drones = GetDrones();
            if (Drones.Any(drone => drone.Id == Id) == true)
                throw new DO.IdExcistsException(Id);
            DO.Drone drone = new DO.Drone() { Id = Id, Model = Model, MaxWeight = MaxWeight };
            Drones.Append(drone);
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Drone>));
            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            ser.Serialize(writer, Drones);
            writer.Close();
        }
        public void AddParcel(int Id, int SenderId, int TargetId, DO.WeightCategories Weight, DO.Priorities Priority, DateTime? Requested, int? DroneId, DateTime? Scheduled, DateTime? PickedUp, DateTime? Delivered)
        {
            IEnumerable<DO.Parcel> Parcels = GetParcels();
            if (Parcels.Any(parcel => parcel.Id == Id) == true)
                throw new DO.IdExcistsException(Id);
            DO.Parcel parcel = new DO.Parcel() { Id = Id, SenderId = SenderId, TargetId = TargetId, Weight = Weight, Priority = Priority,
                Requested = Requested, Scheduled = Scheduled, PickedUp = PickedUp, Delivered = Delivered };
            Parcels.Append(parcel);
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Parcel>));
            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            ser.Serialize(writer, Parcels);
            writer.Close();
        }
        public void AddStation(int Id, string Name, double Longitude, double Lattitude, int ChargeSlots)
        {
            IEnumerable<DO.Station> Stations = GetStations();
            if (Stations.Any(station => station.Id == Id) == true)
                throw new DO.IdExcistsException(Id);
            DO.Station station = new DO.Station() { Id = Id, Name = Name, Longitude = Longitude, Lattitude = Lattitude, ChargeSlots = ChargeSlots };
            Stations.Append(station);
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Station>));
            TextWriter writer = new StreamWriter(@"Data\Stations.xml");
            ser.Serialize(writer, Stations);
            writer.Close();

        }
        public void ChargeDrone(int DroneId, int StationId)
        {
            //adding a new charging drone to the file/list
            DO.DroneCharge drone = new DO.DroneCharge() { DroneId = DroneId, StationId = StationId };
            IEnumerable<DO.DroneCharge> Drones = GetChargingDrones();
            Drones.Append(drone);
            XmlSerializer ChargingDroneSer = new XmlSerializer(typeof(List<DO.DroneCharge>));
            TextWriter ChargingDroneWriter = new StreamWriter(@"Data\ChargingDrones.xml");
            ChargingDroneSer.Serialize(ChargingDroneWriter, Drones);
            ChargingDroneWriter.Close();
            //subtracting 1 from the stations charge slots
            DO.Station station = GetStation(StationId);
            station.ChargeSlots -= 1;
            List<DO.Station> Stations = GetStations().ToList();
            Stations[Stations.FindIndex(station => station.Id == StationId)] = station;
            XmlSerializer StationSer = new XmlSerializer(typeof(List<DO.Station>));
            TextWriter StationWriter = new StreamWriter(@"Data\Stations.xml");
            StationSer.Serialize(StationWriter, Stations);
            StationWriter.Close();
        }
        public void DroneDropOff(int ParcelId)
        {

            //checking if parcel excists
            if (GetParcels().ToList().FindIndex(parcel => parcel.Id == ParcelId) == -1)
                throw new DO.IdNotExistException(ParcelId);
            List<DO.Parcel> Parcels = GetParcels().ToList();
            //copying the parcel 
            DO.Parcel parcel = GetParcel(ParcelId);
            //making the changes
            parcel.Delivered = DateTime.Now;
            //copying it back
            Parcels[Parcels.FindIndex(parcel => parcel.Id == ParcelId)] = parcel;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Parcel>));
            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            ser.Serialize(writer, Parcels);
            writer.Close();
        }
        public void DronePickUp(int ParcelId)
        {
            
            //checking if parcel excists
            if (GetParcels().ToList().FindIndex(parcel => parcel.Id == ParcelId) == -1)
                throw new DO.IdNotExistException(ParcelId);
            List<DO.Parcel> Parcels = GetParcels().ToList();
            //copying the parcel 
            DO.Parcel parcel = GetParcel(ParcelId);
            //making the changes
            parcel.PickedUp = DateTime.Now;
            //copying it back
            Parcels[Parcels.FindIndex(parcel => parcel.Id == ParcelId)] = parcel;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Parcel>));
            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            ser.Serialize(writer, Parcels);
            writer.Close();
        }
        public void LinkParcel(int DroneId, int ParcelId)
        {
            //checking if they excist
            if (GetDrones().ToList().FindIndex(drone => drone.Id == DroneId) == -1)
                throw new DO.IdNotExistException(DroneId);
            if (GetDrones().ToList().FindIndex(parcel => parcel.Id == ParcelId) == -1)
                throw new DO.IdNotExistException(ParcelId);
            List<DO.Parcel> Parcels = GetParcels().ToList();
            //copying the parcel 
            DO.Parcel parcel = GetParcel(ParcelId);
            //making the changes
            parcel.DroneId = DroneId;
            parcel.Scheduled = DateTime.Now;
            //copying it back
            Parcels[Parcels.FindIndex(parcel => parcel.Id == ParcelId)] = parcel;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Parcel>));
            TextWriter writer = new StreamWriter(@"Data\Parcels.xml");
            ser.Serialize(writer, Parcels);
            writer.Close();
        }
        public void UnChargeDrone(int DroneId, int StationId)
        {
            //adding a new charging drone to the file/list
            DO.DroneCharge drone = new DO.DroneCharge() { DroneId = DroneId, StationId = StationId };
            List<DO.DroneCharge> Drones = GetChargingDrones().ToList();
            Drones.Remove(drone);
            XmlSerializer ChargingDroneSer = new XmlSerializer(typeof(List<DO.DroneCharge>));
            TextWriter ChargingDroneWriter = new StreamWriter(@"Data\ChargingDrones.xml");
            ChargingDroneSer.Serialize(ChargingDroneWriter, Drones);
            ChargingDroneWriter.Close();

            //check what to do about drone status

            //subtracting 1 from the stations charge slots
            DO.Station station = GetStation(StationId);
            station.ChargeSlots += 1;
            List<DO.Station> Stations = GetStations().ToList();
            Stations[Stations.FindIndex(station => station.Id == StationId)] = station;
            XmlSerializer StationSer = new XmlSerializer(typeof(List<DO.Station>));
            TextWriter StationWriter = new StreamWriter(@"Data\Stations.xml");
            StationSer.Serialize(StationWriter, Stations);
            StationWriter.Close();
        }
        public void UpdateDrone(int _Id, string _Model)
        {
            DO.Drone drone = GetDrone(_Id);
            drone.Model = _Model;
            List<DO.Drone> Drones = GetDrones().ToList();
            Drones[Drones.FindIndex(drone => drone.Id == _Id)] = drone;
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Drone>));
            TextWriter writer = new StreamWriter(@"Data\Drones.xml");
            ser.Serialize(writer, Drones);
            writer.Close();
        }
        public void UpdateCustomer(int Id, string? name, string? Phone)
        {
            XElement CustomerData = XElement.Load(@"Data\Customers.xml");
            IEnumerable<DO.Customer> Customers = GetCustomers();
            from Customer in CustomerData.Elements() where int.Parse(Customer.Element("Id").Value) == Id
            select new DO.Customer()
            {
                Id = int.Parse(Customer.Element("Id").Value),
                Lattitude = double.Parse(Customer.Element("Lattitude").Value),
                Longitude = double.Parse(Customer.Element("Longitude").Value),
                Name = Customer.Element("Name").Value,
                Phone = Customer.Element("Phone").Value
            });
            CustomerData.Nodes().Append(new XElement("Customer", new XElement[]
            {
                new XElement("Id", "" + Id),
                new XElement("Name", name),
                new XElement("Phone", Phone),
                new XElement("Longitude", ""+Longitude),
                new XElement("Lattitude", ""+Lattitude)
            }));
        }
        public void UpdateParcel(DO.Parcel _parcel)
        {
            List<DO.Parcel> Parcels = GetParcels().ToList();
            Parcels[Parcels.FindIndex(Parcel => Parcel.Id == _parcel.Id)] = _parcel;
            XmlSerializer ParcelSer = new XmlSerializer(typeof(List<DO.Parcel>));
            TextWriter ParcelWriter = new StreamWriter(@"Data\Parcels.xml");
            ParcelSer.Serialize(ParcelWriter, Parcels);
            ParcelWriter.Close();
        }
        public void UpdateStation(int Id, string? name, int? NumOfSlots)
        {
            DO.Station station = GetStation(Id);
            if (name != null)
                station.Name = name;
            if (NumOfSlots != null)
                station.ChargeSlots = (int)NumOfSlots;
            List<DO.Station> Stations = GetStations().ToList();
            Stations[Stations.FindIndex(station => station.Id == Id)] = station;
            XmlSerializer StationSer = new XmlSerializer(typeof(List<DO.Station>));
            TextWriter StationWriter = new StreamWriter(@"Data\Stations.xml");
            StationSer.Serialize(StationWriter, Stations);
            StationWriter.Close();
        }
        public double[] GetBatteryUsage()
        {
            XElement dalConfig = XElement.Load(@"Data\dal-config.xml");
            double[] BatteryUsage = new double[5];
            BatteryUsage[0] = double.Parse(dalConfig.Element("charge").Value);
            BatteryUsage[1] = double.Parse(dalConfig.Element("none").Value);
            BatteryUsage[2] = double.Parse(dalConfig.Element("light").Value);
            BatteryUsage[3] = double.Parse(dalConfig.Element("medium").Value);
            BatteryUsage[4] = double.Parse(dalConfig.Element("heavy").Value);
            return BatteryUsage;
        }
        internal int ParcelIndex()
        {
            XElement dalConfig = XElement.Load(@"Data\dal-config.xml");
            int ParcelId = int.Parse(dalConfig.Element("ParcelID").Value);
            dalConfig.Element("ParcelID").SetValue(ParcelId + 1);
            dalConfig.Save(@"Data\dal-config.xml");
            return ParcelId;
        }
    }
}