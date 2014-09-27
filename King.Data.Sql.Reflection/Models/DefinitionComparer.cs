namespace King.Data.Sql.Reflection.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Definition Comparer
    /// </summary>
    public class DefinitionComparer : EqualityComparer<IDefinition>
    {
        #region Methods
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="x">Definition</param>
        /// <param name="y">Definition</param>
        /// <returns>Are Equal</returns>
        public override bool Equals(IDefinition x, IDefinition y)
        {
            if (null == x)
            {
                throw new ArgumentNullException("x");
            }
            if (null == y)
            {
                throw new ArgumentNullException("y");
            }

            return this.GetHashCode(x) == this.GetHashCode(y);
        }

        /// <summary>
        /// Get Hash Code
        /// </summary>
        /// <param name="obj">Definition</param>
        /// <returns>Hash Code</returns>
        public override int GetHashCode(IDefinition obj)
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return string.Format("{0}{1}", obj.Preface, obj.Name).GetHashCode();
        }
        #endregion
    }
}