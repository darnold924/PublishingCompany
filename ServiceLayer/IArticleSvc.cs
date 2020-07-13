using System.Collections.Generic;
using Entities;

namespace ServiceLayer
{
    public interface IArticleSvc
    {
        List<DtoArticle> GetAll();
        List<DtoArticle> GetArticlesByAuthorId(int id);
        DtoArticle Find(int id);
        void Add(DtoArticle dto);
        void Update(DtoArticle dto);
        void Delete(DtoId dto);
    }
}
