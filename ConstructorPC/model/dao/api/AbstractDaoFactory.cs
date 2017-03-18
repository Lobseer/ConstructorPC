
using ConstructorPC.dao.api;
using ConstructorPC.dao.impl.MySql;

namespace ConstructorPC.dao.api
{
    public abstract class AbstractDaoFactory
    {
        public const int MY_SQL = 1;

        public abstract IBuildDao getBuildDao();

        public abstract IGenericDao<string, int> getCategoryDao();        
        public abstract IGenericDao<string, int> getManufacturerDao();
        public abstract IInterfaceDao getInterfaceDao();

        public abstract IMotherboardDao getMotherboardDao();
        public abstract IPowerSupplyDao getPowerSupplyDao();
        public abstract IGraphicCardDao getGraphicCardDao();
        public abstract ICpuDao getCpuDao();       
        public abstract IRamDao getRamDao();
        public abstract IRomDao getHddDao();

        public static AbstractDaoFactory getDAOFactory(int wichFactory)
        {
            switch (wichFactory)
            {
                case MY_SQL:
                    return new MySqlDaoFactory();
                default:
                    return null;
            }
        }
    }
}
