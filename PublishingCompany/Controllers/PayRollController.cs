using Microsoft.AspNetCore.Mvc;
using PublishingCompany.Models;

namespace PublishingCompany.Controllers
{
    public class PayRollController : Controller
    {
        private IPayRollDM pdm;
        public PayRollController(IPayRollDM payRollDM)
        {
            pdm = payRollDM;
        }
        public IActionResult Index()
        {
            return View(pdm.GetAll());
        }

        public IActionResult Detail(int id = 0)
        {
            return id == 0 ? null : View(pdm.Find(id));
        }

        public ActionResult Create()
        {
            return View(pdm.Add());
        }

        [HttpPost]
        public ActionResult Create(PayRollVM.Payroll payroll, string submit)
        {
            if (submit == "Cancel") return RedirectToAction("Index");
            
            if (!ModelState.IsValid) return View(pdm.PopulateSelectedList(payroll));

            if (pdm.BlnFindPayRollByAuthorId(int.Parse(payroll.AuthorTypeId)))
            {
                ModelState.AddModelError(string.Empty, "Author has an existing PayRoll record.");
            }

            if (!ModelState.IsValid) return View(pdm.PopulateSelectedList(payroll));

            pdm.Add(payroll);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id = 0)
        {
            return id == 0 ? null : View(pdm.Update(id));
        }

        [HttpPost]
        public ActionResult Edit(PayRollVM.Payroll payroll, string submit)
        {
            if (submit == "Cancel") return RedirectToAction("Index");

            if (!ModelState.IsValid) return View(payroll);

            pdm.Update(payroll);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            if (id > 0) pdm.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}