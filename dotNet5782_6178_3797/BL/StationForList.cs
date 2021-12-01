using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class StationForList
    {
        int Id{get; set;}
        string Name{get; set;}
        int NumOfVacantChargers{get; set;}
        int NumOfOccupiedChargers{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Number of Vacant Chargers: {NumOfVacantChargers} Number Of Occupied Chargers: {NumOfOccupiedChargers}";
        }
    }
}
