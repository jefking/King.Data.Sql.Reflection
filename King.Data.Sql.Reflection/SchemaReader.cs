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
    public class SchemaReader : ISchemaReader
    {
        #region Members
        /// <summary>
        /// Sql Executor
        /// </summary>
        protected readonly IExecutor executor = null;

        /// <summary>
        /// Definition Comparer
        /// </summary>
        protected static readonly EqualityComparer<IDefinition> comparer = new DefinitionComparer();

        /// <summary>
        /// Sql Statements
        /// </summary>
        protected readonly IStatements statements = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Connection String</param>
        public SchemaReader(string connectionString)
            :this(new Executor(new SqlConnection(connectionString)), new Statements())
        {
        }

        /// <summary>
        /// Mockable Constructor
        /// </summary>
        public SchemaReader(IExecutor executor, IStatements statements)
        {
            if (null == executor)
            {
                throw new ArgumentNullException("executor");
            }
            if (null == statements)
            {
                throw new ArgumentNullException("statements");
            }

            this.executor = executor;
            this.statements = statements;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Load data from Database, and return the models.
        /// </summary>
        /// <returns>Schemas to process</returns>
        public virtual async Task<IEnumerable<IDefinition>> Load(SchemaTypes type = SchemaTypes.StoredProcedure)
        {
            var schemas = await this.Schemas(type);

            var definitions = this.Minimize(schemas);

            return this.BuildManifest(definitions, schemas);
        }

        /// <summary>
        /// Load Schemas from data source
        /// </summary>
        /// <returns>Schemas</returns>
        public virtual async Task<IEnumerable<ISchema>> Schemas(SchemaTypes type = SchemaTypes.StoredProcedure)
        {
            var sql = this.statements.Get(type);

            var ds = await this.executor.Query(sql);
            return ds.Models<Schema>();
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
        public virtual IEnumerable<IDefinition> BuildManifest(IEnumerable<IDefinition> definitions, IEnumerable<ISchema> schemas)
        {
            if (null == definitions)
            {
                throw new ArgumentNullException("definitions");
            }
            if (null == schemas)
            {
                throw new ArgumentNullException("schemas");
            }

            var manifest = new List<IDefinition>();
            foreach (var d in definitions)
            {
                d.Variables = from s in schemas
                              where s.Name == d.Name
                                  && s.Preface == d.Preface
                                  && !string.IsNullOrWhiteSpace(s.ParameterName)
                                  && !string.IsNullOrWhiteSpace(s.DataType)
                              select s.Map<Variable>();

                manifest.Add(d);
            }

            return manifest;
        }
        #endregion
    }
}