namespace King.Data.Sql.Reflection.Unit.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class StatementsTests
    {
        [Test]
        public void StoredProcedures()
        {
            var sql = Statements.StoredProcedures;
            Assert.IsTrue(sql.Contains("[Parameter]"));
            Assert.IsTrue(sql.Contains("[DataType]"));
            Assert.IsTrue(sql.Contains("[Schema]"));
            Assert.IsTrue(sql.Contains("[Name]"));
            Assert.IsTrue(sql.Contains("[MaxLength]"));
        }

        [Test]
        public void Tables()
        {
            var sql = Statements.Tables;
            Assert.IsTrue(sql.Contains("[Parameter]"));
            Assert.IsTrue(sql.Contains("[DataType]"));
            Assert.IsTrue(sql.Contains("[Schema]"));
            Assert.IsTrue(sql.Contains("[Name]"));
            Assert.IsTrue(sql.Contains("[MaxLength]"));
        }
    }
}