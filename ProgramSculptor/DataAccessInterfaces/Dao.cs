using System;
using System.Configuration;

namespace DataAccessInterfaces
{
    public class Dao
    {
        private const string DaoTag = "Dao";
        private static IDaoFactory factory;

        public static IDaoFactory Factory => factory ?? (factory = DaoFactory());

        private static IDaoFactory DaoFactory()
        {
            DaoConfigurationSection config = (DaoConfigurationSection) ConfigurationManager.GetSection(DaoTag);
            Type daoType = Type.GetType(
                config.DaoClass);
            if (config == null || daoType == null)
            {
                throw new ConfigurationErrorsException("Dao is not configured.");
            }
            return (IDaoFactory) Activator.CreateInstance(daoType);
        }
    }
}