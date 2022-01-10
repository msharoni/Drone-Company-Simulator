using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Parcel
    {
        public int Id{get; set;}
        public CustomerInParcel Sender{get; set;}
        public CustomerInParcel Reciver{get; set;}
        public WeightCategories Weight{get; set;}
        public Priorities Priority{get; set;}
        public DroneInParcel Drone{get; set;}
        public DateTime? Created{get; set;}
        public DateTime? Linked{get; set;}
        public DateTime? PickedUp{get; set;}
        public DateTime? Delivered{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Sender: {Sender} Reciver: {Reciver} Weight: {Weight} Priority: {Priority} Drone: {Drone} Time Created: {Created} Time Linked: {Linked} Time Picked-Up: {PickedUp} Time Delivered: {Delivered}";
        }
    }
}
