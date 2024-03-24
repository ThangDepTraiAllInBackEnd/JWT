using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Context.DatabaseContext
{
    public class MariaDbContext : IDBContext
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public MariaDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("MariaDb"));
        }

        /// <summary>
        /// Get all record in a table in Database 
        /// </summary>
        /// <returns>An Entity List with type T or null if not found</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<List<T>> GetAllAsync<T>()
        {
            var tableName = typeof(T).Name;
            var sql = $"Proc_{tableName}_GetAll";
            var dataList = await this.Connection.QueryAsync<T>(sql, commandType: CommandType.StoredProcedure);
            return dataList.ToList();
        }
        /// <summary>
        ///  Delete entity by Id
        /// </summary>
        /// <param name="entity">Entity to Delete </param>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<int> DeleteAsync<T>(T entity)
        {
            var tableName = typeof(T).Name;

            var parameters = new DynamicParameters();

            var propId = typeof(T).GetProperties()[0];
            var propName = propId.Name;
            var value = propId.GetValue(entity);
            parameters.Add($"@{propName}", value);

            var whereClause = $"{propName} = @{propName}";
            string sqlCommand = $"DELETE FROM {tableName} WHERE {whereClause}";

            return await this.Connection.ExecuteAsync(sqlCommand, parameters, Transaction);
        }

        /// <summary>
        /// Find entity by id with type T
        /// </summary>
        /// <param name="id">Entity's id to find </param>
        /// <returns>An Entity with type T or null if not found</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<T> GetByIdAsync<T>(Guid? id)
        {
            var tableName = typeof(T).Name;
            var propId = typeof(T).GetProperties()[0];
            var propName = propId.Name;
            var sql = $"SELECT * FROM {tableName} WHERE {propName} = @id";
            var parameters = new { id };
            return await this.Connection.QueryFirstOrDefaultAsync<T>(sql, parameters, Transaction);
        }


        /// <summary>
        /// Insert an employee
        /// </summary>
        /// <param name="employee">employee to insert </param>
        /// <returns>Number of record affect</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<int> InsertAsync<T>(T entity)
        {
            string tableName = typeof(T).Name;
            var parameters = new DynamicParameters();

            var props = typeof(T).GetProperties();
            // get all propName and propValue in entity
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var value = prop.GetValue(entity);
                parameters.Add($"@{propName}", value);
            }
            // Construct the name of the stored procedure
            string storedProcedureName = $"Proc_{tableName}_Insert";

            // Execute the stored procedure
            var res = await this.Connection.ExecuteAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: Transaction);
            return res;
        }

        /// <summary>
        ///  Update entity by Id
        /// </summary>
        /// <param name="entity">Entity to Update </param>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<int> UpdateAsync<T>(T entity)
        {
            string tableName = typeof(T).Name;
            var colNameList = new List<string>();
            var colValueList = new List<string>();
            var parameters = new DynamicParameters();

            var props = typeof(T).GetProperties();
            // get all propName and propValue in entity
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var value = prop.GetValue(entity);
                colNameList.Add(propName);
                colValueList.Add($"@{propName}");
                parameters.Add($"@{propName}", value);
            }
            var setClause = string.Join(", ", colNameList.Select(c => $"{c} = @{c}"));
            var whereClause = $"{colNameList[0]} = @{colNameList[0]}";
            string sqlCommand = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";
            return await this.Connection.ExecuteAsync(sqlCommand, parameters, Transaction);
        }

        /// <summary>
        ///  Delete all record in an table
        /// </summary>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<int> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  Get recoreds to paging by offset and next
        /// </summary>
        /// <param name="offSet">Record start </param>
        /// <param name="next">Number of record have to get </param>
        /// <param name="key">search key </param>
        /// <returns>List T was paging</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<List<T>> PagingAsync<T>(int offSet, int next, string key)
        {
            var tableName = typeof(T).Name;
            var sql = $"SELECT * FROM {tableName} LIMIT {offSet},{next}";
            var dataList = await this.Connection.QueryAsync<T>(sql);
            return dataList.ToList();
        }

        /// <summary>
        ///  Get number of page base table and page size
        /// </summary>
        /// <param name="pageSize">Record start </param>
        /// <param name="key">search key </param>
        /// <returns>List T was paging</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async Task<int> GetPageSizeAsync<T>(int pageSize, string key)
        {
            var tableName = typeof(T).Name;
            var sql = $"SELECT COUNT(*) FROM {tableName}";
            var countRecord = await this.Connection.QuerySingleAsync<int>(sql);
            return (int)Math.Ceiling((double)countRecord / pageSize);
        }
    }
}
