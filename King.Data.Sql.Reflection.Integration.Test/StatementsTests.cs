namespace King.Data.Sql.Reflection.Integration.Test
{
    using King.Data.Sql.Reflection.Models;
    using King.Mapper.Data;
    using NUnit.Framework;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class StatementsTests
    {
        #region Members
        private readonly string connectionString = ConfigurationManager.AppSettings["database"];
        #endregion

        [Test]
        public async Task StoredProcedures()
        {
            var loader = new Loader<Schema>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var cmd = connection.CreateCommand();
                cmd.CommandText = Statements.StoredProcedures;

                var models = loader.Models(cmd);
                Assert.IsNotNull(models);
                Assert.AreEqual(16, models.Count());
                foreach (var schema in models)
                {
                    Assert.AreEqual("dbo", schema.Preface);
                    Assert.AreEqual("ManyTypes", schema.Name);
                }
            }
        }

        [Test]
        public async Task Tables()
        {
            var loader = new Loader<Schema>();
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var cmd = connection.CreateCommand();
                cmd.CommandText = Statements.Tables;

                var models = loader.Models(cmd);
                Assert.IsNotNull(models);
                Assert.AreEqual(20, models.Count());
                foreach (var schema in models)
                {
                    Assert.AreEqual("dbo", schema.Preface);
                    Assert.IsTrue(schema.Name == "LotsOfStuff" || schema.Name == "DualPrimaryKeys");
                }
            }
        }
    }
}