using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Parcel
    {
        int Id{get; set;}
        CustomerInParcel Sender{get; set;}
        CustomerInParcel Reciver{get; set;}
        WeightCategories Weight{get; set;}
        Priorities Priority{get; set;}
        DroneInParcel Drone{get; set;}
        DateTime Created{get; set;}
        DateTime Linked{get; set;}
        DateTime PickedUp{get; set;}
        DateTime Delivered{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Sender: {Sender} Reciver: {Reciver} Weight: {Weight} Priority: {Priority} Drone: {Drone} Time Created: {Created} Time Linked: {Linked} Time Picked-Up: {PickedUp} Time Delivered: {Delivered}";
        }
    }
}
