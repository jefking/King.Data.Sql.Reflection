namespace King.Data.Sql.Reflection
{
    /// <summary>
    /// SQL Statements
    /// </summary>
    public struct Statement
    {
        #region Members
        /// <summary>
        /// SQL Stored Procedures Statement
        /// </summary>
        public const string StoredProcedures = @"SELECT parm.name AS [Parameter]
                                                , typ.name AS [DataType]
                                                , SPECIFIC_SCHEMA AS [Schema]
                                                , SPECIFIC_NAME AS [StoredProcedure]
                                                , CASE parm.max_length WHEN -1 THEN 2147483647 ELSE parm.max_length END AS [MaxLength]
                                            FROM sys.procedures sp WITH(NOLOCK) LEFT OUTER JOIN sys.parameters parm WITH(NOLOCK) ON sp.object_id = parm.object_id
                                            INNER JOIN [information_schema].[routines] WITH(NOLOCK) ON routine_type = 'PROCEDURE'
	                                                AND ROUTINE_NAME not like 'sp_%diagram%'
	                                                AND sp.name = SPECIFIC_NAME
                                            LEFT OUTER JOIN sys.types typ WITH(NOLOCK) ON parm.system_type_id = typ.system_type_id
	                                            AND typ.name <> 'sysname'
	                                            AND typ.is_user_defined = 0
                                            ORDER BY SPECIFIC_NAME, SPECIFIC_SCHEMA";
        #endregion
    }
}