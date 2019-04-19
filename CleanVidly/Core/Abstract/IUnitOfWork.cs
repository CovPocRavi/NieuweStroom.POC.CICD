using System.Threading.Tasks;

namespace NieuweStroom.POC.IT.Core.Abstract
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync();

    }
}