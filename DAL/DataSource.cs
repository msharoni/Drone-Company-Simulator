namespace DalObject{

    class DataSource{ 
            internal static Drone[] Drones;
            internal static Station[] Stations;
            internal static Customer[] Customers;
            internal static Parcel[] Parcels;

            internal class Config{
                static void Intalize(){
                    Random r = new Random();
                    int[] ids = {r.Next(9999999,100000000),r.Next(9999999,100000000),r.Next(9999999,100000000)};
                    string[] Names = { "Rufus", "Bear", "Dakota", "Fido","Vanya", "Samuel", "Koani", "Volodya", "Prince", "Yiska" };
                    int IndexStation = r.Next(Names.Length());
                    int IndexCustomer1 = r.Next(Names.Length());
                    int IndexCustomer2 = r.Next(Names.Length());
                    int WeightLevel = r.Next(3);
                    Drone D = new Drone { Id =ids[0], Model= "", MaxWeight = (WeightCategories)WeightLevel  , Status= (DroneStatuses)r.Next(3), Battery = r.Next(101)};
                    Station S = new Station {Id = r.Next(9999999,100000000), Name= Names[IndexStation], Longitude= , Lattitude= , ChargeSlots = };
                    Customer C1 = new Customer {Id = ids[1]), Name= Names[IndexCustomer1], Phone= r.Next(999999999,10000000000), Longitude = , Lattitude=  };
                    Customer C2 = new Customer {Id = ids[2]), Name= Names[IndexCustomer2], Phone= r.Next(999999999,10000000000), Longitude = , Lattitude=  };
                    Parcel P = new Parcel {Id = r.Next(9999999,100000000),Senderid = ids[1] , Targetid = ids[2],Weight = (WeightCategories)r.Next(WeightLevel+1),Priority = (Priorities)r.Next(3), Requested = DateTime.Now, DroneId = ids[0], Scheduled = new DateTime(), PickedUp= new DateTime(),Delivered = new DateTime() };
                    

                }

            }
        }

        
}