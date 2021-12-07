using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class StationForList
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public int NumOfVacantChargers{get; set;}
        public int NumOfOccupiedChargers{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Number of Vacant Chargers: {NumOfVacantChargers} Number Of Occupied Chargers: {NumOfOccupiedChargers}";
        }
    }
}
