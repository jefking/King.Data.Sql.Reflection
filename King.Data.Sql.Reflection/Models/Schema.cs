namespace King.Data.Sql.Reflection.Models
{
    using King.Mapper;

    /// <summary>
    /// Schema
    /// </summary>
    public class Schema : ISchema
    {
        #region Properties
        /// <summary>
        /// Parameter
        /// </summary>
        public virtual string Parameter
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
        #endregion
    }
}