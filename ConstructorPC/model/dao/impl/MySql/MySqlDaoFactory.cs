using System;
using ConstructorPC.dao.api;

namespace ConstructorPC.dao.impl.MySql
{
    class MySqlDaoFactory : DaoFactory
    {
        public override IBuildDao getBuildDao()
        {
            throw new NotImplementedException();
        }

        public override GenericDao<string, int> getCategoryDao()
        {
            return new CategoryDao();
        }

        public override ICpuDao getCpuDao()
        {
            return new CpuDao();
        }

        public override IGraphicCardDao getGraphicCardDao()
        {
            return new GraphicCardDao();
        }

        public override IRomDao getHddDao()
        {
            return new HddDao();
        }

        public override IInterfaceDao getInterfaceDao()
        {
            return new InterfaceDao();
        }

        public override GenericDao<string, int> getManufacturerDao()
        {
            return new ManufacturerDao();
        }

        public override IMotherboardDao getMotherboardDao()
        {
            return new MotherboardDao();
        }

        public override IPowerSupplyDao getPowerSupplyDao()
        {
            return new PowerSupplyDao();
        }

        public override IRamDao getRamDao()
        {
            return new RamDao();
        }
    }
}
