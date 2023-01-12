using ClinicService.Models;

namespace ClinicService.Services
{
    public interface IRepository<T>
    {
        IList<T> GetAll();

        T GetById(int id);

        int Create(T item);

        int Update(T item);  

        int Delete(int item);  

    }
}
