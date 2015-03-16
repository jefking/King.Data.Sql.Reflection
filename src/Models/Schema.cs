namespace King.Data.Sql.Reflection.Models
{
    /// <summary>
    /// Schema
    /// </summary>
    public class Schema : ISchema
    {
        #region Properties
        /// <summary>
        /// Parameter Name
        /// </summary>
        public virtual string ParameterName
        {
            get;
            set;
        }

        /// <summary>
        /// Data Type
        /// </summary>
        public virtual string DataType
        {
            get;
            set;
        }

        /// <summary>
        /// Preface
        /// </summary>
        public virtual string Preface
        {
            get;
            set;
        }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Maximum Length
        /// </summary>
        public virtual string MaxLength
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