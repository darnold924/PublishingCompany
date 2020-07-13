using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DaoAuthor : IDaoAuthor
    {
        private PublishingCompanyContext pc;

        public DaoAuthor(PublishingCompanyContext dbcontext)
        {
            pc = dbcontext;
        }

        public async Task<List<DtoAuthor>> GetAll()
        {
            var dtos = new List<DtoAuthor>();

            var authors =  await pc.Author.ToListAsync();

            dtos.AddRange(authors.Select(author => new DtoAuthor()
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName
            }).ToList());

            return dtos;
        }

        public async Task<DtoAuthor> Find(int id)
        {
            var dto = new DtoAuthor();

            var author =  await pc.Author.FindAsync(id);

            if (author != null)
            {
                dto.AuthorId = author.AuthorId;
                dto.FirstName = author.FirstName;
                dto.LastName = author.LastName;
            }
            else
            {
                throw new Exception($"Author with ID = {id} was not found.");
            }

            return dto;

        }
        public async Task <List<DtoAuthorType>> GetAuthorTypes()
        {
            var dtos = new List<DtoAuthorType>();

            var authors = await pc.Author.ToListAsync();

            foreach (var author in authors)
            {
                DtoAuthorType dto = new DtoAuthorType
                {
                    AuthorId = author.AuthorId,
                    Value = author.AuthorId.ToString(),
                    Text = author.LastName + ", " + author.FirstName
                };

                dtos.Add(dto);
            }

            return dtos;
        }
        public async Task Add(DtoAuthor dto)
        {
            var author = new Author
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            pc.Author.Add(author);
            await pc.SaveChangesAsync();

        }
        public async Task Update(DtoAuthor dto)
        {
            var author = new Author
            {
                AuthorId = dto.AuthorId,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            pc.Entry(author).State = EntityState.Modified;
            await pc.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            
            var author = pc.Author.Find(id);

            if (author != null)
            {
                var articles = await pc.Article.Where(a => a.AuthorId.ToString().Contains(id.ToString())).ToListAsync();
          
                foreach (var article in articles)
                {
                    author.Articles.Remove(article);
                }

                var payrolls = await pc.Payroll.Where(a => a.AuthorId.ToString().Contains(id.ToString())).ToListAsync();

                foreach (var payroll in payrolls)
                {
                    author.Payrolls.Remove(payroll);
                }

                pc.Author.Remove(author);
                await pc.SaveChangesAsync();
            }

        }

    }
}
