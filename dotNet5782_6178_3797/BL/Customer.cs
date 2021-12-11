using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Customer
    {
        public int Id {get; set;}
        public string name {get; set;}
        public string phone {get; set;}
        public Location Location {get; set;}
        public List<ParcelInCustomer> FromCustomer {get; set;}
        public List<ParcelInCustomer> ForCustomer {get; set;}
        public override string ToString()
        {
            string tostring  = $"Id: {Id} name: {name} phone: {phone} Location: {Location} Parcels Sent:  ";
            foreach (ParcelInCustomer parcel in FromCustomer)
                tostring += parcel.ToString();
            tostring += "Parcels Recived: ";
            foreach (ParcelInCustomer parcel in ForCustomer)
                tostring += parcel.ToString();
            return tostring;
        }
    }
}
