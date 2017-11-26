using System.Configuration;

namespace DB.SqlFactory
{
    internal sealed class SqlStringElement : ConfigurationElement
    {
        private const string NameParam = "name";
        private const string SqlParam = "sql";
        private const string ParamsCount = "paramsCount";

        [ConfigurationProperty(NameParam, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this[NameParam];
            }
            set
            {
                this[NameParam] = value;
            }
        }

        [ConfigurationProperty(SqlParam, IsRequired = true)]
        public string Sql
        {
            get
            {
                return (string)this[SqlParam];
            }
            set
            {
                this[SqlParam] = value;
            }
        }

        [ConfigurationProperty(ParamsCount, IsRequired = false, DefaultValue = 0)]
        public int ParametersCount
        {
            get
            {
                return (int) this[ParamsCount]; 
                // TODO: Check int cast.
            }
            set { this[ParamsCount] = value; }
        }
    }
}