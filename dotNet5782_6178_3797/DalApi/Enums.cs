using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum WeightCategories
    {
        Light = 1,
        Medium,
        Heavy
    }
    public enum DroneStatuses
    {
        Available = 1,
        maintenance,
        Delivery
    }
    public enum Priorities
    {
        Regular = 1,
        Fast,
        Emergency
    }
}
