using ConstructorPC.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace ConstructorPC.dao.impl.MySql
{
    public class ProductDao : AbstractDao<Product, int>
    {
        protected override string getSelectAllQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT p.*, w.*, mf.* FROM products AS p ");
            query.AppendLine("JOIN manufacturers AS mf ");
            query.AppendLine("ON p.manufacturers_id=mf.id");
            query.AppendLine("JOIN wares AS w ");
            query.AppendLine("ON w.id=p.ware_id ");
            return query.ToString();
        }

        protected override string getSelectByIdQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT p.*, w.*, mf.* FROM products AS p ");
            query.AppendLine("WHERE p.id=@id");
            query.AppendLine("JOIN manufacturers AS mf ");
            query.AppendLine("ON p.manufacturers_id=mf.id");
            query.AppendLine("JOIN wares AS w ");
            query.AppendLine("ON w.id=p.ware_id ");
            return query.ToString();
        }

        protected override string getDeleteQuery()
        {
            return "DELETE FROM products WHERE id = @id;";
        }

        protected override string getInsertQuery()
        {
            throw new NotImplementedException();
        }

        protected override string getUpdateQuery()
        {
            throw new NotImplementedException();
        }

        protected override void prepareCommandForInsert(MySqlCommand command, Product entity)
        {
            throw new NotImplementedException();
        }

        protected override void prepareCommandForSelectById(MySqlCommand command, int id)
        {
            throw new NotImplementedException();
        }

        protected override void prepareCommandForUpdate(MySqlCommand command, int id, Product entity)
        {
            throw new NotImplementedException();
        }

        protected override void prepareCommandForDelete(MySqlCommand command, int id)
        {
            throw new NotImplementedException();
        }

        protected override Product parseDataReader(MySqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
