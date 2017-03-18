using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using ConstructorPC.dao.api;
using ConstructorPC.beans;

namespace ConstructorPC.dao.impl.MySql
{
    public class InterfaceDao : AbstractDao<string, int>, IInterfaceDao
    {
        public List<string> SelectAllByType(InterfaceType type)
        {
            return ReadAllBy("interfaces", "itf_type", type);
        }
    
        protected override string getSelectByIdQuery()
        {
            return "SELECT * FROM interfaces "+
                "WHERE id=@id";
        }

        protected override string getSelectAllQuery()
        {
            return "SELECT * FROM interfaces";
        }

        protected override string getInsertQuery()
        {
            return "INSERT INTO interfaces (itf_name, itf_type) " +
                " VALUES (@itf_name, @itf_type)";
        }

        protected override string getUpdateQuery()
        {
            return "UPDATE interfaces " +
                " SET itf_name=@itf_name, itf_type=@itf_type " +
                " WHERE id = @id";
        }

        protected override string getDeleteQuery()
        {
            return "DELETE FROM interfaces WHERE id = @id";
        }

        protected override string parseDataReader(MySqlDataReader reader)
        {
            string temp;
            temp = reader.GetString("itf_name");
            return temp;
        }

        protected override void prepareCommandForDelete(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForSelectById(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForInsert(MySqlCommand command, string entity)
        {
            throw new NotImplementedException();
        }

        protected override void prepareCommandForUpdate(MySqlCommand command, int id, string entity)
        {
            throw new NotImplementedException();
        }
    }
}
