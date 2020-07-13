using System.Collections.Generic;
using Entities;

namespace ServiceLayer
{
    public interface IAuthorSvc
    {
        List<DtoAuthor> GetAll();
        List<DtoAuthorType> GetAuthorTypes();
        DtoAuthor Find(int id);
        void Add(DtoAuthor dto);
        void Update(DtoAuthor dto);
        void Delete(DtoId dto);
    }
}
