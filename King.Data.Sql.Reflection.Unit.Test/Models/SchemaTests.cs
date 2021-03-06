﻿namespace King.Data.Sql.Reflection.Unit.Test.Models
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
        public void ParameterName()
        {
            var item = new Schema();
            var data = Guid.NewGuid().ToString();
            item.ParameterName = data;
            Assert.AreEqual(data, item.ParameterName);
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
        public void IsPrimaryKey()
        {
            var item = new Schema();
            Assert.IsFalse(item.IsPrimaryKey);
            item.IsPrimaryKey = true;
            Assert.IsTrue(item.IsPrimaryKey);
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