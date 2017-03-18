using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.beans
{
    public class GraphicCard : Product
    {
        public string GraphicChip { get; set; }

        public string MemoryType { get; set; }
        public int MemorySize { get; set; }
        public int MemoryBusFrequency { get; set; }

        public Dictionary<string, int> interfaces { get; set; }
        public Dictionary<string, int> powerInterfaces { get; set; }
    }
}
