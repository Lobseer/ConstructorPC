using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using ConstructorPC.dao.api;
using ConstructorPC.beans;

namespace ConstructorPC.dao.impl.MySql
{
    public class MotherboardDao : AbstractDao<Motherboard, int>, IMotherboardDao
    {
        protected override string getSelectByIdQuery()
        {
            return "SELECT p.*, m.*, mf.mf_name FROM products AS p"+
                "WHERE ware_id = @id"+
                "JOIN motherboards AS m" +
                "ON m.id=p.ware_id"+
                "JOIN manufacturers AS mf" +
                "ON p.manufacturers_id=mf.id";
        }

        protected override string getSelectAllQuery()
        {
            return "SELECT p.*, m.*, mf.mf_name FROM products AS p " +
                "JOIN motherboards AS m " +
                "ON m.id=p.ware_id"+
                "JOIN manufacturers AS mf" +
                "ON p.manufacturers_id=mf.id";
        }

        protected override string getInsertQuery()
        {
            return "INSERT INTO Motherboards (model_name, Motherboard_brand, owner_id, issue_date, number) " +
                " VALUES (@name, @model, @ownerId, @issueDate, @number)";
        }

        protected override string getUpdateQuery()
        {
            return "UPDATE Motherboards " +
                " SET model_name = @name, Motherboard_brand = @model, owner_id = @ownerId, " + " issue_date = @issueDate, number = @number " +
                " WHERE id = @id";
        }

        protected override string getDeleteQuery()
        {
            return "DELETE FROM Motherboards WHERE id = @id";
        }

        protected override Motherboard parseDataReader(MySqlDataReader reader)
        {
            Motherboard temp = new Motherboard();
            temp.Id = reader.GetInt32("id");
            temp.Name = reader.GetString("name");
            temp.Manufacturer = reader.GetString("mf_name");
            temp.Price = reader.GetDecimal("price");
            temp.InStock = reader.GetInt32("in_stock");
            return temp;
        }

        protected override void prepareCommandForDelete(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForInsert(MySqlCommand command, Motherboard entity)
        {

        }

        protected override void prepareCommandForSelectById(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForUpdate(MySqlCommand command, int id, Motherboard entity)
        {

        }
    }
}
