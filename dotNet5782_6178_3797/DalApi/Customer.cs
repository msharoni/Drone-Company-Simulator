using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }

        //Customer struct constructor
        public Customer(int _Id, string _Name, string _Phone, double _Longitude, double _Lattitude)
        {
            Id = _Id;
            Name = _Name;
            Phone = _Phone;
            Longitude = _Longitude;
            Lattitude = _Lattitude;
        }
        //ToString overrided func
        public override string ToString()
        {
            return $"Id: {Id} Name: {Name} Phone: {Phone} Longitude: {Longitude} Lattitude: {Lattitude}";
        }
    }
}
