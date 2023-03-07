using PetShelter.DataAccessLayer.Models;
using System.Linq.Expressions;

namespace PetShelter.DataAccessLayer.Repository
{
    public interface IBaseRepository<T> where T : IEntity
    {
        Task Add(T entity);
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task Update(T entity);
        Task Delete(T entity);
    }
}