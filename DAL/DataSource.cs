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

        //returning each type as a string using the overrided tostring function
        public string DisplayStation(int StationId){
            Station station = DataSource.Stations.find(station => station.Id == StationId);
            return station.ToString();
        }
        public string DisplayDrone(DroneId){
            Drone drone = DataSource.Drones.find(drone=> drone.Id == DroneId);
            return drone.ToString();

        }
        public string DisplayParcel(ParcelId){
            Parcel parcel = DataSource.Parcels.find(parcel=> parcel.Id == ParcelId);

        }
        public string DisplayCustomer(CustomerId){
            Customer customer = DataSource.Customers.find(customer => customer.Id == CustomerId);
            return customer.ToString();
        }

        //returning each list as a list of strings
        public list<string> DisplayStations(){
            list<string> Mylist;
            for(int i = 0; i < DataSource.Stations.Length(); ++i)
                Mylist.Add(DataSource.Stations[i].ToString());
            return Mylist;
        }
        public list<string> DisplayDrones(){
            list<string> Mylist;
            for(int i = 0; i < DataSource.Drones.Length(); ++i)
                Mylist.Add(DataSource.Drones[i].ToString());
            return Mylist;
        }
        public list<string> DisplayParcels(){
            list<string> Mylist;
            for(int i = 0; i < DataSource.Parcels.Length(); ++i)
                Mylist.Add(DataSource.Parcels[i].ToString());
            return Mylist;
        }
        public list<string> DisplayCustomers(){
            list<string> Mylist;
            for(int i = 0; i < DataSource.Customers.Length(); ++i)
                Mylist.Add(DataSource.Customers[i].ToString());
            return Mylist;
        }

        //
        public list<string> DisplayFreeParcels(){
            list<string> Mylist;
            for(int i = 0; i < DataSource.Parcels.Length(); ++i)
                if(DataSource.Parcels[i].DroneId == "I DONT KNOW YET")    
                    Mylist.Add(DataSource.Parcels[i].ToString());
            return Mylist;
        }
        public list<string> DisplayFreeStations(){
            list<string> Mylist;
            for(int i = 0; i < DataSource.Stations.Length(); ++i)
                if(DataSource.Stations[i].ChargeSlots == "I DONT KNOW YET")
                    Mylist.Add(DataSource.Stations[i].ToString());
            return Mylist;
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