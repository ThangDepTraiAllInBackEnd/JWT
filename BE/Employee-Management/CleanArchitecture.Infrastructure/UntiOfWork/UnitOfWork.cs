using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanArchitecture.Core.Resources
{
    public class UnitOfWork : IUnitOfWork
    {
        IDBContext _dbContext;
        bool _disposed;

        public IEmployeeRepository Employee { get; }

        public IDepartmentRepository Department { get; }

        public UnitOfWork(IDBContext dBContex, IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            this._dbContext = dBContex;
            Employee = employeeRepository;
            Department = departmentRepository;
        }
        /// <summary>
        /// Begin a Trasaction
        /// created by: Nguyễn Thiện Thắng
        /// created date: 2023/11/9
        /// </summary>
        public void BeginTransaction()
        {
            _dbContext.Connection.Open();
            _dbContext.Transaction = _dbContext.Connection.BeginTransaction();
        }

        /// <summary>
        /// commit Trasaction's data
        /// created by: Nguyễn Thiện Thắng
        /// created date: 2023/11/9
        /// </summary>
        public void Commit()
        {
            _dbContext.Transaction.Commit();
        }

        /// <summary>
        /// Close connection to db
        /// created by: Nguyễn Thiện Thắng
        /// created date: 2023/11/9
        /// </summary>
        public void Dispose()
        {
            _dbContext.Connection.Close();
        }

        /// <summary>
        /// Roll back Trasaction's data
        /// created by: Nguyễn Thiện Thắng
        /// created date: 2023/11/9
        /// </summary>
        public void RollBack()
        {
            _dbContext.Transaction.Rollback();
        }
    }
}
