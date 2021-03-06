﻿namespace King.Data.Sql.Reflection
{
    using System;

    /// <summary>
    /// SQL Statements
    /// </summary>
    public class Statements : IStatements
    {
        #region Members
        /// <summary>
        /// SQL Stored Procedures Statement
        /// </summary>
        public const string StoredProcedures = @"SELECT [parm].[name] AS [ParameterName]
                                                , [typ].[name] AS [DataType]
                                                , [SPECIFIC_SCHEMA] AS [Preface]
                                                , [SPECIFIC_NAME] AS [Name]
                                                , CASE [parm].[max_length] WHEN -1 THEN 2147483647 ELSE [parm].[max_length] END AS [MaxLength]
                                            FROM sys.procedures sp WITH(NOLOCK)
                                            LEFT OUTER JOIN sys.parameters parm WITH(NOLOCK) ON sp.object_id = parm.object_id
                                            INNER JOIN [information_schema].[routines] WITH(NOLOCK) ON routine_type = 'PROCEDURE'
	                                                AND ROUTINE_NAME not like 'sp_%diagram%'
	                                                AND sp.name = SPECIFIC_NAME
                                            LEFT OUTER JOIN sys.types typ WITH(NOLOCK) ON parm.system_type_id = typ.system_type_id
	                                            AND typ.name <> 'sysname'
	                                            AND typ.is_user_defined = 0
                                            ORDER BY [SPECIFIC_NAME], [SPECIFIC_SCHEMA]";

        /// <summary>
        /// SQL Table Statement
        /// </summary>
        public const string Tables = @"SELECT [schema].[COLUMN_NAME] AS [ParameterName]
                                            , [schema].[DATA_TYPE] AS [DataType]
                                            , [schema].[TABLE_SCHEMA] AS [Preface]
                                            , [schema].[TABLE_NAME] AS [Name]
                                            , CASE [schema].[CHARACTER_MAXIMUM_LENGTH] WHEN -1 THEN 2147483647 ELSE [schema].[CHARACTER_MAXIMUM_LENGTH] END AS [MaxLength]
                                            , CASE [key].[TABLE_SCHEMA] WHEN [schema].[TABLE_SCHEMA] THEN 1 ELSE 0 END AS [IsPrimaryKey]
	                                    FROM [information_schema].[COLUMNS] [schema] WITH(NOLOCK)
											LEFT OUTER JOIN [INFORMATION_SCHEMA].KEY_COLUMN_USAGE [key] WITH(NOLOCK) ON [schema].[TABLE_NAME] = [key].[TABLE_NAME]
		                                            AND [schema].[COLUMN_NAME] = [key].[COLUMN_NAME]
		                                            AND [schema].[TABLE_SCHEMA] = [key].[TABLE_SCHEMA]
										WHERE [schema].[TABLE_NAME] <> '__RefactorLog'
                                        ORDER BY [schema].[TABLE_NAME], [schema].[TABLE_SCHEMA]";
        #endregion

        #region Methods
        /// <summary>
        /// Get SQL Schema Statement
        /// </summary>
        /// <param name="type">Schema Type</param>
        /// <returns>Schema Select Statement</returns>
        public virtual string Get(SchemaTypes type = SchemaTypes.StoredProcedure)
        {
            switch (type)
            {
                case SchemaTypes.StoredProcedure:
                    return StoredProcedures;
                case SchemaTypes.Table:
                    return Tables;
                default:
                    throw new ArgumentException("Unknown schema type.");
            }
        }
        #endregion
    }
}