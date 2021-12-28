using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public struct Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }


        //Drone struct constructor
        public Drone(int _Id, string _Model, WeightCategories _MaxWeight)
        {
            Id = _Id;
            Model = _Model;
            MaxWeight = _MaxWeight;
        }
        //ToString overided func
        public override string ToString()
        {
            return $"Id: {Id} Model: {Model} MaxWeight: {MaxWeight}";
        }
    }
}