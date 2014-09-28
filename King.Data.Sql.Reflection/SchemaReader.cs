namespace King.Data.Sql.Reflection
{
    using King.Data.Sql.Reflection.Models;
    using King.Mapper;
    using King.Mapper.Data;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Schema Reader
    /// </summary>
    public class SchemaReader : IDataLoader
    {
        #region Members
        /// <summary>
        /// Connection String
        /// </summary>
        private readonly string connectionString = null;

        /// <summary>
        /// Data Loader
        /// </summary>
        private readonly ILoader<Schema> loader = null;

        /// <summary>
        /// Definition Comparer
        /// </summary>
        private static readonly EqualityComparer<IDefinition> comparer = new DefinitionComparer();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        public SchemaReader(string connectionString)
            :this(connectionString, new Loader<Schema>())
        {
        }

        /// <summary>
        /// Mockable Constructor
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        /// <param name="loader">Loader</param>
        public SchemaReader(string connectionString, ILoader<Schema> loader)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("connectionString");
            }
            if (null == loader)
            {
                throw new ArgumentNullException("loader");
            }

            this.connectionString = connectionString;
            this.loader = loader;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load data from Database, and return the models.
        /// </summary>
        /// <returns>Schemas to process</returns>
        public virtual async Task<IDictionary<int, IDefinition>> Load()
        {
            var schemas = await this.Schemas();

            var definitions = this.Minimize(schemas);

            return this.BuildManifest(definitions, schemas);
        }

        /// <summary>
        /// Load Schemas from data source
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<ISchema>> Schemas()
        {
            IEnumerable<ISchema> schemas = null;
            using (var connection = new SqlConnection(connectionString))
            using (var execute = new SqlCommand(Statements.StoredProcedures, connection))
            {
                await connection.OpenAsync();

                schemas = loader.Models(execute);
            }

            return schemas;
        }

        /// <summary>
        /// Minimize Schemas, product definintions
        /// </summary>
        /// <param name="schemas"></param>
        /// <returns>Definitions</returns>
        public virtual IEnumerable<IDefinition> Minimize(IEnumerable<ISchema> schemas)
        {
            if (null == schemas)
            {
                throw new ArgumentNullException("schemas");
            }

            return schemas.Select(s => s.Map<Definition>()).Distinct(comparer);
        }

        /// <summary>
        /// Build Manifest, from definitions and schemas
        /// </summary>
        /// <param name="definitions">Definitions</param>
        /// <param name="schemas">Schemas</param>
        /// <returns>Manifest</returns>
        public virtual IDictionary<int, IDefinition> BuildManifest(IEnumerable<IDefinition> definitions, IEnumerable<ISchema> schemas)
        {
            if (null == definitions)
            {
                throw new ArgumentNullException("definitions");
            }
            if (null == schemas)
            {
                throw new ArgumentNullException("schemas");
            }

            var manifest = new Dictionary<int, IDefinition>();
            foreach (var d in definitions)
            {
                d.Variables = from s in schemas
                              where s.Name == d.Name
                                  && s.Preface == d.Preface
                                  && !string.IsNullOrWhiteSpace(s.Parameter)
                                  && !string.IsNullOrWhiteSpace(s.DataType)
                              select s.Map<Variable>();

                manifest.Add(comparer.GetHashCode(d), d);
            }

            return manifest;
        }
        #endregion
    }
}