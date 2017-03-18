using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorPC.model
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        string Manufacturer { get; set; }
        decimal Price { get; set; }
        int InStock { get; set; }

        object Ware { get; set; }
    }
}
