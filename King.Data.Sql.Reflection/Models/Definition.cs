namespace King.Data.Sql.Reflection.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Definition
    /// </summary>
    public class Definition : IDefinition
    {
        #region Properties
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
        /// Variables
        /// </summary>
        public virtual IEnumerable<IVariable> Variables
        {
            get;
            set;
        }
        #endregion
    }
}