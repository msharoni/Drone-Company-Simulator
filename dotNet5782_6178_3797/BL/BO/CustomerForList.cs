using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class CustomerForList
    {
        public int Id {get; set;}
        public string name {get; set;}
        public string phone {get; set;}
        public int NumOfSent {get; set;}
        public int NumOfUnsent {get; set;}
        public int NumRecived {get; set;}
        public int OnTheWay {get; set;}
        public override string ToString()
        {
            return $"Id: {Id} name: {name} phone: {phone} Amount Sent: {NumOfSent} Amount not yet sent: {NumOfUnsent} Amount recived: {NumRecived} Amount on the way: {OnTheWay}";
        }
    }
}
