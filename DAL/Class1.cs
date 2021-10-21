using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone{
            int Id {get; set};
            string Model;
            WeightCategories MaxWeight;
            DroneStatuses Status;
            double Battery;

        }
        public struct Station {
            int Id;
            int Name;
            double Longitude;
            double Lattitude;
            int ChargeSlots;
         }
        public struct DroneCharge {
             int DroneId;
             int StationId;
         }
        public struct Parcel {
            int Id;
            int Senderid;
            int Targetid;
            WeightCategories Weight;
            Priorities Priority;
            datetime Requested;
            int DroneId;
            datetime Scheduled;
            datetime PickedUp;
            datetime Delivered;
        }
        public struct Customer{
            int Id;
            string Name;
            string Phone;
            double Longitude;
            double Lattitude;
        }


        

    }
}
