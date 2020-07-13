using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DaoArticle :IDaoArticle
    {
        private PublishingCompanyContext pc;
        private IDaoAuthor _daoAuthor;

        public DaoArticle(PublishingCompanyContext dbcontext, IDaoAuthor daoAuthor)
        { 
            pc = dbcontext;
            _daoAuthor = daoAuthor;
        }
        public async Task<List<DtoArticle>> GetAll()
        {
            var dtos = new List<DtoArticle>();

            var articles = await pc.Article.ToListAsync();

            dtos.AddRange(articles.Select(article => new DtoArticle()
            {
                ArticleId = article.ArticleId,
                AuthorId = article.AuthorId,
                Title = article.Title,
                Body = article.Body
            }).ToList());

            return dtos;
        }

        public async Task<List<DtoArticle>> GetArticlesByAuthorId(int id)
        {
            var dtos = new List<DtoArticle>();

            var articles = await pc.Article.Where(a => a.AuthorId.ToString().Contains(id.ToString())).ToListAsync();
           
            foreach (var article in articles)
            {
                var intid = (int)article.AuthorId;

                var dtoauthor = await _daoAuthor.Find(intid);

                var dto = new DtoArticle
                {
                    ArticleId = article.ArticleId,
                    AuthorId = article.AuthorId,
                    AuthorName = dtoauthor.LastName +", " + dtoauthor.FirstName,
                    Title = article.Title,
                    Body = article.Body
                };

                dtos.Add(dto);
            }
             
            return dtos;
        }
        public async Task<DtoArticle> Find(int id)
        {
            var dto = new DtoArticle();

            var article = await pc.Article.FindAsync(id);
            
            if (article != null)
            {
                dto.ArticleId = article.ArticleId;
                dto.AuthorId = article.AuthorId;
                dto.Title = article.Title;
                dto.Body = article.Body;
            }
            else
            {
                throw new Exception($"Article with ID = {id} was not found.");
            }

            return dto;

        }

        public async Task Add(DtoArticle dto)
        {
            var article = new Models.Article
            {
                AuthorId = dto.AuthorId,
                Title = dto.Title,
                Body = dto.Body
            };

            pc.Article.Add(article);
            await pc.SaveChangesAsync();

        }

        public async Task Update(DtoArticle dto)
        {
            var article = new Article
            {
                ArticleId = dto.ArticleId,
                AuthorId = dto.AuthorId,
                Title = dto.Title,
                Body = dto.Body
            };

            pc.Entry(article).State = EntityState.Modified;
            await pc.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var article = pc.Article.Find(id);

            if (article != null)
            {
                pc.Article.Remove(article);
                await pc.SaveChangesAsync();
            }
        }

    }
}

