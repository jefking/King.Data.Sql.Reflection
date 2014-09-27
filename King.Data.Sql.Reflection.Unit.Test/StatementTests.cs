namespace King.Data.Sql.Reflection.Unit.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class StatementTests
    {
        [Test]
        public void StoredProcedures()
        {
            var sql = Statement.StoredProcedures;
            Assert.IsTrue(sql.Contains("[Parameter]"));
            Assert.IsTrue(sql.Contains("[DataType]"));
            Assert.IsTrue(sql.Contains("[Schema]"));
            Assert.IsTrue(sql.Contains("[StoredProcedure]"));
            Assert.IsTrue(sql.Contains("[MaxLength]"));
        }
    }
}