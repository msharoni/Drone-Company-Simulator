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
            Customer tmpCustomer = new Customer();
            IDAL.DO.Customer customer = dalObject.GetCustomer(Id);
            //putting the ready variables into tmpCustomer 
            tmpCustomer.Id = Id;
            tmpCustomer.name = customer.Name;
            tmpCustomer.phone = customer.Phone;
            tmpCustomer.Location = new Location{Longitude = customer.Longitude, Lattitude = customer.Lattitude};
            //creating lists and filling them 
            List<ParcelInCustomer> FromCustomer = new List<ParcelInCustomer>();
            List<ParcelInCustomer> ForCustomer = new List<ParcelInCustomer>(); 
            ParcelInCustomer cParcel = new ParcelInCustomer();
            foreach(IDAL.DO.Parcel parcel in dalObject.GetParcels())
            {
                if(parcel.SenderId == Id)
                {
                    cParcel.Id = parcel.Id;
                    cParcel.Weight = (WeightCategories)parcel.Weight;
                    cParcel.Priority = (Priorities)parcel.Priority;
                    cParcel.Status = (parcel.Delivered != null) ? ParcelStatus.Delivered : (parcel.PickedUp != null) ? ParcelStatus.PickedUp : (parcel.Scheduled != null) ? ParcelStatus.Linked : ParcelStatus.Created;
                    cParcel.Customer = new CustomerInParcel {Id = Id, name = customer.Name};
                    FromCustomer.Add(cParcel);
                }
                else if(parcel.TargetId == Id)
                {
                    cParcel.Id = parcel.Id;
                    cParcel.Weight = (WeightCategories)parcel.Weight;
                    cParcel.Priority = (Priorities)parcel.Priority;
                    cParcel.Status = (parcel.Delivered != null) ? ParcelStatus.Delivered : (parcel.PickedUp != null) ? ParcelStatus.PickedUp : (parcel.Scheduled != null) ? ParcelStatus.Linked : ParcelStatus.Created;
                    cParcel.Customer = new CustomerInParcel { Id = Id, name = customer.Name };
                    ForCustomer.Add(cParcel);
                }
            }
            //putting the lists in tmpCustomer
            tmpCustomer.ForCustomer = ForCustomer;
            tmpCustomer.FromCustomer = FromCustomer;
            return tmpCustomer;
        }
        public List<CustomerForList> DisplayCustomers()
        {
            List<CustomerForList> customers = new List<CustomerForList>();
            CustomerForList CurrentCustomer = new CustomerForList();
            foreach (IDAL.DO.Customer customer in dalObject.GetCustomers())
            {
                CurrentCustomer.Id = customer.Id;
                CurrentCustomer.name = customer.Name;
                CurrentCustomer.phone = customer.Phone;
                CurrentCustomer.NumOfSent = DisplayCustomer(customer.Id).FromCustomer.FindAll(parcel => parcel.Status == ParcelStatus.Delivered).Count();
                CurrentCustomer.NumOfUnsent = DisplayCustomer(customer.Id).FromCustomer.FindAll(parcel => parcel.Status != ParcelStatus.Delivered).Count();
                CurrentCustomer.NumRecived = DisplayCustomer(customer.Id).ForCustomer.FindAll(parcel => parcel.Status == ParcelStatus.Delivered).Count();
                CurrentCustomer.OnTheWay = DisplayCustomer(customer.Id).ForCustomer.FindAll(parcel => parcel.Status != ParcelStatus.Delivered).Count();
                customers.Add(CurrentCustomer);
            }
            return customers;
        }
    }
}
