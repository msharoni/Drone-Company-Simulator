using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class ChargingDrone
    {
        int Id {get; set;}
        double Battery {get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Battery: {Battery}";
        }
    }
}
