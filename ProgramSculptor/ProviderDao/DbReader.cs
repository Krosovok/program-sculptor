using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataAccessInterfaces;
using Model;

namespace ProviderDao
{
    internal abstract class DbReader<T>
    {
        protected List<T> Data;

        public virtual List<T> GetList()
        {
            DbCommand select = SelectCommand();

            Data = new List<T>();
            try
            {
                ReadAllRows(select);
            }
            catch (DbException e)
            {
                throw new DataAccessException("Can't access data of type" + typeof(T).Name, e);
            }
            finally
            {
                Db.Instance.CloseCommand(select);
            }
            
            return Data;
        }

        protected void ReadAllRows(DbCommand select)
        {
            using (DbDataReader reader = select.ExecuteReader())
            {
                while (reader.Read())
                {
                    T item = ReadRow(reader);
                    Data.Add(item);
                }
            }
        }

        protected abstract T ReadRow(IDataRecord data);

        protected abstract DbCommand SelectCommand();

        protected static string GetString(IDataRecord data, string columnName)
        {
            int columnIdx = data.GetOrdinal(columnName);
            if (data.IsDBNull(columnIdx))
            {
                return null;
            }

            return data.GetString(columnIdx);
        }

        protected static int? GetInt32(IDataRecord data, string columnName)
        {
            int columnIdx = data.GetOrdinal(columnName);
            if (data.IsDBNull(columnIdx))
            {
                return null;
            }
            return data.GetInt32(columnIdx);
        }

        protected static int GetInt32NotNull(IDataRecord data, string columnName)
        {
            int columnIdx = data.GetOrdinal(columnName);
            return data.GetInt32(columnIdx);
        }
    }
}