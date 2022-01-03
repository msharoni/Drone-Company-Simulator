using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public int ChargeSlots { get; set; }

        //Station struct Constructor
        public Station(int _Id, string _Name, double _Longitude, double _Lattitude, int _ChargeSlots)
        {
            Id = _Id;
            Name = _Name;
            Longitude = _Longitude;
            Lattitude = _Lattitude;
            ChargeSlots = _ChargeSlots;
        }
        //ToString overrided func
        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Longitude/Lattitude: {Longitude + '/' + Lattitude} ChargeSlots: {ChargeSlots}";
        }
    }
}
