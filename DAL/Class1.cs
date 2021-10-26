using System;

namespace IDAL
{
    namespace DO
    {
        
        public struct Drone{
            int Id {get; set};
            string Model {get; set};
            WeightCategories MaxWeight {get; set};
            DroneStatuses Status {get; set};
            double Battery {get; set};

            //Drone struct constructor
            public Drone(int _Id, string _Model, WeightCategories _MaxWeight, DroneStatuses _Status, double _Battery)
            )
            {
`               Id = _Id;
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
            int Id {get; set};
            string Name {get; set};
            double Longitude {get; set};
            double Lattitude {get; set};
            int ChargeSlots {get; set};

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
            int DroneId {get; set};
            int StationId {get; set};

            //DroneCharge struct constructor
            publoc DroneCharge(int _DroneId, int _StationId)
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
            int Id {get; set};
            int SenderId {get; set};
            int TargetId {get; set};
            WeightCategories Weight {get; set};
            Priorities Priority {get; set};
            DateTime Requested {get; set};
            int DroneId {get; set};
            DateTime Scheduled {get; set};
            DateTime PickedUp {get; set};
            DateTime Delivered {get; set};
            //Parcel struct constructor
            public Parcel(int _Id, int _Senderid, int _TargetId, WeightCategories _Weight, Priorities _Priority, DateTime _Requested, int _DroneId, DateTime _Scheduled, DateTime _PickedUp, DateTime _Delivered)
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
                return $"Id: {Id} Senderid: {Senderid} Targetid: {Targetid} Weight:{Weight}" +
                $"Priority: {Priority} Requested: {Requested} DroneId: {DroneId} Scheduled: {Scheduled} PickedUp: {PickedUp} Delivered: {Delivered}";
            }
            
        }
        public struct Customer{
            int Id {get; set};
            string Name {get; set};
            string Phone {get; set};
            double Longitude {get; set};
            double Lattitude {get; set};

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
        enum WeightCategories{
            Light,
            Medium,
            Heavy
        }
        enum DroneStatuses{
            Available,
            maintenance,
            Delivery
        }
        enum Priorities{
            Regular,
            Fast,
            Emergency
        }

        

    }
}
