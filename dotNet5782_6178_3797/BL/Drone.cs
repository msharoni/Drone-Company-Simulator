using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Drone
    {
        public int Id{get; set;}
        public string Model{get; set;}
        public WeightCategories Weight{get; set;}
        public double Battery{get; set;}
        public DroneStatuses Status{get; set;}
        public MovedParcel Parcel{get; set;}
        public Location CurrentLocation{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Model: {Model} Weight: {Weight} Battery: {(int)Battery}% Status: {Status} Parcel: {Parcel} Current Location: {CurrentLocation}";
        }
    }
}
