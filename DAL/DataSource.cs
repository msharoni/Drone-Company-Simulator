using IDAL.DO;
namespace DalObject{
    class DalObject{
        //adding each type using List.Add
        public void AddDrone(int Id, string Model, WeightCategories MaxWeight, DroneStatuses Status, double Battery){
            DataSource.Drones.Add(new Drone(Id,Model,MaxWeight,Status,Battery));
        }
        public void AddStation(int Id, string Name, double Longitude, double Lattitude, int ChargeSlots){
            DataSource.Stations.Add(new Station(Id,Name,Longitude,Lattitude,ChargeSlots));
        }
        public void AddCustomer(int Id, string Name, string Phone, double Longitude, double Lattitude){
            DataSource.Customers.Add(new Station(Id,Name,Phone,Longitude,Lattitude));
        }
        public void AddParcel(int Id, int SenderId, int TargetId, WeightCategories Weight, Priorities Priority, DateTime Requested, int DroneId, DateTime Scheduled, DateTime PickedUp, DateTime Delivered){
            DataSource.Parcels.Add(new Parcel(Id,TargetId,Weight,Priority,Requested,DroneId,Scheduled,PickedUp,Delivered));
        }

        //Linking the parcel to a drone
        public void LinkParcel(int DroneId, int ParcelId)
        {
            //finding the relevant drone/parcel
            Drone drone = DataSource.Drones.find(drone=> drone.Id == DroneId);
            Parcel parcel = DataSource.Parcels.find(parcel => parcel.Id == ParcelId);
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Pindex = DataSource.Parcels.IndexOf(parcel);
            //making the needed changes
            drone.Status = DroneStatuses.Delivery;
            parcel.DroneId = DroneId;
            parcel.Scheduled = Date.Now;
            //copying them back into the List
            DataSource.Parcels[Pindex] = parcel;
            DataSource.Drones[Dindex] = drone;
        }
        public void DronePickUp(int ParcelId)
        {
            //finding the relevant drone/parcel
            Parcel parcel = DataSource.Parcels.find(parcel => parcel.Id == ParcelId);
            Drone drone = DataSource.Drones.find(drone=> drone.Id == parcel.DroneId);
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
            Parcel parcel = DataSource.Parcels.find(parcel => parcel.Id == ParcelId);
            Drone drone = DataSource.Drones.find(drone=> drone.Id == parcel.DroneId);
            //saving their index for later
            int Pindex = DataSource.Parcels.IndexOf(parcel);
            //making the needed changes
            parcel.Delivered = DateTime.Now;
             //copying them back into the list
            DataSource.Parcels[Pindex] = parcel;
        }
        public void ChargeDrone(int DroneId, int StationId)
        {
            //finding the relevant drone/station
            Drone drone = DataSource.Drones.find(drone => drone.Id == DroneId);
            Station station = DataSource.Stations.find(station => station.Id == StationId);
            //saving their index for later
            int Dindex = DataSource.Drones.IndexOf(drone);
            int Sindex = DataSource.Stations.IndexOf(station);
            //making the needed changes
            station.ChargeSlots -= 1;
            drone.Status = DroneStatuses.maintenance;
            DataSource.ChargingDrones.Add(new ChargeDrone(DroneId,StationId));
             //copying them back into the list    
            DataSource.Drones[Dindex] = drone;
            DataSource.Starions[Sindex] = station;            
        }
        public void ChargeDrone(int DroneId, int StationId)
        {
            //finding the relevant drone/station/ChargingDrone
            Drone drone = DataSource.Drones.find(drone => drone.Id == DroneId);
            Station station = DataSource.Stations.find(station => station.Id == StationId);
            ChargingDrones charging = DataSource.ChargingDrones.find(charging => charging.DroneId == DroneId);
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
            DataSource.Starions[Sindex] = station;            
        }
        public Drone GetDrone(DroneId){
            return DataSource.Drones.find(drone=> drone.Id == DroneId);
        }
        public Parcel GetParcel(ParcelId){
            return DataSource.Parcels.find(parcel=> parcel.Id == ParcelId);

        }
        public Customer GetCustomer(CustomerId){
            return DataSource.Customers.find(customer => customer.Id == CustomerId);
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
            return DataSource.Parcels.FindAll(parcel => parcel.DroneId != 0);
        }
        public List<Station> GetFreeStations(){
            return DataSource.Stations.FindAll(station => station.ChargeSlots != 0);
        }
        

    }

    class DataSource{ 
            internal static List<Drone> Drones;
            internal static List<Station> Stations;
            internal static List<Customer> Customers;
            internal static List<Parcel> Parcels;
            internal static List<DroneCharge> ChargingDrones;
            static void Intalize(){
                    Random r = new Random();
                    int[] ids = {,r.Next(9999999,100000000),r.Next(9999999,100000000)};
                    string[] Names = { "Rufus", "Bear", "Dakota", "Fido","Vanya", "Samuel", "Koani", "Volodya", "Prince", "Yiska" };
                    int IndexStation = r.Next(Names.Length());
                    int IndexCustomer1 = r.Next(Names.Length());
                    int IndexCustomer2 = r.Next(Names.Length());
                    int WeightLevel = r.Next(3);
                    for(int i=0; i<5; ++i)
                         Drones.Add(new Drone {r.Next(1000), Model= "Model"+(string)r.Next(100), MaxWeight = (WeightCategories)r.Next(3)  , Status= (DroneStatuses)r.Next(3), Battery = r.Next(101)});
                    for(int i=0; i<2; ++i)
                        Stations.Add(new Station {Id = r.Next(1000), Name= "Station"+(string)r.Next(100), Longitude= r.Next(-180,181), Lattitude= r.Next(-90,91) , ChargeSlots = r.Next(100)};)
                    for(int i=0; i<10; ++i)
                        Customers.Add(new Customer {Id = r.Next(99999990,1000000000), Name= Names[r.Next(Names.Length())], Phone= (string)r.Next(999999999,10000000000), Longitude= r.Next(-180,181), Lattitude= r.Next(-90,91)});
                    for(int i=0; i<10; ++i)
                        Parcels.Add(new Parcel {Id = r.Next(99999999,1000000000), SenderId = r.Next(99999999,1000000000) , TargetId = r.Next(99999999,1000000000),Weight = (WeightCategories)r.Next(3),Priority = (Priorities)r.Next(3), Requested = DateTime.Now, DroneId = r.Next(100), Scheduled = new DateTime(), PickedUp= new DateTime(),Delivered = new DateTime() });
                }
            internal class Config{

                
                

            }
        }

        
}