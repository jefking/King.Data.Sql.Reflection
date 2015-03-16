namespace King.Data.Sql.Reflection.Models
{
    /// <summary>
    /// Variable
    /// </summary>
    public class Variable : IVariable
    {
        #region Properties
        /// <summary>
        /// Data Type
        /// </summary>
        public virtual string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// Parameter Name
        /// </summary>
        public virtual string ParameterName
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum Length
        /// </summary>
        public virtual int MaxLength
        {
            get;
            set;
        }

        /// <summary>
        /// Is Primary Key
        /// </summary>
        public virtual bool IsPrimaryKey
        {
            get;
            set;
        }
        #endregion
    }
}