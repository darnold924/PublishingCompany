namespace PublishingCompany.Models
{
    public interface IArticleDM
    {
        ArticleVM GetAll();
        ArticleVM GetArticlesByAuthorId(int id);
        ArticleVM.Article Find(int id);
        ArticleVM.Article Add(int id);
        void Add(ArticleVM.Article author);
        ArticleVM.Article Update(int id);
        void Update(ArticleVM.Article author);
        void Delete(int id);
    }
}
