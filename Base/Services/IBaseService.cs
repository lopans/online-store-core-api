using Base.DAL;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Services
{
    public interface IBaseService<T> where T: IBaseEntity
    {
        Task<T> CreateAsync(IUnitOfWork unitOfWork, T obj);
        Task<T> Update(IUnitOfWork unitOfWork, T obj);
        void Delete(IUnitOfWork unitOfWork, T obj, bool setHidden = true);
        void Delete(IUnitOfWork unitOfWork, int id, bool setHidden = true);
        void ChangeSortOrder(IUnitOfWork unitOfWork, T obj, int posId);
        T CreateDefault(IUnitOfWork unitOfWork);
        IQueryable<T> GetAll(IUnitOfWork unitOfWork, bool hidden = false);
    }
}
