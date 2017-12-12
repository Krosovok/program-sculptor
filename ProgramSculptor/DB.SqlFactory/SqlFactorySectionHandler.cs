using System.Configuration;

namespace DB.SqlFactory
{
    public sealed class SqlFactorySectionHandler : ConfigurationSection
    {
        private const string ConnectionStringNameAttr = "connectionStringName";

        [ConfigurationProperty(ConnectionStringNameAttr)]
        public string ConnectionStringName => (string) base[ConnectionStringNameAttr];

        [ConfigurationProperty("SqlStrings", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(SqlStringCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public SqlStringCollection SqlStrings => (SqlStringCollection) this["SqlStrings"];
    }
}