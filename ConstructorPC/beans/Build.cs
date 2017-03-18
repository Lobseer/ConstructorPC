using ConstructorPC.beans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.beans
{
    public class Build
    {
        public List<Motherboard> mbList;
        public List<Cpu> cpuList;
        public List<PowerSupply> psList;
        public List<Ram> ramList;
        public List<Hdd> romList;
        public List<GraphicCard> vcList;
    }
}
