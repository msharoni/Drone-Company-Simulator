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

            public override string ToString()
            {
                return $"Id: {Id} Model: {Model} MaxWeight: {MaxWeight} Status {Status} Battery {Battery}";
            }
        }
        public struct Station {
            int Id {get; set};
            int Name {get; set};
            double Longitude {get; set};
            double Lattitude {get; set};
            int ChargeSlots {get; set};

            public override string ToString()
            {
                return $"Id: {Id} Name: {Name} Longitude: {Longitude} Lattitude: {Lattitude} ChargeSlots: {ChargeSlots}";
            }
         }
        public struct DroneCharge {
            int DroneId {get; set};
            int StationId {get; set};

            public override string ToString()
            {
                return $"DroneId: {DroneId} StationId: {StationId}";
            }
         }
        public struct Parcel {
            int Id {get; set};
            int Senderid {get; set};
            int Targetid {get; set};
            WeightCategories Weight {get; set};
            Priorities Priority {get; set};
            DateTime Requested {get; set};
            int DroneId {get; set};
            DateTime Scheduled {get; set};
            DateTime PickedUp {get; set};
            DateTime Delivered {get; set};
            +
                $" 
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
