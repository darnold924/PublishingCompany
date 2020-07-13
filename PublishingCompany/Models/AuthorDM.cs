using System.Linq;
using ServiceLayer;
using Entities;

namespace PublishingCompany.Models
{
    public class AuthorDM :IAuthorDM
    {
        private IAuthorSvc svc;
        public AuthorDM(IAuthorSvc authorSvc)
        {
            svc = authorSvc;
        }

        public AuthorVM GetAll()
        {
            var vm = new AuthorVM();

            var dtos = svc.GetAll().ToList();

            vm.Authors.AddRange(dtos.Select(dto => new AuthorVM.Author()
            {
                AuthorID = dto.AuthorId,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            }).ToList());

            return vm;
        }

        public AuthorVM.Author Find(int id)
        {
            var dto = svc.Find(id);

            var author = new AuthorVM.Author
            {
                AuthorID = dto.AuthorId,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            return author;
        }

        public AuthorVM.Author Add()
        {
            return new AuthorVM.Author();
        }

        public void Add(AuthorVM.Author author)
        {
            var dto = new DtoAuthor
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            svc.Add(dto);
        }

        public AuthorVM.Author Update(int id)
        {
            var dto = Find(id);

            var author = new AuthorVM.Author
            {
                AuthorID = dto.AuthorID,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            return author;
        }

        public void Update(AuthorVM.Author author)
        {
            var dto = new DtoAuthor
            {
                AuthorId = author.AuthorID,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            svc.Update(dto);
        }

        public void Delete(int id)
        {
            var dto = new DtoId
            {
                Id = id
            };

            svc.Delete(dto);
        }
       
    }
}
