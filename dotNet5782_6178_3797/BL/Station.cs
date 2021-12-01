using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Station
    {
        int Id{get; set;}
        string Name{get; set;}
        Location Location{get; set;}
        int NumOfVacantChargers{get; set;}
        List<ChargingDrone> DronesCharging{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Location: {Location} Number of Vacant Chargers: {NumOfVacantChargers} Drones Charging: {DronesCharging}";
        }
    }
}
