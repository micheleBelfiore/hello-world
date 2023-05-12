using ContosoUniversity.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContosoUniversity.Services
{
    public class TempTableHandler
    {
        private readonly SchoolContext _Context;
        private readonly IModel _Model;
        private readonly DatabaseFacade _Database;
        private SchoolContext schoolContext;
        private IModel model;
        private DatabaseFacade database;
        private Func<DbSet<MyTempTable>> _Set;

        public TempTableHandler(SchoolContext context, IModel model, DatabaseFacade Database, Func<DbSet<MyTempTable>> set) { 
            _Context = context;
            _Model = model;
            _Database = Database;
            _Set = set;
        }


        private string GetCreateTableSql(
       bool createPk)
        {


            var sqlGenHelper = _Context.GetService<ISqlGenerationHelper>();
            var entity = _Model.FindEntityType(typeof(MyTempTable));
            var tableName = entity.GetViewName();
            var escapedTableName = sqlGenHelper.DelimitIdentifier(tableName);
            var idProperty = entity.FindProperty(nameof(MyTempTable.Id));
            var columnName = idProperty.GetColumnName();
            var escapedColumnName = sqlGenHelper.DelimitIdentifier(columnName);
            var columnType = idProperty.GetColumnType();
            var nullability = idProperty.IsNullable ? "NULL" : "NOT NULL";

            var pkSql = createPk ? $", PRIMARY KEY ({escapedColumnName})" : null;


            var sql = $@"
                        CREATE TABLE {escapedTableName}
                        (
                           {escapedColumnName} {columnType} {nullability}
                           {pkSql}
                        );";

            return sql;


        }
        public IQueryable<int> getMyTempTable
        => _Set()
                   .FromSqlRaw($"SELECT * FROM #MyTempTable")
                   .Select(t => t.Id);

        private SqlBulkCopy GetSqlBulkCopy()
        {

            var sqlGenHelper = _Context.GetService<ISqlGenerationHelper>();

            SqlConnection sqlCon = (SqlConnection)_Database.GetDbConnection();

            var entity = _Model.FindEntityType(typeof(MyTempTable));
            var tableName = entity.GetViewName();
            var escapedTableName = sqlGenHelper.DelimitIdentifier(tableName);

            var idProperty = entity.FindProperty(nameof(MyTempTable.Id));
            var idColumnName = idProperty.GetColumnName();

            return new SqlBulkCopy(sqlCon)
            {
                DestinationTableName = escapedTableName,
                ColumnMappings =
             {
                new SqlBulkCopyColumnMapping(0, idColumnName)
             }
            };
        }

        public async Task CreateMyTempTableAsync(
        bool createPk,
        CancellationToken cancellationToken = default)
        {
           /* var sql = GetCreateTableSql(createPk);
            await _Database.OpenConnectionAsync(cancellationToken);

            try
            {
                await _Database.ExecuteSqlRawAsync(sql, cancellationToken);
            }
            catch (Exception)
            {
                _Database.CloseConnection();
                throw;
            }*/
        }
        public async Task BulkInsertIntoMyTempTableAsync(
       IEnumerable<int> values,
       CancellationToken cancellationToken = default)
        {
            await _Database.OpenConnectionAsync(cancellationToken);

            using var bulkCopy = GetSqlBulkCopy();



            try
            {
                using var reader = new MyTempTableDataReader(values);
                await bulkCopy.WriteToServerAsync(reader, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception)
            {
                _Database.CloseConnection();
                throw;
            }
        }

        public async Task<bool> DeleteMyTempTableAsync(CancellationToken cancellationToken = default)
        {
            var sql = "drop table dbo.MyTempTables";
            await _Database.OpenConnectionAsync(cancellationToken);

            try
            {
                await _Database.ExecuteSqlRawAsync(sql, cancellationToken);
                return true;
            }
            catch (Exception)
            {
                _Database.CloseConnection();
                throw;
            }

        }

    }
}
