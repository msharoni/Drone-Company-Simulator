using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneForList
    {
        public int Id{get; set;}
        public string Model{get; set;}
        public WeightCategories MaxWeight{get; set;}
        public double Battery{get; set;}
        public DroneStatuses Status{get; set;}
        public Location CurrentLocation{get; set;}
        public int ParcelId{get; set;}
        public override string ToString()
        {
            string str = $"Id: {Id}Model: {Model} MaxWeight: {MaxWeight} Battery: {Battery:f2}% Status: {Status} Current Location: {CurrentLocation}";
            if(ParcelId != -1)
                str+= $" Parcel ID: {ParcelId}";
            return str;
        }
    }
}
