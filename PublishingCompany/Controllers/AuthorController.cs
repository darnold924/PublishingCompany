using Microsoft.AspNetCore.Mvc;
using PublishingCompany.Models;

namespace PublishingCompany.Controllers
{
    public class AuthorController : Controller
    {
        private IAuthorDM adm;
        public AuthorController(IAuthorDM authorDM)
        {
            adm = authorDM;
        }

        public IActionResult Index()
        {
            return View(adm.GetAll());
        }

        public IActionResult Detail(int id = 0)
        {
            return id == 0 ? null : View(adm.Find(id));
        }

        public IActionResult Create()
        {
            return View(adm.Add());
        }

        [HttpPost]
        public ActionResult Create(AuthorVM.Author author, string submit)
        {
            if (submit == "Cancel") return RedirectToAction("Index");

            if (!ModelState.IsValid) return View(author);

            adm.Add(author);
            return RedirectToAction("Index");
        }
        
        public ActionResult Edit(int id = 0)
        {
            return id == 0 ? null : View(adm.Update(id));
        }
        
        [HttpPost]
        public ActionResult Edit(AuthorVM.Author author, string submit)
        {
            if (submit == "Cancel") return RedirectToAction("Index");
                        
            if (!ModelState.IsValid) return View(author);
                       
            adm.Update(author);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id = 0)
        {
            if (id > 0) adm.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}