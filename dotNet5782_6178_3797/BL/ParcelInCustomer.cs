﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ParcelInCustomer
    {
        public int Id{get; set;}
        public WeightCategories Weight{get; set;}
        public Priorities Priority{get; set;}
        public ParcelStatus Status{get; set;}
        public CustomerInParcel Customer{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Weight: {Weight} Priority: {Priority} Status: {Status} Customer: {Customer}";
        }
    }
}
