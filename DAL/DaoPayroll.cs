using DAL.Models;
using System;
using System.Threading.Tasks;
using Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class DaoPayroll :IDaoPayroll
    {
        private PublishingCompanyContext pc;
        private IDaoAuthor _daoauthor;

        public DaoPayroll(PublishingCompanyContext dbcontext, IDaoAuthor daoAuthor)
        {
            pc = dbcontext;
            _daoauthor = daoAuthor;
        }

        public async Task<List<DtoPayroll>> GetAll()
        {
            var dtos = new List<DtoPayroll>();

            var payrolls = await pc.Payroll.ToListAsync();

            foreach (var payroll in payrolls)
            {
                var dtoauthor = await _daoauthor.Find(payroll.AuthorId); 

                var dto = new DtoPayroll
                {
                    PayrollId = payroll.PayrollId,
                    AuthorId = payroll.AuthorId,
                    AuthorFirstName = dtoauthor.FirstName,
                    AuthorLastName = dtoauthor.LastName,
                    Salary = payroll.Salary
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<DtoPayroll> Find(int id)
        {
            var dto = new DtoPayroll();

            var payroll = await pc.Payroll.FindAsync(id);

            if (payroll != null)
            { 
                var dtoauthor = await _daoauthor.Find(payroll.AuthorId);

                if (dtoauthor != null)
                {
                    dto.PayrollId = payroll.PayrollId;
                    dto.AuthorId = payroll.AuthorId;
                    dto.AuthorFirstName = dtoauthor.FirstName;
                    dto.AuthorLastName = dtoauthor.LastName;
                    dto.Salary = payroll.Salary;
                }
                else
                {
                    throw new Exception($"Author with ID = {id} was not found.");
                }
            }
            else
            {
                throw new Exception($"Payroll with ID = {id} was not found.");
            }

            return dto;

        }

        public async Task<DtoPayroll> FindPayRollByAuthorId(int id)
        {
            var dto = new DtoPayroll();
            
            var payroll = await pc.Payroll.Where(a =>a.AuthorId == id).SingleOrDefaultAsync();

            if (payroll != null)
            {
                dto.PayrollId = payroll.PayrollId;
            }

            return dto;
        }

        public async Task Add(DtoPayroll dto)
        {
            var payroll = new Payroll
            {
                AuthorId = dto.AuthorId,
                Salary = dto.Salary
            };

            pc.Payroll.Add(payroll);
            await pc.SaveChangesAsync();

        }

        public async Task Update(DtoPayroll dto)
        {
            var payroll = new Payroll
            {
                PayrollId = dto.PayrollId,
                AuthorId = dto.AuthorId,
                Salary = dto.Salary
            };

            pc.Entry(payroll).State = EntityState.Modified;
            await pc.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var payroll =  pc.Payroll.Find(id);

            if (payroll != null)
            {
                pc.Payroll.Remove(payroll);
                await pc.SaveChangesAsync();
            }
        }

    }
}
