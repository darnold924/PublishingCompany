using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DAL;
using Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayRollController : ControllerBase
    {
        private IDaoPayroll dao;
        public PayRollController(IDaoPayroll daoPayroll)
        {
            dao = daoPayroll;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<DtoPayroll>> GetAll()
        {
            return await dao.GetAll();
        }

        [HttpGet]
        [Route("Find")]
        public async Task<DtoPayroll> Find(int id)
        {
            return await dao.Find(id);
        }


        [HttpGet]
        [Route("FindPayRollByAuthorId")]
        public async Task<DtoPayroll> FindPayRollByAuthorId(int id)
        {
            return await dao.FindPayRollByAuthorId(id);
        }

        [HttpPost]
        [Route("Add")]
        public async Task Add(DtoPayroll dto)
        {
            await dao.Add(dto);
        }

        [HttpPost]
        [Route("Update")]
        public async  Task Update(DtoPayroll dto)
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