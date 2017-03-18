using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using ConstructorPC.dao.api;

namespace ConstructorPC.dao.impl.MySql
{
    public abstract class AbstractDao<T, PK> : GenericDao<T, PK>
    {
        protected abstract string getInsertQuery();
        protected abstract string getSelectByIdQuery();
        protected abstract string getUpdateQuery();
        protected abstract string getDeleteQuery();
        protected abstract string getSelectAllQuery();

        protected abstract void prepareCommandForInsert(MySqlCommand command, T entity);
        protected abstract void prepareCommandForSelectById(MySqlCommand command, PK id);
        protected abstract void prepareCommandForUpdate(MySqlCommand command, PK id, T entity);
        protected abstract void prepareCommandForDelete(MySqlCommand command, PK id);

        protected abstract T parseDataReader(MySqlDataReader reader);

        public List<string> ReadStringField(string tableName, string fieldName)
        {
            List<string> result = new List<string>();
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM {tableName} AS t;";
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    result.Add(reader.GetString(fieldName));
                }
                conn.Close();
            }
            return result;
        }

        protected List<T> ReadAllBy(string tableName, string fieldName, object value)
        {
            List<T> list = new List<T>();
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = $"SELECT * FROM {tableName} WHERE {fieldName} = @value;";
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                cmd.Parameters.AddWithValue("value", value);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(parseDataReader(reader));
                }
                conn.Close();
            }
            return list;
        }

        public PK GetLastId(string tableName)
        {
            PK id;
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = $"SELECT MAX(t.id) FROM {tableName} as t;";
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                id = (PK)reader.GetValue(0);
                conn.Close();
            }
            return id;
        }

        public void Create(T entity)
        {
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = getInsertQuery();
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                prepareCommandForInsert(cmd, entity);
                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new PersistException("Affected " + affectedRows + ". Expected that query would affect 1 row!");
                }
                conn.Close();
            }
        }

        public void Delete(PK id)
        {
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = getDeleteQuery();
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                prepareCommandForDelete(cmd, id);
                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new PersistException("Affected " + affectedRows + ". Expected that query would affect 1 row!");
                }
                conn.Close();
            }
        }

        public T Read(PK id)
        {
            T entity = default(T);
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = getSelectByIdQuery();
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                prepareCommandForSelectById(cmd, id);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entity = parseDataReader(reader);
                }
                conn.Close();
            }
            return entity;
        }

        public List<T> ReadAll()
        {
            List<T> entities = new List<T>();
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = getSelectAllQuery();
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entities.Add(parseDataReader(reader));
                }
                conn.Close();
            }
            return entities;
        }

        public List<T> ReadAllByProcedure(string tableName)
        {
            List<T> entities = new List<T>();
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = "SelectAllProductsByCategoryName";
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("category", tableName);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entities.Add(parseDataReader(reader));
                }
                conn.Close();
            }
            return entities;
        }

        public void Update(PK id, T entity)
        {
            using (DbConnection conn = ConnectionFactory.getInstance().getConnection())
            {
                conn.Open();
                string query = getUpdateQuery();
                MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)conn);
                prepareCommandForUpdate(cmd, id, entity);
                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    throw new PersistException("Affected " + affectedRows + ". Expected that query would affect 1 row!");
                }
                conn.Close();
            }
        }
    }
}
