using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Customer
    {
        int Id {get; set;}
        string name {get; set;}
        int phone {get; set;}
        Location Location {get; set;}
        List<ParcelInCustomer> FromCustomer {get; set;}
        List<ParcelInCustomer> ForCustomer {get; set;}
        public override string ToString()
        {
            return $"Id: {Id} name: {name} phone: {phone} Location: {Location} Parcels Sent: {FromCustomer} Parcels Recived: {ForCustomer}";
        }
    }
}
