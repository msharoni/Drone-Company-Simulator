using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class MovedParcel
    {
        int Id{get; set;}
        bool Status{get; set;}
        Priorities Priority{get; set;}
        WeightCategories Weight{get; set;}
        CustomerInParcel Sender{get; set;}
        CustomerInParcel Reciver{get; set;}
        Location PickUp{get; set;}
        Location DropOff{get; set;}
        double Distance{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Status: {Status} Weight: {Weight} Sender: {Sender} Reciver: {Reciver} Pick-Up: {PickUp} Drop-Off: {DropOff} Distance: {Distance}";
        }
    }
}
