using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    [Serializable]
    internal class UnAvailabe : Exception
    {
        private int id;

        public UnAvailabe()
        {
        }

        public UnAvailabe(int id)
        {
            this.id = id;
        }

        public UnAvailabe(string message) : base(message)
        {
        }

        public UnAvailabe(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnAvailabe(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"the drone with Id: {id} is currently unavailable";
        }
    }
    [Serializable]
    public class NotEnoughBattery : Exception
    {
        private int id;

        public NotEnoughBattery()
        {
        }

        public NotEnoughBattery(int id)
        {
            this.id = id;
        }

        public NotEnoughBattery(string message) : base(message)
        {
        }

        public NotEnoughBattery(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughBattery(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"the drone with Id: {id} does not have enough battery to get to the nearest charging station";
        }

    }
    [Serializable]
    public class NotInMaintenance : Exception
    {
        private int id;

        public NotInMaintenance(int id)
        {
            this.id = id;
        }

        public NotInMaintenance(string message) : base(message)
        {
        }

        public NotInMaintenance(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotInMaintenance(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"the drone with Id: {id} is not in maintenance";
        }

    }
    [Serializable]
    public class NotLinkedYet : Exception
    {
        private int id;

        public NotLinkedYet()
        {
        }

        public NotLinkedYet(int droneId)
        {
            this.id = id;
        }

        public NotLinkedYet(string message) : base(message)
        {
        }

        public NotLinkedYet(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotLinkedYet(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"the drone with Id: {id} is not linked to a parcel yet";
        }
    }
    [Serializable]
    public class ParcelHasAlreadyBeenPickedUp : Exception
    {
        private int parcelId;

        public ParcelHasAlreadyBeenPickedUp()
        {
        }

        public ParcelHasAlreadyBeenPickedUp(int parcelId)
        {
            this.parcelId = parcelId;
        }

        public ParcelHasAlreadyBeenPickedUp(string message) : base(message)
        {
        }

        public ParcelHasAlreadyBeenPickedUp(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParcelHasAlreadyBeenPickedUp(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"the parcel with Id: {parcelId} has been picked up already";
        }
    }
    [Serializable]
    public class NotLinkedOrAlreadyDelivered : Exception
    {
        private int droneId;

        public NotLinkedOrAlreadyDelivered()
        {
        }

        public NotLinkedOrAlreadyDelivered(int droneId)
        {
            this.droneId = droneId;
        }

        public NotLinkedOrAlreadyDelivered(string message) : base(message)
        {
        }

        public NotLinkedOrAlreadyDelivered(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotLinkedOrAlreadyDelivered(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"the drone with Id: {droneId} is either not linked to a parcel yet or has already delivered the parcel";
        }
    }
}
