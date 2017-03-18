
using ConstructorPC.beans;
using System.Collections.Generic;

namespace ConstructorPC.dao.api
{
    public interface IInterfaceDao : IGenericDao<string, int>
    {
        List<string> SelectAllByType(InterfaceType type);
    }
}
