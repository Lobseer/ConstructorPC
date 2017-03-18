using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.beans
{
    public class Ram : Product
    {
        public string MemoryType { get; set; }
        public int MemorySize { get; set; }
        public int MemoryBusFrequency { get; set; }
    }
}
