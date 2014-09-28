﻿namespace King.Data.Sql.Reflection.Integration.Test
{
    using King.Data.Sql.Reflection.Models;
    using NUnit.Framework;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class SchemaReaderTests
    {
        #region Members
        private readonly string connectionString = ConfigurationManager.AppSettings["database"];
        #endregion

        [Test]
        public async Task Load()
        {
            var dl = new SchemaReader(connectionString);
            var manifest = await dl.Load();

            Assert.IsNotNull(manifest);
            var c = new DefinitionComparer();
            Assert.AreEqual(1, manifest.Values.Count());
            var manyTypes = manifest.Values.FirstOrDefault();
            Assert.IsNotNull(manyTypes);
            Assert.AreEqual(16, manyTypes.Variables.Count());
        }

        [Test]
        public async Task Schemas()
        {
            var dl = new SchemaReader(connectionString);
            var data = await dl.Schemas();

            Assert.IsNotNull(data);
            Assert.AreEqual(16, data.Count());
        }
    }
}