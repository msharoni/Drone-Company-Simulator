using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class IdNotExistException : Exception
    {
        int Id;
        private int? droneId;

        public IdNotExistException(int id)
        {
            Id = id;
        }

        public IdNotExistException(string message) : base(message)
        {
        }

        public IdNotExistException(int? droneId)
        {
            this.droneId = droneId;
        }

        public IdNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IdNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            return $"Id {Id} dosent exists";
        }
    }
    [Serializable]
    public class IdExcistsException : Exception
    {
        int Id;
        private int? droneId;

        public IdExcistsException(int id)
        {
            Id = id;
        }

        public IdExcistsException(string message) : base(message)
        {
        }

        public IdExcistsException(int? droneId)
        {
            this.droneId = droneId;
        }

        public IdExcistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IdExcistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return $"Id {Id} already exists";
        }
    }
    [Serializable]
    public class NoChargeSlotsException : Exception
    {
        public NoChargeSlotsException()
        {
        }

        public NoChargeSlotsException(string message) : base(message)
        {
        }

        public NoChargeSlotsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NoChargeSlotsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public override string ToString()
        {
            return "station has no chargeslots left in station";
        }
    }
}

