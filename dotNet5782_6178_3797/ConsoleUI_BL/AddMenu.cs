/*﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleUI_BL
{
    enum AddChoices
    {
        Station = 1,
        Drone,
        Customer,
        Parcel
    }
    enum Updates
    {
        LinkDrone = 1,
        PickUp,
        DropOff,
        Charge,
        Uncharge
    }
    enum Display
    {
        Drone = 1,
        Parcel,
        Customer,
        Station ,
    }
    enum DisplayAll
    {
        Drones = 1,
        Parcels,
        Customers,
        Stations,
        FreeParcels,
        VacantStations

    }

    partial class Program
    {
        static BL.IBL b = new BL.BL();
        static public void AddMenu()
        {
            Console.WriteLine("Options: Add station press 1, Add Drone press 2, Add customer Press 3 to Add Parcel press 4");
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException) { }
            switch (choice)
            {
                case (int)AddChoices.Station:
                    {
                        Console.WriteLine("Enter Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Name: ");
                        string Name = Console.ReadLine();
                        Console.WriteLine("Enter Longitude: ");
                        double Longitude = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter Lattitude: ");
                        double Lattitude = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter amount of Charging Slots: ");
                        int ChargeSlots = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.AddStation(Id, Name, new BL.Location { Longitude = Longitude, Lattitude = Lattitude }, ChargeSlots);
                        }
                        catch (BL.IdExcistsException ex)
                        {
                            Console.WriteLine("Station " + ex);
                        }
                    }
                    break;
                case (int)AddChoices.Drone:
                    {
                        Console.WriteLine("Enter Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Model: ");
                        string Model = Console.ReadLine();
                        Console.WriteLine("Enter Max Weight 1 for Light, 2 for Medium, 3 for Heavy: ");
                        int MaxWeight = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Station Id: ");
                        int StationId = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.AddDrone(Id, Model, MaxWeight, StationId);
                        }
                        catch (BL.IdExcistsException ex)
                        {
                            Console.WriteLine("Drone " + ex);
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine("Station " + ex);
                        }
                    }
                    break;
                case (int)AddChoices.Customer:
                    {
                        Console.WriteLine("Enter Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Name: ");
                        string Name = Console.ReadLine();
                        Console.WriteLine("Enter Phone Number: ");
                        string Phone = Console.ReadLine();
                        Console.WriteLine("Enter Longitude: ");
                        double Longitude = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Enter Lattitude: ");
                        double Lattitude = Convert.ToDouble(Console.ReadLine());
                        try
                        {
                            b.AddCustomer(Id, Name, Phone, new BL.Location { Longitude = Longitude, Lattitude = Lattitude });
                        }
                        catch (BL.IdExcistsException ex)
                        {
                            Console.WriteLine("Customer " + ex);
                        }
                    }
                    break;
                case (int)AddChoices.Parcel:
                    {
                        Console.WriteLine("Enter Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Sender Id: ");
                        int SenderId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Target Id: ");
                        int TargetId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Weight 1 for Light, 2 for Medium, 3 for Heavy: ");
                        int Weight = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Priority 1 for Regular, 2 for Fast, 3 for Emergency: ");
                        int Priority = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.AddParcel(Id, SenderId, TargetId, Weight, Priority);
                        }
                        catch(BL.IdNotExistException ex)
                        {
                            Console.WriteLine("Customer" + ex);
                        }
                        catch (BL.IdExcistsException ex)
                        {
                            Console.WriteLine("Parcel" + ex);
                        }
                    }
                    break;
                default:
                    Console.WriteLine("invalid input try again");
                    break;

            }
        }
        static public void UpdateMenu()
        {
            Console.WriteLine("Options: to Link Drone to Parcel 1,to Pick-Up parcel press 2, to deliver parcel press 3, to charge drone press 4, to uncharge drone press 5");
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException) { }
            switch (choice)
            {
                case (int)Updates.LinkDrone:
                    {
                        Console.WriteLine("enter drone Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.LinkDroneToParcel(Id);
                        }
                        catch (BL.UnAvailabe ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch(BL.IdNotExistException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    break;
                case (int)Updates.PickUp:
                    {
                        Console.WriteLine("enter drone Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.PickUpParcel(Id);
                        }
                        catch (BL.NotLinkedYet ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch(BL.ParcelHasAlreadyBeenPickedUp ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    break;
                case (int)Updates.DropOff:
                    {
                        Console.WriteLine("enter drone Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.DeliverParcel(Id);
                        }
                        catch (BL.NotLinkedOrAlreadyDelivered ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    break;
                case (int)Updates.Charge:
                    {
                        Console.WriteLine("enter drone Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.ChargeDrone(Id);
                        }
                        catch(BL.UnAvailabe ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (BL.NotEnoughBattery ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    break;
                case (int)Updates.Uncharge:
                    {
                        Console.WriteLine("enter drone Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("enter how many hours drone was charging: ");
                        int time = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            b.UnChargeDrone(Id,time);
                        }
                        catch (BL.NotInMaintenance ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                    break;
                default:
                    Console.WriteLine("invalid input try again");
                    break;
            }
        }
        static public void DisplayMenu()
        {
            Console.WriteLine("Options: to Display Drone press 1,to display parcel press 2, to display customer press 3, to display station press 4");
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException) { }
            switch (choice)
            {
                case (int)Display.Drone:
                    {
                        Console.WriteLine("enter drone Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(b.DisplayDrone(Id));
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine("Drone" + ex);
                        }
                    }
                    break;
                case (int)Display.Parcel:
                    {
                        Console.WriteLine("enter Parcel Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(b.DisplayParcel(Id));
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine("Parcel" + ex);
                        }
                    }
                    break;
                case (int)Display.Customer:
                    {
                        Console.WriteLine("enter customer Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(b.DisplayCustomer(Id));
                        try
                        {
                            Console.WriteLine(b.DisplayCustomer(Id));
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine("Customer" + ex);
                        }
                    }
                    break;
                case (int)Display.Station:
                    {
                        Console.WriteLine("enter Station Id: ");
                        int Id = Convert.ToInt32(Console.ReadLine());
                        try
                        {
                            Console.WriteLine(b.DisplayStation(Id));
                        }
                        catch (BL.IdNotExistException ex)
                        {
                            Console.WriteLine("Station" + ex);
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input try again");
                    break;
            }
        }
        static public void DisplayAllMenu()
        {
            Console.WriteLine("Options: to Display all Drones press 1,to display all parcels press 2, to display all customers press 3, to display all stations press 4, do display all free parcels press 5, do display all free stations press 6");
            int choice = 0;
            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException) { }
            switch (choice)
            {
                case (int)DisplayAll.Drones:
                    {
                        foreach(BL.DroneForList drone in b.DisplayDrones())
                        {
                            Console.WriteLine(drone);
                        }
                    }
                    break;
                case (int)DisplayAll.Parcels:
                    {
                        foreach (BL.ParcelForList parcel in b.DisplayParcels())
                            Console.WriteLine(parcel);
                    }
                    break;
                case (int)DisplayAll.Customers:
                    {
                        foreach(BL.CustomerForList customer in b.DisplayCustomers())
                            Console.WriteLine(customer);
                    }
                    break;
                case (int)DisplayAll.Stations:
                    {

                        foreach(BL.StationForList station in b.DisplayStations())
                            Console.WriteLine(station);
                    }
                    break;
                case (int)DisplayAll.FreeParcels:
                    {
                        foreach (BL.ParcelForList parcel in b.DisplayFreeParcels())
                            Console.WriteLine(parcel);
                    }
                    break;
                case (int)DisplayAll.VacantStations:
                    {
                        foreach (BL.StationForList station in b.DisplayFreeStations())
                            Console.WriteLine(station);
                    }
                    break;
                default:
                    Console.WriteLine("invalid input try again");
                    break;
            }

        }
    }
}
*/
