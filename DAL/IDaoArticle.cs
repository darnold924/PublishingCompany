using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
    public interface IDaoArticle
    {
        Task<List<DtoArticle>> GetAll();
        Task<List<DtoArticle>> GetArticlesByAuthorId(int id);
        Task<DtoArticle> Find(int id);
        Task Add(DtoArticle dto);
        Task Update(DtoArticle dto);
        Task Delete(int id);
    }
}
