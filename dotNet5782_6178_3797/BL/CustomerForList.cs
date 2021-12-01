using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    class CustomerForList
    {
        int Id {get; set;}
        string name {get; set;}
        int phone {get; set;}
        int NumOfSent {get; set;}
        int NumOfUnsent {get; set;}
        int NumRecived {get; set;}
        int OnTheWay {get; set;}
        public override string ToString()
        {
            return $"Id: {Id} name: {name} phone: {phone} Amount Sent: {NumOfSent} Amount not yet sent: {NumOfUnsent} Amount recived: {NumRecived} Amount on the way: {OnTheWay}";
        }
    }
}
