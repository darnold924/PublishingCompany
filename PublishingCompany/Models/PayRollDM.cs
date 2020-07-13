using ServiceLayer;
using System.Linq;
using Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PublishingCompany.Models
{
    public class PayRollDM : IPayRollDM
    {
        private IPayRollSvc svc;
        private IAuthorSvc svcauth;
        public PayRollDM(IPayRollSvc payRollSvc, IAuthorSvc authorSvc)
        {
            svc = payRollSvc;
            svcauth = authorSvc;
        }

        public PayRollVM GetAll()
        {
            var vm = new PayRollVM();

            var dtos = svc.GetAll().ToList();

            vm.Payrolls.AddRange(dtos.Select(dto => new PayRollVM.Payroll()
            {
                PayrollId = dto.PayrollId,
                AuthorId = dto.AuthorId,
                AuthorFirstName = dto.AuthorFirstName,
                AuthorLastName = dto.AuthorLastName,
                Salary = dto.Salary
            }).ToList());

            return vm;
        }

        public PayRollVM.Payroll Find(int id)
        {
            var dto = svc.Find(id);

            var payroll = new PayRollVM.Payroll
            {
                PayrollId = dto.PayrollId,
                AuthorId = dto.AuthorId,
                AuthorFirstName = dto.AuthorFirstName,
                AuthorLastName = dto.AuthorLastName,
                Salary = dto.Salary
            };

            return payroll;
        }
        public bool BlnFindPayRollByAuthorId(int id)
        {
            bool blnflag = false;

            var dto = svc.FindPayRollByAuthorId(id);

            if (dto.PayrollId != 0)
            {
                blnflag = true;
            }

            return blnflag;
        }
        public PayRollVM.Payroll Add()
        {
            return PopulateSelectedList( new PayRollVM.Payroll());
        }

        public void Add(PayRollVM.Payroll payroll)
        {
            var dto = new DtoPayroll
            {
                AuthorId = int.Parse(payroll.AuthorTypeId),
                Salary = payroll.Salary
            };

            svc.Add(dto);
        }

        public PayRollVM.Payroll Update(int id)
        {
            var dto = Find(id);

            var payroll = new PayRollVM.Payroll
            {
                PayrollId = dto.PayrollId,
                AuthorId = dto.AuthorId,
                AuthorFirstName = dto.AuthorFirstName,
                AuthorLastName = dto.AuthorLastName,
                Salary = dto.Salary
            };

            return payroll;
        }

        public void Update(PayRollVM.Payroll payroll)
        {
            var dto = new DtoPayroll
            {
                PayrollId = payroll.PayrollId,
                AuthorId = payroll.AuthorId,
                Salary = payroll.Salary
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

        public PayRollVM.Payroll PopulateSelectedList(PayRollVM.Payroll payroll)
        {
            var dtos = svcauth.GetAuthorTypes().ToList();

            payroll.AuthorTypes.AddRange(dtos.Select(dto => new SelectListItem()
            {
                Value = dto.Value,
                Text = dto.Text
            }).ToList());

            var selected = (from a in payroll.AuthorTypes.Where(a => a.Value == payroll.AuthorTypeId) select a)
                .SingleOrDefault();

            if (selected != null)
                selected.Selected = true;

            return payroll;

        }
    }
}
