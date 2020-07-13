using Microsoft.AspNetCore.Mvc;
using PublishingCompany.Models;

namespace PublishingCompany.Controllers
{
    public class ArticleController : Controller
    {
        private IAuthorDM _authorDM;
        private IArticleDM _articleDM;
        public ArticleController(IAuthorDM authorDM, IArticleDM articleDM)
        {
            _authorDM = authorDM;
            _articleDM = articleDM;
        }
        public IActionResult Index() 
        {
            return View(_authorDM.GetAll());
        }
        public IActionResult ArticleIndex(int id)
        {
            TempData["AuthorId"] = id;

            return View(_articleDM.GetArticlesByAuthorId(id));
        }
        public ActionResult Create()
        {
            return View(_articleDM.Add((int)TempData["AuthorId"]));
        }

        [HttpPost]
        public ActionResult Create(ArticleVM.Article  article, string submit)
        {
            TempData["AuthorId"] = article.AuthorId;

            if (submit == "Cancel") return RedirectToAction("ArticleIndex", new { id = (int)TempData["AuthorId"] });

            if (!ModelState.IsValid) return View(article);

            int authorid = article.AuthorId;
                
            _articleDM.Add(article);
            return RedirectToAction("ArticleIndex", new {id = (int) TempData["AuthorId"]});
        }
        public ActionResult Edit(int id = 0)
        {
            return id == 0 ? null : View(_articleDM.Update(id));
        }

        [HttpPost]
        public ActionResult Edit(ArticleVM.Article article, string submit)
        {
            TempData["AuthorId"] = article.AuthorId;

            if (submit == "Cancel") return RedirectToAction("ArticleIndex", new { id = (int)TempData["AuthorId"] });

            if (!ModelState.IsValid) return View(article);

            _articleDM.Update(article);
            return RedirectToAction("ArticleIndex", new { id = (int)TempData["AuthorId"] });
        }
    
        public IActionResult Delete(int id = 0)
        {
            if (id > 0) _articleDM.Delete(id);

            return RedirectToAction("ArticleIndex", new {id = (int)TempData["AuthorId"] });
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
    }
}