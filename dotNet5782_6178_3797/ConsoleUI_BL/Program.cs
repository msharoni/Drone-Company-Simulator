using System;

namespace ConsoleUI_BL
{
    partial class Program
    {
        enum Choice
        {
            Add = 1,
            Update,
            Display,
            DisplayAll,
            Exit
        }
        static void Main(string[] args)
        {

            bool run = true;
            while (run)
            {
                Console.WriteLine("Options: Add press 1, Update press 2 \n Display Press 3, Display-All press 4 Exit press 5");
                int choice = 0;
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch(FormatException ){}
                switch (choice)
                {
                    //add case
                    case (int)Choice.Add:
                        AddMenu();
                        break;
                    case (int)Choice.Update:
                        UpdateMenu();
                        break;
                    case (int)Choice.Display:
                        DisplayMenu();
                        break;
                    case (int)Choice.DisplayAll:
                        DisplayAllMenu();
                        break;
                    case (int)Choice.Exit:
                        run = false;
                        break;
                    default:
                        Console.WriteLine("invalid input try again");
                        break;
                }

            }
        }
    }
}