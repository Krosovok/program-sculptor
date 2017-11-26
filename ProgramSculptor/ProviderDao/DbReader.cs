using System.Data;

namespace ProviderDao
{
    public class DbReader
    {
        protected static string GetString(IDataRecord data, string columnName)
        {
            int columnIdx = data.GetOrdinal(columnName);
            string result = data.GetString(columnIdx);
            return result;
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