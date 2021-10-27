using System;

namespace ConsoleUI
{
    class Program
    {
        enum Choice{
            Add=1,
            Update,
            Display,
            DisplayAll,
            Exit
        }
        enum AddChoices{
            Station=1,
            Drone,
            Customer,
            Parcel
        }
        enum Display{
            Station=1,
            Drone,
            Customer,
            Parcel
        }
        enum DisplayAll{
            Stations=1,
            Drones,
            Customers,
            Parcels,
            FreeParcels,
            FreeStations

        }

        DalObject.DalObject db= DalObject.DalObject;
        //input output Add functions
        //MAKE SURE THAT I AM ALLOWED TO DO THIS
        void AddStation()
        {
            Console.WriteLine("enter ID"); 
            int _Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Model"); 
            string _Model = Console.ReadLine();
            Console.WriteLine("enter Max weight (1,2,3):"); 
            WeightCategories _MaxWeight = (WeightCategories)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Drone Status (1 for available, 2 for maintenance and 3 for delivery):");
            DroneStatuses _Status = (DroneStatuses)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter battery power (1-100):");
            double _Battery = Convert.ToDouble(Console.ReadLine());
            //probally dosent work like this
            db.AddStation(_Id,_Model,_MaxWeight,_Status,_Battery);
        }



        static void Main(string[] args)
        {
            bool run = true;
            while(run){
                Console.WriteLine("Options: Add press 1, Update press 2, Display Press 3, Display-All press 4 to Exit press 5");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch(choice)
                {
                    //add case
                    case (int)Choice.Add:
                        Console.WriteLine("Options: Add station press 1, Add Drone press 2, Add customer Press 3 to Add Parcel press 4");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch(choice)
                        {
                            case (int)AddChoices.Station:

                                break;
                            case (int)AddChoices.Drone:
                                break;
                            case (int)AddChoices.Customer:
                                break;
                            case (int)AddChoices.Parcel:
                                break;
                            default:
                                break;
                        }
                        break;
                    //update case
                    case (int)Choice.Update:
                        switch(choice)
                        {

                        }
                        break;
                    //display by id case
                    case (int)Choice.Display:
                        Console.WriteLine("Options: Display station press 1, Display Drone press 2, Display customer Press 3 to Display Parcel press 4");
                        int choice = Convert.ToInt32(Console.ReadLine());
                        switch(choice)
                        {
                            case (int)Display.Station:
                                break;
                            case (int)Display.Drone:
                                break;
                            case (int)Display.Customer:
                                break;
                            case (int)Display.Parcel:
                                break;
                            default:
                                break;
                        }
                        break;
                    //display all of certain type case
                    case (int)Choice.DisplayAll:
                        switch(choice)
                        {
                            case (int)DisplayAll.Stations:
                                break;
                            case (int)DisplayAll.Drones:
                                break;
                            case (int)DisplayAll.Customers:
                                break;
                            case (int)DisplayAll.Parcels:
                                break;
                            case (int)DisplayAll.FreeParcels:
                                break;
                            case (int)DisplayAll.FreeStations:
                                break;
                            default:
                                break;
                        }
                        break;
                    //Exit case
                    case (int)Choice.Exit:
                        run = false;
                        break;
                    //the default if any other value was entered
                    default:
                        Console.WriteLine("invalid input try again");
                        break;

                }
            }


            IDAL.DO.BaseStation baseStation = new IDAL.DO.BaseStation();
            
        }
    }
}
