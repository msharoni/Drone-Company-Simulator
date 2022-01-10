using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class MovedParcel
    {
        public int Id{get; set;}
        public bool Status{get; set;}
        public Priorities Priority{get; set;}
        public WeightCategories Weight{get; set;}
        public CustomerInParcel Sender{get; set;}
        public CustomerInParcel Reciver{get; set;}
        public Location PickUp{get; set;}
        public Location DropOff{get; set;}
        public double Distance{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Status: {Status} Weight: {Weight} Sender: {Sender} Reciver: {Reciver} Pick-Up: {PickUp} Drop-Off: {DropOff} Distance: {Distance}";
        }
    }
}
