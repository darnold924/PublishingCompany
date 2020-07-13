using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private IDaoArticle dao;
        public ArticleController(IDaoArticle daoArticle)
        {
            dao = daoArticle;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<DtoArticle>> GetAll()
        {
            return await dao.GetAll();
        }

        [HttpGet]
        [Route("GetArticlesByAuthorId")]
        public async Task<List<DtoArticle>> GetArticlesByAuthorId(int id)
        {
            return await dao.GetArticlesByAuthorId(id);
        }


        [HttpGet]
        [Route("Find")]
        public async Task<DtoArticle> Find(int id)
        {
            return await dao.Find(id);
        }

        [HttpPost]
        [Route("Add")]
        public async Task Add(DtoArticle dto)
        {
            await dao.Add(dto);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(DtoArticle dto)
        {
            await dao.Update(dto);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task Delete(DtoId dto)
        {
            await dao.Delete(dto.Id);
        }
    }
}