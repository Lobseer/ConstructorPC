using System.Collections.Generic;

namespace ConstructorPC.dao.api
{
    public interface IGenericDao<T, PK> {
        void Create(T entity);
        T Read(PK id);
        void Update(PK id, T entity);
        void Delete(PK id);
        List<T> ReadAll();
        List<T> ReadAllByProcedure(string tableName);
        PK GetLastId(string table_name);
    }
}
