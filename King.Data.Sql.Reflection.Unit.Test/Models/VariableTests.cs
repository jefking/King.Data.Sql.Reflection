namespace King.Data.Sql.Reflection.Unit.Test.Models
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using King.Mapper;
    using King.Data.Sql.Reflection.Models;

    [TestFixture]
    public class VariableTest
    {
        #region Valid Cases
        [Test]
        public void Constructor()
        {
            new Variable();
        }

        [Test]
        public void IsIVariable()
        {
            Assert.IsNotNull(new Variable() as IVariable);
        }

        [Test]
        public void DataType()
        {
            var item = new Variable();
            var data = Guid.NewGuid().ToString();
            item.DataType = data;
            Assert.AreEqual(data, item.DataType);
        }

        [Test]
        public void IsPrimaryKey()
        {
            var item = new Variable();
            Assert.IsFalse(item.IsPrimaryKey);
            item.IsPrimaryKey = true;
            Assert.IsTrue(item.IsPrimaryKey);
        }

        [Test]
        public void ParameterName()
        {
            var item = new Variable();
            var data = Guid.NewGuid().ToString();
            item.ParameterName = data;
            Assert.AreEqual(data, item.ParameterName);
        }

        [Test]
        public void MaxLength()
        {
            var random = new Random();
            var item = new Variable();
            var data = random.Next();
            item.MaxLength = data;
            Assert.AreEqual(data, item.MaxLength);
        }
        #endregion
    }
}