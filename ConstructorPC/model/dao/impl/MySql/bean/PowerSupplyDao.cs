﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using ConstructorPC.dao.api;
using ConstructorPC.beans;
using System.Text;

namespace ConstructorPC.dao.impl.MySql
{
    public class PowerSupplyDao : AbstractDao<PowerSupply, int>, IPowerSupplyDao
    {
        protected override string getSelectByIdQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT p.*, w.*, mf.*, ps.* FROM products AS p ");
            query.AppendLine("WHERE ware_id = @id");
            query.AppendLine("JOIN manufacturers AS mf ");
            query.AppendLine("ON p.manufacturers_id=mf.id");
            query.AppendLine("JOIN wares AS w ");
            query.AppendLine("ON w.id=p.ware_id ");
            query.AppendLine("JOIN power_supplies AS ps ");
            query.AppendLine("ON ps.id=w.ware_id ");
            return query.ToString();
        }

        protected override string getSelectAllQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("SELECT p.*, w.*, mf.*, ps.* FROM products AS p ");
            query.AppendLine("JOIN manufacturers AS mf ");
            query.AppendLine("ON p.manufacturers_id=mf.id");
            query.AppendLine("JOIN wares AS w ");
            query.AppendLine("ON w.id=p.ware_id ");
            query.AppendLine("JOIN power_supplies AS ps ");
            query.AppendLine("ON ps.id=w.ware_id ");
            return query.ToString();
        }

        protected override string getInsertQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("INSERT INTO power_supplies (power, efficiency, pfc_modul, modular, noize_level, features)");
            query.AppendLine("VALUES (@power, @efficiency, @pfc_modul, @modular, @noize_level, @features);");
            //query.AppendLine("@lastId := 1;");
            query.AppendLine("INSERT INTO wares (ware_name, category_id, ware_id)");
            query.AppendLine("VALUES (@ware_name, @category_id, last_insert_id());");
            return query.ToString();
        }

        protected override string getUpdateQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("UPDATE power_supplies");
            query.AppendLine("SET power=@power, efficiency=@efficiency, pfc_modul=@pfc_modul, modular=@modular, noize_level=@noize_level, features=@features");
            query.AppendLine("WHERE id=@id;");
            query.AppendLine("UPDATE wares");
            query.AppendLine("SET ware_name=@ware_name");
            query.AppendLine("WHERE ware_id=@id;");
            return query.ToString();
        }

        protected override string getDeleteQuery()
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine("DELETE FROM power_supplies WHERE id = @id;");
            query.AppendLine("DELETE FROM wares WHERE ware_id=@id;");
            return query.ToString();
        }

        protected override PowerSupply parseDataReader(MySqlDataReader reader)
        {
            PowerSupply temp = new PowerSupply();
            temp.Id = reader.GetInt32("id");
            temp.Name = reader.GetString("ware_name");
            temp.Manufacturer = reader.GetString("mf_name");
            temp.Price = reader.GetDecimal("price");
            temp.InStock = reader.GetInt32("in_stock");

            temp.Power = reader.GetInt32("power");
            temp.Efficiency = reader.GetFloat("efficiency");
            temp.IsActivePfc = reader.GetBoolean("pfc_modul");
            temp.IsModular = reader.GetBoolean("modular");
            temp.NoizeLevel = reader.GetInt32("noize_level");
            temp.Features = reader.GetString("ps_features");
            return temp;
        }

        protected override void prepareCommandForDelete(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForInsert(MySqlCommand command, PowerSupply entity)
        {
            command.Parameters.AddWithValue("power", entity.Power);
            command.Parameters.AddWithValue("efficiency", entity.Efficiency);
            command.Parameters.AddWithValue("pfc_modul", entity.IsActivePfc);
            command.Parameters.AddWithValue("modular", entity.IsModular);
            command.Parameters.AddWithValue("noize_level", entity.NoizeLevel);
            command.Parameters.AddWithValue("features", entity.Features);
            command.Parameters.AddWithValue("category_id", 2);
            command.Parameters.AddWithValue("ware_name", entity.Name);
        }

        protected override void prepareCommandForSelectById(MySqlCommand command, int id)
        {
            command.Parameters.AddWithValue("id", id);
        }

        protected override void prepareCommandForUpdate(MySqlCommand command, int id, PowerSupply entity)
        {
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("power", entity.Power);
            command.Parameters.AddWithValue("efficiency", entity.Efficiency);
            command.Parameters.AddWithValue("pfc_modul", entity.IsActivePfc);
            command.Parameters.AddWithValue("modular", entity.IsModular);
            command.Parameters.AddWithValue("noize_level", entity.NoizeLevel);
            command.Parameters.AddWithValue("features", entity.Features);
            command.Parameters.AddWithValue("ware_name", entity.Name);
        }
    }
}
