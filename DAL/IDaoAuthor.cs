using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public interface IDaoAuthor
    {
        Task<List<DtoAuthor>> GetAll();
        Task<DtoAuthor> Find(int id);
        Task<List<DtoAuthorType>> GetAuthorTypes();
        Task Add(DtoAuthor dto);
        Task Update(DtoAuthor dto);
        Task  Delete(int id);
    }
}
