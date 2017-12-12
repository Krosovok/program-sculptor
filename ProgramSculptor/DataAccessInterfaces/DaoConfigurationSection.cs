using System.Configuration;

namespace DataAccessInterfaces
{
    public class DaoConfigurationSection : ConfigurationSection
    {
        private const string DaoClassName = "DaoClassName";

        [ConfigurationProperty(DaoClassName, IsRequired = true)]
        public string DaoClass
        {
            get { return this[DaoClassName].ToString(); }
            set { this[DaoClassName] = value; }
        }
    }
}