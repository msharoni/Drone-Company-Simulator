using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ParcelForList
    {
        public int Id{get; set;}
        public string SenderName{get; set;}
        public string ReciverName{get; set;}
        public WeightCategories Weight{get; set;}
        public Priorities Priority{get; set;}
        ParcelStatus Status{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Sender name: {SenderName} Reciver name: {ReciverName} Weight: {Weight} Priority: {Priority} Status: {Status}";
        }
    }
}
