namespace King.Data.Sql.Reflection.Unit.Test.Models
{
    using King.Data.Sql.Reflection.Models;
    using King.Mapper;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class SchemaTests
    {
        [Test]
        public void Constructor()
        {
            new Schema();
        }

        [Test]
        public void IsISchema()
        {
            Assert.IsNotNull(new Schema() as ISchema);
        }

        [Test]
        public void Parameter()
        {
            var item = new Schema();
            var data = Guid.NewGuid().ToString();
            item.Parameter = data;
            Assert.AreEqual(data, item.Parameter);
        }

        [Test]
        public void DataType()
        {
            var item = new Schema();
            var data = Guid.NewGuid().ToString();
            item.DataType = data;
            Assert.AreEqual(data, item.DataType);
        }

        [Test]
        public void Name()
        {
            var item = new Schema();
            var data = Guid.NewGuid().ToString();
            item.Name = data;
            Assert.AreEqual(data, item.Name);
        }

        [Test]
        public void NameAction()
        {
            var item = new Schema();
            var property = (from p in item.GetType().GetProperties()
                            where p.Name == "Name"
                            select p).FirstOrDefault();

            Assert.IsNotNull(property);
            var action = property.GetAttribute<ActionNameAttribute>();
            Assert.IsNotNull(action);
            Assert.AreEqual("StoredProcedure", action.Name);
            Assert.AreEqual(ActionFlags.Load, action.Action);
        }

        [Test]
        public void PrefaceAction()
        {
            var item = new Schema();
            var property = (from p in item.GetType().GetProperties()
                            where p.Name == "Preface"
                            select p).FirstOrDefault();

            Assert.IsNotNull(property);
            var action = property.GetAttribute<ActionNameAttribute>();
            Assert.IsNotNull(action);
            Assert.AreEqual("Schema", action.Name);
            Assert.AreEqual(ActionFlags.Load, action.Action);
        }

        [Test]
        public void Preface()
        {
            var item = new Schema();
            var data = Guid.NewGuid().ToString();
            item.Preface = data;
            Assert.AreEqual(data, item.Preface);
        }

        [Test]
        public void MaxLength()
        {
            var item = new Schema();
            var data = Guid.NewGuid().ToString();
            item.MaxLength = data;
            Assert.AreEqual(data, item.MaxLength);
        }
    }
}