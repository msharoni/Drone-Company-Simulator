using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ParcelForList
    {
        int Id{get; set;}
        string SenderName{get; set;}
        string ReciverName{get; set;}
        WeightCategories Weight{get; set;}
        Priorities Priority{get; set;}
        ParcelStatus Status{get; set;}
        public override string ToString()
        {
            return $"Id: {Id} Sender name: {SenderName} Reciver name: {ReciverName} Weight: {Weight} Priority: {Priority} Status: {Status}";
        }
    }
}
