using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OOZAuthServerSample.SharedLibrary.Dto;

namespace OOZAuthServereSample.Core.Service
{
    public interface IServiceGeneric<TEntity,TDto> where TEntity:class where TDto:class
    {
        Task<Response<TDto>> GetByIdAsync(int id);

        Task<Response<IEnumerable<TDto>>> GetAllAsync();

        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

        Task<Response<TDto>> AddAsync(TDto entity);

        Task<Response<NoDataDto>> Remove(int id);

        Task<Response<NoDataDto>> Update(TDto entity,int id);
    }
}
