namespace King.Data.Sql.Reflection.Models
{
    using System.Collections.Generic;

    #region ISchema
    /// <summary>
    /// Variable Interface
    /// </summary>
    public interface IVariable
    {
        #region Properties
        /// <summary>
        /// Data Type
        /// </summary>
        string DataType
        {
            get;
        }

        /// <summary>
        /// Parameter Name
        /// </summary>
        string ParameterName
        {
            get;
        }

        /// <summary>
        /// Maximum Length
        /// </summary>
        int MaxLength
        {
            get;
        }

        /// <summary>
        /// Is Primary Key
        /// </summary>
        bool IsPrimaryKey
        {
            get;
        }
        #endregion
    }
    #endregion

    #region ISchema
    /// <summary>
    /// Schema Interface
    /// </summary>
    public interface ISchema
    {
        #region Properties
        /// <summary>
        /// Parameter
        /// </summary>
        string ParameterName
        {
            get;
        }

        /// <summary>
        /// Data Type
        /// </summary>
        string DataType
        {
            get;
        }

        /// <summary>
        /// Preface
        /// </summary>
        string Preface
        {
            get;
        }

        /// <summary>
        /// Name
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Maximum Length
        /// </summary>
        string MaxLength
        {
            get;
        }

        /// <summary>
        /// Is Primary Key
        /// </summary>
        bool IsPrimaryKey
        {
            get;
        }
        #endregion
    }
    #endregion

    #region IDefinition
    /// <summary>
    /// Definition Interface
    /// </summary>
    public interface IDefinition
    {
        #region Properties
        /// <summary>
        /// Preface
        /// </summary>
        string Preface
        {
            get;
        }

        /// <summary>
        /// Name
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Variables
        /// </summary>
        IEnumerable<IVariable> Variables
        {
            get;
            set;
        }
        #endregion
    }
    #endregion
}