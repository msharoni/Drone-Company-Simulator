using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    partial class BL : IBL.IBL
    {
        List<CustomerForList> Customers;
        void UpdateCustomer(int _Id, string? _Name, int? _Phone)
        {
            if(_Name != null) { Customers[Customers.FindIndex(customer => customer.Id == _Id)].name = _Name; }
            if(_Phone != null) { Customers[Customers.FindIndex(customer => customer.Id == _Id)].phone = (int)_Phone; }
        }
    }
}
