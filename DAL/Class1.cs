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

        }
        public struct Station {
            int Id {get; set};
            int Name {get; set};
            double Longitude {get; set};
            double Lattitude {get; set};
            int ChargeSlots {get; set};
         }
        public struct DroneCharge {
             int DroneId {get; set};
             int StationId {get; set};
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
        }
        public struct Customer{
            int Id {get; set};
            string Name {get; set};
            string Phone {get; set};
            double Longitude {get; set};
            double Lattitude {get; set};
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
