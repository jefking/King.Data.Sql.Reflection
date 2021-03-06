﻿namespace King.Data.Sql.Reflection.Integration.Test
{
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
        public async Task LoadDefault()
        {
            var dl = new SchemaReader(connectionString);
            var manifest = await dl.Load();

            Assert.IsNotNull(manifest);
            Assert.AreEqual(1, manifest.Count());
            var manyTypes = manifest.FirstOrDefault();
            Assert.IsNotNull(manyTypes);
            Assert.AreEqual(16, manyTypes.Variables.Count());
        }

        [Test]
        public async Task LoadSprocs()
        {
            var dl = new SchemaReader(connectionString);
            var manifest = await dl.Load(SchemaTypes.StoredProcedure);

            Assert.IsNotNull(manifest);
            Assert.AreEqual(1, manifest.Count());
            var manyTypes = manifest.FirstOrDefault();
            Assert.IsNotNull(manyTypes);
            Assert.AreEqual(16, manyTypes.Variables.Count());
        }

        [Test]
        public async Task LoadTable()
        {
            var dl = new SchemaReader(connectionString);
            var manifest = await dl.Load(SchemaTypes.Table);

            Assert.IsNotNull(manifest);
            Assert.AreEqual(2, manifest.Count());
            var table = (from m in manifest
                               where m.Preface == "dbo"
                               && m.Name == "LotsOfStuff"
                               select m).FirstOrDefault();
            Assert.IsNotNull(table);
            Assert.AreEqual(17, table.Variables.Count());
            var key = (from v in table.Variables
                       where v.IsPrimaryKey
                       select v).FirstOrDefault();
            Assert.IsNotNull(key);
            Assert.AreEqual("Id", key.ParameterName);
        }

        [Test]
        public async Task DualPrimaryKeys()
        {
            var dl = new SchemaReader(connectionString);
            var manifest = await dl.Load(SchemaTypes.Table);

            Assert.IsNotNull(manifest);
            Assert.AreEqual(2, manifest.Count());
            var table = (from m in manifest
                         where m.Preface == "dbo"
                         && m.Name == "DualPrimaryKeys"
                         select m).FirstOrDefault();
            Assert.IsNotNull(table);
            Assert.AreEqual(3, table.Variables.Count());
            var keys = from v in table.Variables
                       where v.IsPrimaryKey
                       select v;
            Assert.IsNotNull(keys);
            foreach (var key in keys)
            {
                Assert.IsTrue(key.ParameterName == "FirstId" || key.ParameterName == "SecondId");
            }
        }

        [Test]
        public async Task SchemasDefault()
        {
            var dl = new SchemaReader(connectionString);
            var data = await dl.Schemas();

            Assert.IsNotNull(data);
            Assert.AreEqual(16, data.Count());
        }

        [Test]
        public async Task SchemasSprocs()
        {
            var dl = new SchemaReader(connectionString);
            var data = await dl.Schemas(SchemaTypes.StoredProcedure);

            Assert.IsNotNull(data);
            Assert.AreEqual(16, data.Count());
        }

        [Test]
        public async Task SchemasTable()
        {
            var dl = new SchemaReader(connectionString);
            var data = await dl.Schemas(SchemaTypes.Table);

            Assert.IsNotNull(data);
            Assert.AreEqual(20, data.Count());
        }
    }
}