using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Interfaces;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace CleanArchitecture.Infrastructure.Context
{
    public class DBContext<T> : IBaseRepository<T>, IDisposable where T : class
    {

        protected IDBContext _dbContext;
        public DBContext(IDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Get all record in a table in Database 
        /// </summary>
        /// <returns>An Entity List with type T or null if not found</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.GetAllAsync<T>();
        }

        /// <summary>
        ///  Delete all record in an table
        /// </summary>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2
        public async Task<int> DeleteAllAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Find entity by id
        /// </summary>
        /// <param name="id">Entity's id to find </param>
        /// <returns>An Entity with type T or null if not found</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2s
        public async Task<T> GetByIdAsync(Guid? id)
        {
            return await _dbContext.GetByIdAsync<T>(id);
        }

        /// <summary>
        /// Get all record in a table in Database 
        /// </summary>
        /// <param name="entity">Entity to insert </param>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2
        public virtual async Task<int> InsertAsync(T entity)
        {
            if (_dbContext.Transaction == null)
            {
                _dbContext.Connection.Open();
                _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            }
            var res = await _dbContext.InsertAsync<T>(entity);
            //_dbContext.Transaction.Commit();
            return res;
        }
        /// <summary>
        ///  Update entity by Id
        /// </summary>
        /// <param name="entity">Entity to Update </param>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2
        public async virtual Task<int> UpdateAsync(T entity)
        {
            if (_dbContext.Transaction == null)
            {
                _dbContext.Connection.Open();
                _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            }
            var res = await _dbContext.UpdateAsync<T>(entity);
            //_dbContext.Transaction.Commit();
            return res;
        }

        /// <summary>
        ///  Delete entity by Id
        /// </summary>
        /// <param name="entity">Entity to Delete </param>
        /// <returns>nummber of record affected</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2
        public async Task<int> DeleteAsync(T entity)
        {
            if (_dbContext.Transaction == null)
            {
                _dbContext.Connection.Open();
                _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
            }
            var res = await _dbContext.DeleteAsync<T>(entity);
            //_dbContext.Transaction.Commit();
            return res;
        }
        /// <summary>
        ///  Close all opening connection 
        /// </summary>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2
        public void Dispose()
        {
            _dbContext.Connection.Close();
        }
        /// <summary>
        ///  Get recoreds to paging by offset and next
        /// </summary>
        /// <param name="offSet">Record start </param>
        /// <param name="next">Number of record have to get </param>
        /// <returns>List T was paging</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created_at: 2023/12/2
        public async virtual Task<List<T>> PagingAsync(int offSet, int next, string key)
        {
            return await _dbContext.PagingAsync<T>(offSet, next, key);
        }

        /// <summary>
        ///  Get number of page base table and page size
        /// </summary>
        /// <param name="pageSize">Record start </param>
        /// <param name="key">key to search </param>
        /// <returns>number of page base table and page size</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async virtual Task<int> GetPageSizeAsync<T>(int pageSize, string key)
        {
            return await _dbContext.GetPageSizeAsync<T>(pageSize, key);
        }


    }
}
