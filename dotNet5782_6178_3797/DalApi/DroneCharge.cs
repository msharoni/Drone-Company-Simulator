using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct DroneCharge
    {
        public int DroneId { get; set; }
        public int StationId { get; set; }

        //DroneCharge struct constructor
        public DroneCharge(int _DroneId, int _StationId)
        {
            DroneId = _DroneId;
            StationId = _StationId;
        }
        //ToString overrided func
        public override string ToString()
        {
            return $"DroneId: {DroneId} StationId: {StationId}";
        }
    }
}
