using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using DataAccessInterfaces;
using Model;

namespace ProviderDao
{
    internal abstract class DbReader<T>
    {
        protected List<T> Data;

        public virtual List<T> GetList()
        {
            try
            {
                DbCommand select = SelectAllCommand();

                return ReadData(select);
            }
            catch (DbException e)
            {
                throw new DataAccessException("Can't access data of type " + typeof(T).Name, e);
            }
        }

        public T GetById(int id)
        {
            try
            {
                DbCommand select = SelectByIdCommand(id);

                return ReadData(select).First();
            }
            catch (DbException e)
            {
                throw new DataAccessException("Can't access data of type " + typeof(T).Name, e);
            }
        }

        protected abstract DbCommand SelectByIdCommand(int id);

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

        protected abstract DbCommand SelectAllCommand();

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

        protected List<T> ReadData(DbCommand select)
        {
            Data = new List<T>();
            try
            {
                ReadAllRows(select);
            }
            finally
            {
                Db.Instance.CloseCommand(select);
            }

            return Data;
        }
    }
}