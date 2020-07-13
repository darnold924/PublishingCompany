using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entities;
using DAL;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IDaoAuthor dao;
        public AuthorController(IDaoAuthor daoAuthor)
        {
            dao = daoAuthor;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<DtoAuthor>> GetAll()
        {
            return  await dao.GetAll();
        }

        [HttpGet]
        [Route("GetAuthorTypes")]
        public async Task<List<DtoAuthorType>> GetAuthorTypes()
        {
            return await dao.GetAuthorTypes();
        }

        [HttpGet]
        [Route("Find")]
        public async Task<DtoAuthor> Find(int id)
        {
            return await dao.Find(id);
        }

        [HttpPost]
        [Route("Add")]
        public async Task Add(DtoAuthor dto)
        {
             await dao.Add(dto);
        }

        [HttpPost]
        [Route("Update")]
        public async Task Update(DtoAuthor dto)
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