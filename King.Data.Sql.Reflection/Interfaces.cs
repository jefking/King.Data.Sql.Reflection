namespace King.Data.Sql.Reflection
{
    using King.Data.Sql.Reflection.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #region IDataLoader
    /// <summary>
    /// Data Loader
    /// </summary>
    public interface IDataLoader
    {
        #region Methods
        /// <summary>
        /// Load Manifest (From Data Store)
        /// </summary>
        /// <returns>Manifest</returns>
        Task<IDictionary<int, IDefinition>> Load();
        #endregion
    }
    #endregion

    #region IStatements
    /// <summary>
    /// 
    /// </summary>
    public interface IStatements
    {
        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string Get(SchemaTypes type = SchemaTypes.StoredProcedure);
        #endregion
    }
    #endregion
}