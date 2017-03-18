using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.beans
{
    public class Motherboard : Product
    {
        public string Socket { get; set; }
        public string FormFActor { get; set; }

        public Dictionary<string, int> Slots { get; set; }
        public Dictionary<string, int> Interfaces { get; set; }
        public Dictionary<string, int> PowerInterfaces { get; set; }

        public Motherboard() { }
    }
}
