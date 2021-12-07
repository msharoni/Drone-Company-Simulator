using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DroneInParcel
    {
        int Id;
        double Battery{get; set;}
        Location CurrentLocation{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Battery: {Battery} Current Location: {CurrentLocation}";
        }
    }
}
