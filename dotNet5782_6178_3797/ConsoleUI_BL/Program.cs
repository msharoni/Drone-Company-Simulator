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
            Distance,
            Exit
        }
        static void Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Options: Add press 1, Update press 2 \n Display Press 3, Display-All press 4 Exit press 5");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    //add case
                    case (int)Choice.Add:
                        AddMenu();
                        break;
                }

            }
        }
    }
}
