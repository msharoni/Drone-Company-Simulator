using System;
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
        Station = 1,
        Drone,
        Customer,
        Parcel
    }
    enum DisplayAll
    {
        Stations = 1,
        Drones,
        Customers,
        Parcels,
        FreeParcels,
        VacantStations

    }
    partial class Program
    {
        static public void AddMenu()
        {
            Console.WriteLine("Options: Add station press 1, Add Drone press 2, Add customer Press 3 to Add Parcel press 4");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case (int)AddChoices.Station:
                    AddStation();
                    break;
            }
            static public void UpdateMenu()
            {
                int c = 0;
            }
            static public void DisplayMenu()
            {

            }
            static public void DisplayAllMenu()
            {

            }
        }
    }
}

