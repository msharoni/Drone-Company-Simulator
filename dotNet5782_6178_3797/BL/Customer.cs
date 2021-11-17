using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Customer
    {
        int Id;
        string name;
        int phone;
        Place place;
        List<ParcelInCustomer> FromCustomer;
        List<ParcelInCustomer> ForCustomer;
    }
}
