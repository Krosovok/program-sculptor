using System;
using System.Configuration;

namespace DataAccessInterfaces
{
    public class Dao
    {
        private const string DAO_TAG = "Dao";

        public static IDaoFactory Factory
        {
            get
            {
                DaoConfigurationSection config = (DaoConfigurationSection) ConfigurationManager.GetSection(DAO_TAG);
                Type daoType = Type.GetType(
                    config.DaoClass);
                return (IDaoFactory) Activator.CreateInstance(daoType);
            }
        }
    }
}