namespace King.Data.Sql.Reflection.Unit.Test.Models
{
    using King.Data.Sql.Reflection.Models;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DefinitionComparerTests
    {
        [Test]
        public void EqualsTrue()
        {
            var preface = Guid.NewGuid().ToString();
            var name = Guid.NewGuid().ToString();
            var x = new Definition()
            {
                Preface = preface,
                Name = name,
            };
            var y = new Definition()
            {
                Preface = preface,
                Name = name,
            };
            var c = new DefinitionComparer();
            Assert.IsTrue(c.Equals(x, y));
        }

        [Test]
        public void EqualsSame()
        {
            var preface = Guid.NewGuid().ToString();
            var name = Guid.NewGuid().ToString();
            var x = new Definition()
            {
                Preface = preface,
                Name = name,
            };

            var c = new DefinitionComparer();
            Assert.IsTrue(c.Equals(x, x));
        }

        [Test]
        public void EqualsXNull()
        {
            var x = new Definition()
            {
                Preface = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var c = new DefinitionComparer();
            Assert.That(() => c.Equals(null, x), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void EqualsYNull()
        {
            var x = new Definition()
            {
                Preface = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var c = new DefinitionComparer();
            Assert.That(() => c.Equals(x, null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Equalsfalse()
        {
            var x = new Definition()
            {
                Preface = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };
            var y = new Definition()
            {
                Preface = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var c = new DefinitionComparer();
            Assert.IsFalse(c.Equals(x, y));
        }

        [Test]
        public void GetHashCodeTest()
        {
            var x = new Definition()
            {
                Preface = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
            };

            var hash = string.Format("{0}{1}", x.Preface, x.Name).GetHashCode();
            var c = new DefinitionComparer();
            Assert.AreEqual(hash, c.GetHashCode(x));
        }

        [Test]
        public void GetHashCodeDefinitionNull()
        {
            var c = new DefinitionComparer();
            Assert.That(() => c.GetHashCode(null), Throws.TypeOf<ArgumentNullException>());
        }
    }
}