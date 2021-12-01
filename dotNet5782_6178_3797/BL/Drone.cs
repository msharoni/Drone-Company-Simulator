using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class Drone
    {
        int Id{get; set;}
        string Model{get; set;}
        WeightCategories Weight{get; set;}
        double Battery{get; set;}
        DroneStatuses Status{get; set;}
        MovedParcel Parcel{get; set;}
        Location CurrentLocation{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Model: {Model} Weight: {Weight} Battery: {Battery} Status: {Status} Parcel: {Parcel} Current Location: {CurrentLocation}";
        }
    }
}
