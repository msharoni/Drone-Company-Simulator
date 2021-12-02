using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ParcelInCustomer
    {
        int Id{get; set;}
        WeightCategories Weight{get; set;}
        Priorities Priority{get; set;}
        ParcelStatus Status{get; set;}
        CustomerInParcel Customer{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Weight: {Weight} Priority: {Priority} Status: {Status} Customer: {Customer}";
        }
    }
}
