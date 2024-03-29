﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
namespace BL
{
    sealed partial class BL : IBL
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(int Id, string? name, string? Phone)
        {
            lock (Idal)
            {
                if (name != null)
                    try
                    {
                        Idal.UpdateCustomer(Id, name, null);
                    }
                    catch (DO.IdNotExistException)
                    {
                        throw new IdNotExistException(Id);
                    }
                if (Phone != null)
                    try
                    {
                        Idal.UpdateCustomer(Id, null, Phone);
                    }
                    catch (DO.IdNotExistException)
                    {
                        throw new IdNotExistException(Id);
                    }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(int Id, string Name, string Phone, Location location)
        {
            lock (Idal)
            {
                try
                {
                    Idal.AddCustomer(Id, Name, Phone, location.Longitude, location.Lattitude);
                }
                catch (DO.IdExcistsException)
                {
                    throw new IdExcistsException(Id);
                }
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer DisplayCustomer(int Id)
        {
            lock (Idal)
            {
                try
                {
                    Idal.GetCustomer(Id);
                }
                catch (DO.IdNotExistException EX)
                {
                    throw new IdNotExistException(Id);
                }
                Customer tmpCustomer = new Customer();
                DO.Customer customer = Idal.GetCustomer(Id);
                //putting the ready variables into tmpCustomer 
                tmpCustomer.Id = Id;
                tmpCustomer.name = customer.Name;
                tmpCustomer.phone = customer.Phone;
                tmpCustomer.Location = new Location { Longitude = customer.Longitude, Lattitude = customer.Lattitude };
                //creating lists and filling them 
                List<ParcelInCustomer> FromCustomer = new List<ParcelInCustomer>();
                List<ParcelInCustomer> ForCustomer = new List<ParcelInCustomer>();
                foreach (DO.Parcel parcel in Idal.GetParcels())
                {
                    ParcelInCustomer cParcel = new ParcelInCustomer();
                    if (parcel.SenderId == Id)
                    {
                        cParcel.Id = parcel.Id;
                        cParcel.Weight = (WeightCategories)parcel.Weight;
                        cParcel.Priority = (Priorities)parcel.Priority;
                        cParcel.Status = (parcel.Delivered != null) ? ParcelStatus.Delivered : (parcel.PickedUp != null) ? ParcelStatus.PickedUp : (parcel.Scheduled != null) ? ParcelStatus.Linked : ParcelStatus.Created;
                        cParcel.Customer = new CustomerInParcel { Id = Id, name = customer.Name };
                        FromCustomer.Add(cParcel);
                    }
                    if (parcel.TargetId == Id)
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
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<CustomerForList> DisplayCustomers()
        {
            lock (Idal)
            {
                List<CustomerForList> customers = new List<CustomerForList>();
                foreach (DO.Customer customer in Idal.GetCustomers())
                {
                    CustomerForList CurrentCustomer = new CustomerForList();
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
}
