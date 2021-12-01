using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    
    class Location
    {
        double Longitude{get; set;}
        double Lattitude{get; set;}
        public override string ToString()
        {
            return $"Longitude: {Longitude} Lattitude: {Lattitude}";
        }
    }

}
