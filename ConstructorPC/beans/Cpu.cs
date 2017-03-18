using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.beans
{
    public class Cpu : Product
    {
        public string Socket { get; set; }
        public int CoreCount { get; set; }
        public int ClockRate { get; set; }
        public int DataBusFrequency { get; set; }
        public bool GraphicAddapter { get; set; }
        public bool UnlockedMultiplier { get; set; }
    }
}
