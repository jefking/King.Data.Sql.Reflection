namespace King.Data.Sql.Reflection.Unit.Test
{
    using King.Mapper;
    using King.Mapper.Data;
    using NUnit.Framework;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using King.Data.Sql.Reflection.Models;

    [TestFixture]
    public class SchemaLoader
    {
        [Test]
        public void Constructor()
        {
            new SchemaReader(Guid.NewGuid().ToString());
        }

        [Test]
        public void ConstructorWithLoader()
        {
            var loader = Substitute.For<ILoader<Schema>>();
            new SchemaReader(Guid.NewGuid().ToString(), loader);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ConstructorConnectionStringNull()
        {
            var loader = Substitute.For<ILoader<Schema>>();
            new SchemaReader(null, loader);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorLoaderNull()
        {
            new SchemaReader(Guid.NewGuid().ToString(), null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildManifestDefinitionsNull()
        {
            var loader = Substitute.For<ILoader<Schema>>();
            var dl = new SchemaReader(Guid.NewGuid().ToString(), loader);
            dl.BuildManifest(null, new List<Schema>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BuildManifestSchemaNull()
        {
            var loader = Substitute.For<ILoader<Schema>>();
            var dl = new SchemaReader(Guid.NewGuid().ToString(), loader);
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
                    Parameter = Guid.NewGuid().ToString(),
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

            var loader = Substitute.For<ILoader<Schema>>();
            var dl = new SchemaReader(Guid.NewGuid().ToString(), loader);
            var manifest = dl.BuildManifest(defs, schemas);
            Assert.IsNotNull(manifest);
            Assert.AreEqual(count + 1, manifest.Count);
            var c = new DefinitionComparer();
            Assert.AreEqual(schemaCount, manifest[c.GetHashCode(def)].Variables.Count());
        }

        [Test]
        public void BuildManifestEmtpy()
        {
            var loader = Substitute.For<ILoader<Schema>>();
            var dl = new SchemaReader(Guid.NewGuid().ToString(), loader);
            var returned = dl.BuildManifest(new List<Definition>(), new List<Schema>());
            Assert.IsNotNull(returned);
            Assert.AreEqual(0, returned.Count());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MinimizeSchemaNull()
        {
            var loader = Substitute.For<ILoader<Schema>>();
            var dl = new SchemaReader(Guid.NewGuid().ToString(), loader);
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

            var loader = Substitute.For<ILoader<Schema>>();
            var dl = new SchemaReader(Guid.NewGuid().ToString(), loader);
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