using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOZAuthServereSample.Core.Repositories;
using OOZAuthServereSample.Core.Service;
using OOZAuthServereSample.Core.Service.UnitOfWork;
using OOZAuthServerSample.SharedLibrary.Dto;

namespace OOZAuthServerSample.Service.Services
{
    public class ServiceGeneric<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public ServiceGeneric(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            this._genericRepository = genericRepository;
            this._unitOfWork = unitOfWork;

        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.AddAsync(newEntity);

            await _unitOfWork.CommitAsync();

            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);

            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var models = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());

            return Response<IEnumerable<TDto>>.Success(models, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var model = await _genericRepository.GetByIdAsync(id);

            if (model == null)
            {

                return Response<TDto>.Fail("Id is not found", 404, true);
            }

            return Response<TDto>.Success(ObjectMapper.Mapper.Map<TDto>(model), 200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var isExist = await _genericRepository.GetByIdAsync(id);

            if (isExist == null)
            {
                return Response<NoDataDto>.Fail("Id is not found", 404, true);
            }

            _genericRepository.Remove(isExist);

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);
        }

        public async Task<Response<NoDataDto>> Update(TDto entity, int id)
        {
            var isExist = await _genericRepository.GetByIdAsync(id);

            if (isExist == null)
            {
                return Response<NoDataDto>.Fail("Id is not found", 404, true);
            }

            _genericRepository.Update(ObjectMapper.Mapper.Map<TEntity>(entity));

            await _unitOfWork.CommitAsync();

            return Response<NoDataDto>.Success(200);

        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _genericRepository.Where(predicate);

            List<TEntity> finalList = await entities.ToListAsync();

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(finalList), 200);
        }
    }
}
