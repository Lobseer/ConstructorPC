using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using ConstructorPC.dao.api;
using ConstructorPC.beans;

namespace ConstructorPC.dao.impl.MySql
{
    public class CpuDao : AbstractDao<Cpu, int>, ICpuDao
    {
        protected override string getSelectByIdQuery()
        {
            return "SELECT p.*, m.*, mf.mf_name FROM products AS p" +
                "WHERE ware_id = @id" +
                "JOIN motherboards AS m" +
                "ON m.id=p.ware_id" +
                "JOIN manufacturers AS mf" +
                "ON p.manufacturers_id=mf.id";
        }

        protected override string getSelectAllQuery()
        {
            return "SELECT p.*, w.*, mf.mf_name FROM products AS p " +
                "JOIN wares AS w " +
                "ON w.id=p.ware_id " +
                "JOIN manufacturers AS mf " +
                "ON p.manufacturers_id=mf.id";
        }

        protected override string getInsertQuery()
        {
            return "INSERT INTO pc_components.power_supplies (power, efficiency, pfc_modul, modular, noize_level, features) " +
                " VALUES (@power, @efficiency, @pfc_modul, @modular, @noize_level, @features)";
        }

        protected override string getUpdateQuery()
        {
            return "UPDATE Motherboards " +
                " SET power=@power, efficiency=@efficiency, pfc_modul=@pfc_modul, modular=@modular, noize_level=@noize_level, features=@features " +
                " WHERE id = @id";
        }

        protected override string getDeleteQuery()
        {
            return "DELETE FROM Motherboards WHERE id = @id";
        }

        protected override Cpu parseDataReader(MySqlDataReader reader)
        {
            Cpu temp = new Cpu();
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

        protected override void prepareCommandForInsert(MySqlCommand command, Cpu entity)
        {

        }

        protected override void prepareCommandForSelectById(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForUpdate(MySqlCommand command, int id, Cpu entity)
        {
            command.Parameters.AddWithValue("id", id);

        }
    }
}
