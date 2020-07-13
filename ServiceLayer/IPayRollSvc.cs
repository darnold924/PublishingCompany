using Entities;
using System.Collections.Generic;

namespace ServiceLayer
{
    public interface IPayRollSvc
    {
        List<DtoPayroll> GetAll();
        DtoPayroll Find(int id);
        DtoPayroll FindPayRollByAuthorId(int id);
        void Add(DtoPayroll dto);
        void Update(DtoPayroll dto);
        void Delete(DtoId dto);
    }
}

