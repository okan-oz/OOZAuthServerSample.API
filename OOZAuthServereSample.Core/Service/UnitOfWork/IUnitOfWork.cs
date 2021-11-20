using System;
using System.Threading.Tasks;

namespace OOZAuthServereSample.Core.Service.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
