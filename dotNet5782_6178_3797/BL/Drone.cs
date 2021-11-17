using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Drone
    {
        int Id;
        string Model;
        WeightCategories Weight;
        double Battery;
        DroneStatuses Status;
        MovedParcel Parcel;
        Place CurrentPlace;
    }
}
