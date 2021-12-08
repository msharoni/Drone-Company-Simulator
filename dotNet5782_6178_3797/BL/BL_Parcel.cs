using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void PickUpParcel(int DroneId)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == DroneId);
            if (Drones[DroneIndex].Status != DroneStatuses.Delivery)
                throw new NotLinkedYet(DroneId);
            if (dalObject.GetParcel(Drones[DroneIndex].ParcelId).PickedUp != null)
                throw new ParcelHasAlreadyBeenPickedUp(Drones[DroneIndex].ParcelId);
            Drones[DroneIndex].Battery -= Drones[DroneIndex].batteryPerKM * Distance(Drones[DroneIndex].CurrentLocation, SenderLocation(Drones[DroneIndex].ParcelId));
            Drones[DroneIndex].CurrentLocation = SenderLocation(Drones[DroneIndex].ParcelId);
            IDAL.DO.Parcel parcel = dalObject.GetParcel(Drones[DroneIndex].ParcelId);
            parcel.PickedUp = DateTime.Now;
            dalObject.UpdateParcel(parcel);
        }
        public void DeliverParcel(int DroneId)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == DroneId);
            //throwing exception if not relevant
            if (dalObject.GetParcel(Drones[DroneIndex].ParcelId).Delivered != null && dalObject.GetParcel(Drones[DroneIndex].ParcelId).PickedUp != null)
                throw new NotLinkedOrAlreadyDelivered(DroneId);
            //updating drone
            Drones[DroneIndex].Status = DroneStatuses.Available;
            Drones[DroneIndex].Battery -= Drones[DroneIndex].batteryPerKM * Distance(Drones[DroneIndex].CurrentLocation, SenderLocation(Drones[DroneIndex].ParcelId));
            //updating parcel
            IDAL.DO.Parcel parcel = dalObject.GetParcel(Drones[DroneIndex].ParcelId);
            parcel.Delivered = DateTime.Now;
            dalObject.UpdateParcel(parcel);
        }

        public void AddParcel()
        {
            //check out the id field
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel();
            Console.WriteLine("Enter Id: ");
            parcel.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Sender Id: ");
            parcel.SenderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Target Id: ");
            parcel.TargetId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Weight 1 for Light, 2 for Medium, 3 for Heavy: ");
            parcel.Weight = (IDAL.DO.WeightCategories)Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Priority 1 for Regular, 2 for Fast, 3 for Emergency: ");
            parcel.Priority = (IDAL.DO.Priorities)Convert.ToInt32(Console.ReadLine());
            dalObject.AddParcel(parcel.Id, parcel.SenderId, parcel.TargetId, parcel.Weight, parcel.Priority,DateTime.Now ,null, new DateTime() , new DateTime() , new DateTime());
        }
        public Parcel DisplayParcel(int Id)
        {
            return parcel;
        }
        public List<ParcelForList> DisplayParcels()
        {
            return parcels;
        }
        public List<ParcelForList> DisplayFreeParcels()
        {
            return freeParcels;
        }
    }
}
