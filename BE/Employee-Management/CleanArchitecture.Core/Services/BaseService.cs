using CleanArchitecture.Core.DTOs;
using CleanArchitecture.Core.Exeptions;
using CleanArchitecture.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected IBaseRepository<T> _baseRepository;
        public BaseService(IBaseRepository<T> baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        /// <summary>
        /// insert by repository || Check is all entity' properies is valid before insert
        /// </summary>
        /// <param name="entity">Entity to check </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/1/10
        public async virtual Task<ServiceResult> InsertServiceAsync(T entity)
        {
            {
                int res = await _baseRepository.InsertAsync(entity);
                if (res > 0)
                {
                    return new ServiceResult()
                    {
                        Data = res,
                        Success = true,
                        Code = System.Net.HttpStatusCode.Created,
                        DevMsg = Resources.MsgResource_VN.AddSuccess,
                        UserMsg = Resources.MsgResource_VN.AddSuccess
                    };
                }
                throw new InternalServerErrorCustomException(Resources.MsgResource_VN.AddErr);
            }
        }

        /// <summary>
        /// update by repository || Check is all entity' properies is valid before update
        /// </summary>
        /// <param name="entity">Entity to check </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/1/10
        public async virtual Task<ServiceResult> UpdateServiceAsync(T entity)
        {
            int res = await _baseRepository.UpdateAsync(entity);
            if (res > 0)
            {
                return new ServiceResult()
                {
                    Data = res,
                    Success = true,
                    Code = System.Net.HttpStatusCode.OK,
                    DevMsg = Resources.MsgResource_VN.UpdateSuccess,
                    UserMsg = Resources.MsgResource_VN.UpdateSuccess
                };
            }
            throw new InternalServerErrorCustomException(Resources.MsgResource_VN.UpdateErr);
        }

        /// <summary>
        /// delete by repository || Check is all entity' properies is valid before delete
        /// </summary>
        /// <param name="entity">Entity to check </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async virtual Task<ServiceResult> DeleteServiceAsync(T entity)
        {
            int res = await _baseRepository.DeleteAsync(entity);
            if (res > 0)
            {
                return new ServiceResult()
                {
                    Data = res,
                    Success = true,
                    Code = System.Net.HttpStatusCode.OK,
                    DevMsg = Resources.MsgResource_VN.DeleteSucess,
                    UserMsg = Resources.MsgResource_VN.DeleteSucess
                };
            }
            throw new InternalServerErrorCustomException(Resources.MsgResource_VN.DeleteErr);
        }

        /// <summary>
        /// Action after insert entity into database
        /// </summary>
        /// <param name="entity">entity to manipulate </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        protected virtual ServiceResult AfterInsert(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To set new Id before insert an entity into database
        /// </summary>
        /// <param name="entity">entity to set new id  </param>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        protected virtual void SetNewId(T entity)
        {
            var className = typeof(T).Name;
            var prop = typeof(T).GetProperty($"{className}Id");
            if (prop != null && (prop.PropertyType == typeof(Guid) || prop.PropertyType == typeof(Guid?)))
            {
                prop.SetValue(entity, Guid.NewGuid());
            }
        }

        /// <summary>
        /// Action before insert entity into database
        /// </summary>
        /// <param name="entity">Entity to manipulate</param>
        /// <returns>
        /// true- entity valid to insert
        /// false-  entity valid not to insert
        /// </returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/1/9
        public async virtual Task<bool> BeforeInsertAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Action before update entity into database
        /// </summary>
        /// <param name="entity">Entity to manipulate</param>
        /// <returns>
        /// true- entity valid to update
        /// false-  entity valid not to update
        /// </returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/1/9
        public async virtual Task<bool> BeforeUpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Paging records base page and pageSize
        /// </summary>
        /// <param name="page">Current page </param>
        ///  <param name="pageSize">record'number /page </param>
        ///  <param name="key">search key </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2024/15/1
        public async virtual Task<ServiceResult> PagingServiceAsync(int page, int pageSize, string key)
        {
            // page have to > 0
            if (page < 1)
            {
                throw new BadRequestCustomException(CleanArchitecture.Core.Resources.MsgResource_VN.PagingErr);
            }
            List<T> pagingList = await _baseRepository.PagingAsync((page - 1) * pageSize, pageSize, key);
            if (pagingList.Count >= 0)
            {
                Page<T> pageObject = new Page<T>()
                {
                    ListRecord = pagingList,
                    CurrentPage = page,
                    TotalPage = await _baseRepository.GetPageSizeAsync<T>(pageSize, key)
                };
                return new ServiceResult()
                {
                    Data = pageObject,
                    Success = true,
                    Code = System.Net.HttpStatusCode.OK,
                    DevMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess,
                    UserMsg = CleanArchitecture.Core.Resources.MsgResource_VN.GetSuccess
                };
            }
            throw new InternalServerErrorCustomException(CleanArchitecture.Core.Resources.MsgResource_VN.GetErr);
        }

        /// <summary>
        /// validate before return data for user
        /// </summary>
        /// <param name="entity">Entity to check </param>
        /// <returns>Service result ( sucsess or failed with all details )</returns>
        ///  created by: Nguyễn Thiện Thắng
        ///  created at: 2023/12/2
        public async virtual Task<ServiceResult> GetAllServiceAsync()
        {
            throw new NotImplementedException();
        }
    }
}
