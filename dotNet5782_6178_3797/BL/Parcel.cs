using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Parcel
    {
        int Id;
        CustomerInParcel Sender;
        CustomerInParcel Reciver;
        WeightCategories Weight;
        Priorities Priority;
        DroneInParcel Drone;
        DateTime Created;
        DateTime Linked;
        DateTime PickedUp;
        DateTime Delivered;
    }
}
