namespace King.Data.Sql.Reflection.Models
{
    using King.Mapper;

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
        [ActionName("Parameter")]
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
        #endregion
    }
}