using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Station
    {
        int Id;
        string Name;
        Place place;
        int NumOfFreeChargers;
        List<ChargingDrone> DronesCharging;
    }
}
