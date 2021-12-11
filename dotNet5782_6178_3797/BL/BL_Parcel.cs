using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL
    {
        public void PickUpParcel(int DroneId)
        {
            int DroneIndex = Drones.FindIndex(drone => drone.Id == DroneId);
            if (Drones[DroneIndex].Status != DroneStatuses.Delivery)
                throw new NotLinkedYet(DroneId);
            if (dalObject.GetParcel(Drones[DroneIndex].ParcelId).PickedUp != null)
                throw new ParcelHasAlreadyBeenPickedUp(Drones[DroneIndex].ParcelId);
            Drones[DroneIndex].Battery -= dalObject.GetBatteryUsage()[1] * Distance(Drones[DroneIndex].CurrentLocation, SenderLocation(Drones[DroneIndex].ParcelId));
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
            Drones[DroneIndex].Battery -= dalObject.GetBatteryUsage()[(int)dalObject.GetParcel(Drones[DroneIndex].ParcelId).Weight] * Distance(Drones[DroneIndex].CurrentLocation, SenderLocation(Drones[DroneIndex].ParcelId));
            //updating parcel
            IDAL.DO.Parcel parcel = dalObject.GetParcel(Drones[DroneIndex].ParcelId);
            parcel.Delivered = DateTime.Now;
            dalObject.UpdateParcel(parcel);
        }

        public void AddParcel(int Id,int SenderId,int TargetId,int Weight,int Priority)
        {
            try
            {
                dalObject.GetCustomer(SenderId);
            }
            catch(DalObject.IdNotExistException ex)
            {
                throw new IdNotExistException(SenderId);
            }
            try
            {
                dalObject.GetCustomer(TargetId);
            }
            catch (DalObject.IdNotExistException ex)
            {
                throw new IdNotExistException(TargetId);
            }
            try
            {
                dalObject.AddParcel(Id, SenderId, TargetId, (IDAL.DO.WeightCategories)Weight, (IDAL.DO.Priorities)Priority, DateTime.Now, null, null, null, null);
            }
            catch (DalObject.IdExcistsException ex)
            {
                throw new IdExcistsException(Id);
            }
        }
        public Parcel DisplayParcel(int Id)
        {
            try
            {
                dalObject.GetParcel(Id);
            }
            catch (DalObject.IdNotExistException EX)
            {
                throw new IdNotExistException(Id);
            }
            Parcel tmpParcel = new Parcel();
            IDAL.DO.Parcel parcel = dalObject.GetParcel(Id);
            tmpParcel.Id = Id;
            //creating 2 customers of type CustomerInParcel for our movedParcel
            CustomerInParcel Sender = new CustomerInParcel();
            CustomerInParcel Reciver = new CustomerInParcel();
            IDAL.DO.Customer sCustomer = dalObject.GetCustomer(parcel.SenderId);
            IDAL.DO.Customer tCustomer = dalObject.GetCustomer(parcel.TargetId);
            Sender.Id = sCustomer.Id;
            Sender.name = sCustomer.Name;
            Reciver.Id = tCustomer.Id;
            Reciver.name = tCustomer.Name;
            //putting the customers in the movedParcel
            tmpParcel.Sender = Sender;
            tmpParcel.Reciver = Reciver;
            tmpParcel.Weight = (WeightCategories)parcel.Weight;
            tmpParcel.Priority = (Priorities)parcel.Priority;
            //creating drone of type DroneInParcel to put in parcel
            DroneInParcel drone = new DroneInParcel();
            int DroneIndex = Drones.FindIndex(drone => drone.Id == parcel.DroneId);
            drone.Id = (int)parcel.DroneId;
            drone.Battery = Drones[DroneIndex].Battery;
            drone.CurrentLocation = Drones[DroneIndex].CurrentLocation;
            tmpParcel.Drone = drone;
            tmpParcel.Created = parcel.Requested;
            tmpParcel.Linked = (DateTime)parcel.Scheduled;
            tmpParcel.PickedUp = (DateTime)parcel.PickedUp;
            tmpParcel.Delivered = (DateTime)parcel.Delivered;
            return tmpParcel;
        }
        public IEnumerable<ParcelForList> DisplayParcels()
        {
            List<ParcelForList> parcels = new List<ParcelForList>();
            foreach (IDAL.DO.Parcel parcel in dalObject.GetParcels())
            {
                ParcelForList CurrentParcel = new ParcelForList();
                CurrentParcel.Id = parcel.Id;
                CurrentParcel.SenderName = dalObject.GetCustomer(parcel.SenderId).Name;
                CurrentParcel.ReciverName = dalObject.GetCustomer(parcel.TargetId).Name;
                CurrentParcel.Weight = (WeightCategories)parcel.Weight;
                CurrentParcel.Priority = (Priorities)parcel.Priority;
                CurrentParcel.Status = (parcel.Delivered != null) ? ParcelStatus.Delivered : (parcel.PickedUp != null) ? ParcelStatus.PickedUp : (parcel.Scheduled != null) ? ParcelStatus.Linked : ParcelStatus.Created;
                parcels.Add(CurrentParcel);
            }
            return parcels;
        }
        public IEnumerable<ParcelForList> DisplayFreeParcels()
        {
            return DisplayParcels().Where(parcel => parcel.Status == ParcelStatus.Created);
        }
    }
}
