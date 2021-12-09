using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CustomerInParcel
    {
        public int Id {get; set;}
        public string name {get; set;}
        public override string ToString()
        {
            return $"Id: {Id} name: {name}";
        }
    }
}
