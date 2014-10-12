namespace King.Data.Sql.Reflection.Unit.Test
{
    using King.Data.Sql.Reflection.Models;
    using King.Mapper;
    using King.Mapper.Data;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class SchemaReaderTests
    {
        private const string ConnectionString = "Server=server;Database=database;Trusted_Connection=True;";

        [Test]
        public void Constructor()
        {
            new SchemaReader(ConnectionString);
        }

        [Test]
        public void IsISchemaReader()
        {
            Assert.IsNotNull(new SchemaReader(ConnectionString) as ISchemaReader);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorExecutorNull()
        {
            var statements = Substitute.For<IStatements>();
            new SchemaReader(null, statements);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorStatementsNull()
        {
            var executor = Substitute.For<IExecutor>();
            new SchemaReader(executor, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildManifestDefinitionsNull()
        {
            var executor = Substitute.For<IExecutor>();
            var statements = Substitute.For<IStatements>();
            var dl = new SchemaReader(executor, statements);
            dl.BuildManifest(null, new List<Schema>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildManifestSchemaNull()
        {
            var executor = Substitute.For<IExecutor>();
            var statements = Substitute.For<IStatements>();
            var dl = new SchemaReader(executor, statements);
            dl.BuildManifest(new List<Definition>(), null);
        }

        [Test]
        public void BuildManifest()
        {
            var random = new Random();
            var defs = new List<Definition>();
            var schemas = new List<Schema>();
            var schema = new Schema()
            {
                Name = Guid.NewGuid().ToString(),
                Preface = Guid.NewGuid().ToString(),
            };
            var def = schema.Map<Definition>();
            defs.Add(def);

            var schemaCount = random.Next(15);
            for (var i = 0; i < schemaCount; i++)
            {
                var s = new Schema()
                {
                    Name = schema.Name,
                    Preface = schema.Preface,
                    DataType = Guid.NewGuid().ToString(),
                    ParameterName = Guid.NewGuid().ToString(),
                };
                schemas.Add(s);
            }
            var count = random.Next(15);
            for (var i = 0; i < count; i++)
            {
                var d = new Definition()
                {
                    Name = Guid.NewGuid().ToString(),
                    Preface = Guid.NewGuid().ToString(),
                };
                defs.Add(d);
            }

            var executor = Substitute.For<IExecutor>();
            var statements = Substitute.For<IStatements>();

            var dl = new SchemaReader(executor, statements);
            var manifest = dl.BuildManifest(defs, schemas);

            Assert.IsNotNull(manifest);
            Assert.AreEqual(count + 1, manifest.Count());
            var c = new DefinitionComparer();
            Assert.AreEqual(schemaCount, manifest.ElementAt(0).Variables.Count());
        }

        [Test]
        public void BuildManifestEmtpy()
        {
            var executor = Substitute.For<IExecutor>();
            var statements = Substitute.For<IStatements>();
            var dl = new SchemaReader(executor, statements);
            var returned = dl.BuildManifest(new List<Definition>(), new List<Schema>());
            Assert.IsNotNull(returned);
            Assert.AreEqual(0, returned.Count());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MinimizeSchemaNull()
        {
            var executor = Substitute.For<IExecutor>();
            var statements = Substitute.For<IStatements>();
            var dl = new SchemaReader(executor, statements);
            dl.Minimize(null);
        }

        [Test]
        public void Minimize()
        {
            var schemas = new List<Schema>();
            var schema = new Schema()
            {
                Name = Guid.NewGuid().ToString(),
                Preface = Guid.NewGuid().ToString(),
            };

            schemas.AddRange(new[] { schema, schema, schema, schema });

            var executor = Substitute.For<IExecutor>();
            var statements = Substitute.For<IStatements>();
            var dl = new SchemaReader(executor, statements);
            var returned = dl.Minimize(schemas);

            Assert.IsNotNull(returned);
            Assert.AreEqual(1, returned.Count());
            var def = returned.FirstOrDefault();
            Assert.IsNotNull(def);
            Assert.AreEqual(schema.Name, def.Name);
            Assert.AreEqual(schema.Preface, def.Preface);
        }
    }
}