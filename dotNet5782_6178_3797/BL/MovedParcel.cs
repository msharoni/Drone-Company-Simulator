using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class MovedParcel
    {
        int Id;
        bool Status;
        Priorities Priority;
        WeightCategories Weight;
        CustomerInParcel Sender;
        CustomerInParcel Reciver;
        Place PickUp;
        Place DropOff;
        double Distance;
    }
}
