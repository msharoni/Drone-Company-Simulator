using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInParcel
    {
        public int Id;
        public double Battery{get; set;}
        public Location CurrentLocation{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Battery: {Battery}% Current Location: {CurrentLocation}";
        }
    }
}
