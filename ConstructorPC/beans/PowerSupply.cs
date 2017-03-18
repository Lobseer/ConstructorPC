using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.beans
{
    public class PowerSupply : Product
    {
        public int Power { get; set; }

        public float Efficiency { get; set; }

        public bool IsActivePfc { get; set; }

        public bool IsModular { get; set; }

        public int NoizeLevel { get; set; }

        public string Features { get; set; }

        public Dictionary<string, int> PowerInterfaces { get; set; }
    }
}
