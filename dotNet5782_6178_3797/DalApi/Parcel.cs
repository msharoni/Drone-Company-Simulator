using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Requested { get; set; }
        public int? DroneId { get; set; }
        public DateTime? Scheduled { get; set; }
        public DateTime? PickedUp { get; set; }
        public DateTime? Delivered { get; set; }
        //Parcel struct constructor
        public Parcel(int _Id, int _SenderId, int _TargetId, WeightCategories _Weight, Priorities _Priority, DateTime? _Requested, int? _DroneId, DateTime? _Scheduled, DateTime? _PickedUp, DateTime? _Delivered)
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
}
