using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class DroneForList
    {
        public int Id{get; set;}
        public string Model{get; set;}
        public WeightCategories MaxWeight{get; set;}
        public double Battery{get; set;}
        public DroneStatuses Status{get; set;}
        public Location CurrentLocation{get; set;}
        public int ParcelId{get; set;}
        public double batteryPerKM { get; set; }
        public override string ToString()
        {
            return $"Id: {Id} Model: {Model} Weight: {Weight} Battery: {Battery} Status: {Status} Current Location: {CurrentLocation} Parcel ID: {ParcelId}";
        }
    }
}
