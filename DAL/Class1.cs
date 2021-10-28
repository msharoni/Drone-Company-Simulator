using System;

namespace IDAL
{
    namespace DO
    {
        
        public struct Drone{
            public int Id {get; set;}
            public string Model {get; set;}
            public WeightCategories MaxWeight {get; set;}
            public DroneStatuses Status {get; set;}
            public double Battery {get; set;}

            //Drone struct constructor
            public Drone(int _Id, string _Model, WeightCategories _MaxWeight, DroneStatuses _Status, double _Battery)
            {
                Id = _Id;
                Model = _Model;
                MaxWeight = _MaxWeight;
                Status = _Status;
                Battery = _Battery;
            }
            //ToString overided func
            public override string ToString()
            {
                return $"Id: {Id} Model: {Model} MaxWeight: {MaxWeight} Status {Status} Battery {Battery}";
            }
        }
        public struct Station {
            public int Id {get; set;}
            public string Name {get; set;}
            public double Longitude {get; set;}
            public double Lattitude {get; set;}
            public int ChargeSlots {get; set;}

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
                return $"Id: {Id} Name: {Name} Longitude: {Longitude} Lattitude: {Lattitude} ChargeSlots: {ChargeSlots}";
            }
         }
        public struct DroneCharge {
            public int DroneId {get; set;}
            public int StationId {get; set;}

            //DroneCharge struct constructor
            public DroneCharge(int _DroneId, int _StationId)
            {
                DroneId = _DroneId;
                StationId = _StationId;
            }
            //ToString overrided func
            public override string ToString()
            {
                return $"DroneId: {DroneId} StationId: {StationId}";
            }
         }
        public struct Parcel {
            public int Id {get; set;}
            public int SenderId {get; set;}
            public int TargetId {get; set;}
            public WeightCategories Weight {get; set;}
            public Priorities Priority {get; set;}
            public DateTime Requested {get; set;}
            public int? DroneId {get; set;}
            public DateTime? Scheduled {get; set;}
            public DateTime? PickedUp {get; set;}
            public DateTime? Delivered {get; set;}
            //Parcel struct constructor
            public Parcel(int _Id, int _SenderId, int _TargetId, WeightCategories _Weight, Priorities _Priority, DateTime _Requested, int? _DroneId, DateTime? _Scheduled, DateTime? _PickedUp, DateTime? _Delivered)
            {
                Id = _Id;
                SenderId = _SenderId;
                TargetId = _TargetId;
                Weight = _Weight;
                Priority = _Priority;
                Requested = _Requested;
                DroneId = _DroneId;
                Scheduled = _Scheduled;
                PickedUp = _PickedUp;
                Delivered = _Delivered;
            }
            //ToString overrided func
            public override string ToString()
            {
                return $"Id: {Id} Senderid: {SenderId} Targetid: {TargetId} Weight:{Weight}" +
                $"Priority: {Priority} Requested: {Requested} DroneId: {DroneId} Scheduled: {Scheduled} PickedUp: {PickedUp} Delivered: {Delivered}";
            }
            
        }
        public struct Customer{
            public int Id {get; set;}
            public string Name {get; set;}
            public string Phone {get; set;}
            public double Longitude {get; set;}
            public double Lattitude {get; set;}

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
        public enum WeightCategories{
            Light=1,
            Medium,
            Heavy
        }
        public enum DroneStatuses{
            Available=1,
            maintenance,
            Delivery
        }
        public enum Priorities{
            Regular=1,
            Fast,
            Emergency
        }

        

    }
}
