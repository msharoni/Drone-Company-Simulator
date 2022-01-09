using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public Location Location{get; set;}
        public int NumOfVacantChargers{get; set;}
        public List<ChargingDrone> DronesCharging{get; set;}
        public override string ToString()
        {
            string tostring = $"Id: {Id} Name: {Name} Location: {Location} Number of Vacant Chargers: {NumOfVacantChargers} Drones Charging: ";
            foreach (ChargingDrone drone in DronesCharging)
                tostring += drone.ToString();
            return tostring;
        }
    }
}
