using System;
using IDAL.DO;

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
        enum Updates { 
            LinkDrone = 1,
            PickUp,
            DropOff,
            Charge,
            Uncharge
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

        static DalObject.DalObject db = new DalObject.DalObject();

        /*              add functions             */
        //input output Add functions
        static void AddDrone()
        {
            Console.WriteLine("enter ID:"); 
            int _Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Model:"); 
            string _Model = Console.ReadLine();
            Console.WriteLine("enter Max weight (1,2,3):"); 
            IDAL.DO.WeightCategories _MaxWeight = (WeightCategories)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Drone Status (1 for available, 2 for maintenance and 3 for delivery):");
            DroneStatuses _Status = (DroneStatuses)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter battery power (1-100):");
            double _Battery = Convert.ToDouble(Console.ReadLine());
            db.AddDrone(_Id,_Model,_MaxWeight,_Status,_Battery);
        }
        static void AddStation()
        {
            Console.WriteLine("enter ID:"); 
            int _Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Name:"); 
            string _Name = Console.ReadLine();
            Console.WriteLine("enter Longitude cordinate:");
            double _Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("enter Lattitude cordinate:");
            double _Lattitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("enter ammount of free Charging slots:");
            int _ChargeSlots = Convert.ToInt32(Console.ReadLine());
            db.AddStation(_Id,_Name,_Longitude,_Lattitude,_ChargeSlots);
        }
        static void AddCustomer()
        {
            Console.WriteLine("enter ID:"); 
            int _Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Name:"); 
            string _Name = Console.ReadLine();
            Console.WriteLine("enter Phone Number:");
            string _Phone = Console.ReadLine();
            Console.WriteLine("enter Longitude cordinate:");
            double _Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("enter Lattitude cordinate:");
            double _Lattitude = Convert.ToDouble(Console.ReadLine());
            db.AddCustomer(_Id,_Name,_Phone,_Longitude,_Lattitude);
        }
        static void AddParcel()
        {
            Console.WriteLine("enter parcel ID:"); 
            int _Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Sender ID:"); 
            int _SenderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter Target ID"); 
            int _TargetId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter weight (1,2,3):"); 
            WeightCategories _MaxWeight = (WeightCategories)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter priority Level (1,2,3):");
            Priorities _Priority = (Priorities)Convert.ToInt32(Console.ReadLine());
            db.AddParcel(_Id, _SenderId, _TargetId, _MaxWeight, _Priority, DateTime.Now, null, null, null, null);
        }

        /*              update functions             */
        //link Drone to parcel
        static void LinkDrone()
        {
            Console.WriteLine("Enter drone ID:");
            int DroneId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter parcel ID:");
            int ParcelId = Convert.ToInt32(Console.ReadLine());
            db.LinkParcel(DroneId, ParcelId);
        }

        //pick up parcel
        static void PickUp()
        {
            Console.WriteLine("Enter parcel ID:");
            int ParcelId = Convert.ToInt32(Console.ReadLine());
            db.DronePickUp(ParcelId);
        }

        //Drop Off parcel
        static void DropOff()
        {
            Console.WriteLine("Enter parcel ID:");
            int ParcelId = Convert.ToInt32(Console.ReadLine());
            db.DroneDropOff(ParcelId);
        }

        //Charge Drone
        static void Charge()
        {
            Console.WriteLine("Enter drone ID:");
            int DroneId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter station ID:");
            int StationId = Convert.ToInt32(Console.ReadLine());
            db.ChargeDrone(DroneId,StationId);
        }
        //uncharge Drone
        static void UnCharge()
        {
            Console.WriteLine("Enter drone ID:");
            int DroneId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter station ID:");
            int StationId = Convert.ToInt32(Console.ReadLine());
            db.UnChargeDrone(DroneId, StationId);
        }

        /*              display functions             */
        static void DisplayDrone()
        {
            Console.WriteLine("Enter Drone Id");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(db.GetDrone(Id));
        }
        static void DisplayStation()
        {
            Console.WriteLine("Enter Station Id");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(db.GetStation(Id));
        }
        static void DisplayCustomer()
        {
            Console.WriteLine("Enter Customer Id");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(db.GetCustomer(Id));
        }
        static void DisplayParcel()
        {
            Console.WriteLine("Enter Parcel Id");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(db.GetParcel(Id));
        }
        static void DisplayDrones()
        {
            db.GetDrones().ForEach(name => Console.WriteLine(name));
        }
        static void DisplayStations()
        {
            db.GetStations().ForEach(name => Console.WriteLine(name));
        }
        static void DisplayCustomers()
        {
            db.GetCustomers().ForEach( name => Console.WriteLine(name));
        }
        static void DisplayParcels()
        {
            db.GetParcels().ForEach(name => Console.WriteLine(name));
        }
        static void DisplayFreeStations()
        { 
            db.GetFreeStations().ForEach(name => Console.WriteLine(name));
        }
        static void DisplayFreeParcels()
        {
            db.GetFreeParcels().ForEach(name => Console.WriteLine(name));
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
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch(choice)
                        {
                            case (int)AddChoices.Station:
                                AddStation();
                                break;
                            case (int)AddChoices.Drone:
                                AddDrone();
                                break;
                            case (int)AddChoices.Customer:
                                AddCustomer();
                                break;
                            case (int)AddChoices.Parcel:
                                AddParcel();
                                break;
                            default:
                                Console.WriteLine("invalid input try again");
                                break;
                        }
                        break;
                    //update case
                    case (int)Choice.Update:
                        Console.WriteLine("Options: Link drone to parcel press 1, to pick up parcel press 2, to deliver parcel Press 3 to charge drone press 4 to remove drone from charging press 5");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch(choice)
                        {
                            case (int)Updates.LinkDrone:
                                LinkDrone();
                                break;
                            case (int)Updates.PickUp:
                                PickUp();
                                break;
                            case (int)Updates.DropOff:
                                DropOff();
                                break;
                            case (int)Updates.Charge:
                                Charge();
                                break;
                            case (int)Updates.Uncharge:
                                UnCharge();
                                break;
                            default:
                                Console.WriteLine("invalid input try again");
                                break;
                        }
                        break;
                    //display by id case
                    case (int)Choice.Display:
                        Console.WriteLine("Options: Display station press 1, Display Drone press 2, Display customer Press 3 to Display Parcel press 4");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch(choice)
                        {
                            case (int)Display.Station:
                                DisplayStation();
                                break;
                            case (int)Display.Drone:
                                DisplayDrone();
                                break;
                            case (int)Display.Customer:
                                DisplayCustomer();
                                break;
                            case (int)Display.Parcel:
                                DisplayParcel();
                                break;
                            default:
                                Console.WriteLine("invalid input try again");
                                break;
                        }
                        break;
                    //display all of certain type case
                    case (int)Choice.DisplayAll:
                        Console.WriteLine("Options: Display stations press 1, Display Drones press 2, Display customers Press 3 to Display Parcels press 4 to display free parcels press 5 to display free stations press 6:");
                        choice = Convert.ToInt32(Console.ReadLine());
                        switch (choice)
                        {
                            case (int)DisplayAll.Stations:
                                DisplayStations();
                                break;
                            case (int)DisplayAll.Drones:
                                DisplayDrones();
                                break;
                            case (int)DisplayAll.Customers:
                                DisplayCustomers();
                                break;
                            case (int)DisplayAll.Parcels:
                                DisplayParcels();
                                break;
                            case (int)DisplayAll.FreeParcels:
                                DisplayFreeParcels();
                                break;
                            case (int)DisplayAll.FreeStations:
                                DisplayFreeStations();
                                break;
                            default:
                                Console.WriteLine("invalid input try again");
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

            
        }
    }
}
