using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDaoPayroll
    {
        Task<List<DtoPayroll>> GetAll();
        Task<DtoPayroll> Find(int id);
        Task<DtoPayroll> FindPayRollByAuthorId(int id);
        Task Add(DtoPayroll dto);
        Task Update(DtoPayroll dto);
        Task Delete(int id);
    }
}
