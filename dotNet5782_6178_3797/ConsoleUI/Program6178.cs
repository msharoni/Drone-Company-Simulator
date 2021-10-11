using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6178();
            Welcome3797();
        }

        static partial void Welcome3797();
        private static void Welcome6178()
        {
            Console.WriteLine("Enter your name: ");
            string user = Console.ReadLine();
            Console.WriteLine("{0}, welcome to our first console application!", user);
        }
    }
}