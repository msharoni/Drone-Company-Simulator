using IDAL.DO;
namespace DalObject{
    class DalObject{
        //adding each type using list.Add
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
            //copying them back into the list
            DataSource.Parcels[Pindex] = parcel;
            DataSource.Drones[Dindex] = drone;
        }
        public void 

        //
        //returning each type by their id
        public Station GetStation(int StationId){
            return DataSource.Stations.find(station => station.Id == StationId);

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
        public list<Station> GetStations(){
            return DataSource.Stations;
        }
        public list<Drone> GetDrones(){
            return DataSource.Drones;
        }
        public list<Parcel> GetParcels(){
            return DataSource.Parcels;
        }
        public list<Customer> GetCustomers(){
            return DataSource.Customers;
        }

        //making sure that the relevant fields exsist
        public list<Parcel> GetFreeParcels(){
            return DataSource.Parcels.FindAll(parcel => parcel.DroneId != 0);
        }
        public list<string> GetFreeStations(){
            return DataSource.Stations.FindAll(station => station.ChargeSlots != 0);
        }
        

    }

    class DataSource{ 
            internal static list<Drone> Drones;
            internal static list<Station> Stations;
            internal static list<Customer> Customers;
            internal static list<Parcel> Parcels;
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