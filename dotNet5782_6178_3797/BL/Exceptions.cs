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
}
