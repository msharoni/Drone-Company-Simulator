using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void UpdateCustomer(int Id, string? name, string? Phone)
        {
            if (name != null)
                dalObject.UpdateCustomer(Id, name, null);
            if (Phone != null)
                dalObject.UpdateCustomer(Id,null, Phone);
        }
        public void AddCustomer()
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer();
            Console.WriteLine("Enter Id: ");
            customer.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Name: ");
            customer.Name = Console.ReadLine();
            Console.WriteLine("Enter Phone Number: ");
            customer.Phone = Console.ReadLine();
            Console.WriteLine("Enter Longitude: ");
            customer.Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Lattitude: ");
            customer.Lattitude = Convert.ToDouble(Console.ReadLine());
            dalObject.AddCustomer(customer.Id, customer.Name,customer.Phone, customer.Longitude, customer.Lattitude);
        }
        public Customer DisplayCustomer(int Id)
        {
            return customer;
        }
        public List<CustomerForList> DisplayCustomers()
        {
            return customers;
        }
    }
}
