﻿namespace King.Data.Sql.Reflection.Unit.Test
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class StatementsTests
    {
        [Test]
        public void StoredProcedures()
        {
            var sql = Statements.StoredProcedures;
            Assert.IsTrue(sql.Contains("[ParameterName]"));
            Assert.IsTrue(sql.Contains("[DataType]"));
            Assert.IsTrue(sql.Contains("[Preface]"));
            Assert.IsTrue(sql.Contains("[Name]"));
            Assert.IsTrue(sql.Contains("[MaxLength]"));
        }

        [Test]
        public void GetStoredProcedures()
        {
            var statement = new Statements();
            Assert.AreEqual(Statements.StoredProcedures, statement.Get());
            Assert.AreEqual(Statements.StoredProcedures, statement.Get(SchemaTypes.StoredProcedure));
        }

        [Test]
        public void Tables()
        {
            var sql = Statements.Tables;
            Assert.IsTrue(sql.Contains("[ParameterName]"));
            Assert.IsTrue(sql.Contains("[DataType]"));
            Assert.IsTrue(sql.Contains("[Preface]"));
            Assert.IsTrue(sql.Contains("[Name]"));
            Assert.IsTrue(sql.Contains("[MaxLength]"));
            Assert.IsTrue(sql.Contains("[IsPrimaryKey]"));
        }

        [Test]
        public void GetTables()
        {
            var statement = new Statements();
            Assert.AreEqual(Statements.Tables, statement.Get(SchemaTypes.Table));
        }

        [Test]
        public void GetUnknown()
        {
            var statement = new Statements();
            Assert.That(() => statement.Get(SchemaTypes.Unknown), Throws.TypeOf<ArgumentException>());
        }
    }
}