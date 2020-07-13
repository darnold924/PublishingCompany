using System.Linq;
using Entities;
using ServiceLayer;

namespace PublishingCompany.Models
{
    public class ArticleDM : IArticleDM
    {
        private IArticleSvc svc;
        public ArticleDM(IArticleSvc articleSvc)
        {
            svc = articleSvc;
        }

        public ArticleVM GetAll()
        {
            var vm = new ArticleVM();

            var dtos = svc.GetAll().ToList();

            vm.Articles.AddRange(dtos.Select(dto => new ArticleVM.Article()
            {
                ArticleId = dto.ArticleId,
                AuthorId = dto.AuthorId,
                Title = dto.Title,
                Body = dto.Body
            }).ToList());

            return vm;
        }

        public ArticleVM GetArticlesByAuthorId(int id)
        {
            var vm = new ArticleVM();

            var dtos = svc.GetArticlesByAuthorId(id).ToList();

            vm.Articles.AddRange(dtos.Select(dto => new ArticleVM.Article()
            {
                ArticleId = dto.ArticleId,
                AuthorId = dto.AuthorId,
                AuthorName = dto.AuthorName,
                Title = dto.Title,
                Body = dto.Body
            }).ToList());

            return vm;
        }
        public ArticleVM.Article Find(int id)
        {
            var dto = svc.Find(id);

            var article = new ArticleVM.Article
            {
                ArticleId = dto.ArticleId,
                AuthorId = dto.AuthorId,
                Title = dto.Title,
                Body = dto.Body
            };

            return article;
        }

        public ArticleVM.Article Add(int id)
        {
            return new ArticleVM.Article{AuthorId = id};
        }

        public void Add(ArticleVM.Article article)
        {
            var dto = new DtoArticle
            {
                AuthorId = (int) article.AuthorId,
                Title = article.Title,
                Body = article.Body
            };

            svc.Add(dto);
        }

        public ArticleVM.Article Update(int id)
        {
            var dto = Find(id);

            var article = new ArticleVM.Article
            {
                ArticleId = dto.ArticleId,
                AuthorId = dto.AuthorId,
                Title = dto.Title,
                Body = dto.Body
            };

            return article;
        }

        public void Update(ArticleVM.Article article)
        {
            var dto = new DtoArticle
            {
                ArticleId = article.ArticleId,
                AuthorId = article.AuthorId,
                Title = article.Title,
                Body = article.Body
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
