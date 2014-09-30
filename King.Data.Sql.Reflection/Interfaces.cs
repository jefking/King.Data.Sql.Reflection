namespace King.Data.Sql.Reflection
{
    using King.Data.Sql.Reflection.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #region ISchemaReader
    /// <summary>
    /// Data Loader
    /// </summary>
    public interface ISchemaReader
    {
        #region Methods
        /// <summary>
        /// Load Manifest (From Data Store)
        /// </summary>
        /// <returns>Manifest</returns>
        Task<IEnumerable<IDefinition>> Load(SchemaTypes type = SchemaTypes.StoredProcedure);
        #endregion
    }
    #endregion

    #region IStatements
    /// <summary>
    /// SQL Statements Interface
    /// </summary>
    public interface IStatements
    {
        #region Methods
        /// <summary>
        /// Get SQL Schema Statement
        /// </summary>
        /// <param name="type">Schema Type</param>
        /// <returns>Schema Select Statement</returns>
        string Get(SchemaTypes type = SchemaTypes.StoredProcedure);
        #endregion
    }
    #endregion
}