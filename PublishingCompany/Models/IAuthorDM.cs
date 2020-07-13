namespace PublishingCompany.Models
{
    public interface IAuthorDM
    {
        AuthorVM GetAll();
        AuthorVM.Author Find(int id);
        AuthorVM.Author Add();
        void Add(AuthorVM.Author author);
        AuthorVM.Author Update(int id);
        void Update(AuthorVM.Author author);
        void Delete(int id);
    }
}
