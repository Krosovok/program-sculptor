using System.Configuration;

namespace DB.SqlFactory
{
    internal sealed class SqlFactorySectionHandler : ConfigurationSection
    {
        [ConfigurationProperty("connetionStringName")]
        public string ConnectionStringName => (string) base["connetionStringName"];
        
        [ConfigurationProperty("SqlStrings", IsDefaultCollection = false),
         ConfigurationCollection(typeof(SqlStringCollection), 
             AddItemName = "add", 
             ClearItemsName = "clear", 
             RemoveItemName = "remove")]
        public SqlStringCollection SqlStrings => (SqlStringCollection)this["SqlStrings"];
    }
}
