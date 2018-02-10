using System.Configuration;

namespace DB.SqlFactory
{
    public sealed class SqlFactorySectionHandler : ConfigurationSection
    {
        private const string ConnectionStringNameAttr = "connectionStringName";
        private const string ParameterPrefixAttr = "parameterPrefix";

        [ConfigurationProperty(ConnectionStringNameAttr)]
        public string ConnectionStringName => (string) base[ConnectionStringNameAttr];

        [ConfigurationProperty(ParameterPrefixAttr)]
        public string ParameterPrefix => (string) base[ParameterPrefixAttr];

        [ConfigurationProperty("SqlStrings", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SqlStringCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public SqlStringCollection SqlStrings => (SqlStringCollection) this["SqlStrings"];
    }
}