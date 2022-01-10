using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    
    public class Location
    {
        public double Longitude{get; set;}
        public double Lattitude{get; set;}
        public static bool operator ==(Location A,Location B)
        {
            return A.Longitude != B.Longitude ? false : A.Lattitude == B.Lattitude ? true : false;
        }
        public static bool operator !=(Location A, Location B)
        {
            return A == B ? false : true;
        }
        public override string ToString()
        {
            return $"Longitude: {Longitude} Lattitude: {Lattitude}";
        }
    }

}
