using System.Configuration;
using DB.SqlFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.DB.SqlFactoryTest
{
    [TestClass]
    public class SqlFactoryTest
    {
        private const string ExpectedConnectionString = "MyConn";
        private const string SqlKey = "Sql";
        //private const string SqlParamsKey = "SqlParams";
        
        private const string ExpectedSql = "SqlText";
        //private const string Param = "param";
        //private const string ExpectedSqlWithParams = "sql with param";
        
        [TestMethod]
        public void TestConnectionString()
        {
            string actualConnectionString = SqlStringFactory.ConnectionString;

            Assert.AreEqual(
                ExpectedConnectionString,
                actualConnectionString);
        }

        [TestMethod]
        public void TestSqlStringRead()
        {
            string sqlText = SqlStringFactory.GetSql(SqlKey);

            Assert.AreEqual(ExpectedSql, sqlText);
        }

        //[TestMethod]
        //public void TestSqlWithParameters()
        //{
        //    string sqlWithParameters = SqlStringFactory.GetSql(SqlParamsKey, Param);
            
        //    Assert.AreEqual(ExpectedSqlWithParams, sqlWithParameters);
        //}
    }
}