using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using ConstructorPC.dao.api;
using ConstructorPC.beans;

namespace ConstructorPC.dao.impl.MySql
{
    public class ManufacturerDao : AbstractDao<string, int>
    {
        protected override string getSelectByIdQuery()
        {
            return "SELECT * FROM manufacturers "+
                "WHERE id=@id";
        }

        protected override string getSelectAllQuery()
        {
            return "SELECT * FROM manufacturers";
        }

        protected override string getInsertQuery()
        {
            return "INSERT INTO manufacturers (mf_name) " +
                " VALUES (@mf_name, @mf_name)";
        }

        protected override string getUpdateQuery()
        {
            return "UPDATE manufacturers " +
                " SET mf_name=@mf_name " +
                " WHERE id = @id";
        }

        protected override string getDeleteQuery()
        {
            return "DELETE FROM manufacturers WHERE id = @id";
        }

        protected override string parseDataReader(MySqlDataReader reader)
        {
            string temp;
            temp = reader.GetString("mf_name");
            return temp;
        }

        protected override void prepareCommandForDelete(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForInsert(MySqlCommand command, string entity)
        {
            command.Parameters.AddWithValue("mf_name", entity);
        }

        protected override void prepareCommandForSelectById(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForUpdate(MySqlCommand command, int id, string entity)
        {
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("mf_name", entity);
        }
    }
}
