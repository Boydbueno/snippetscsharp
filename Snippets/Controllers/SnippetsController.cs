using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippets.Models;

namespace Snippets.Controllers
{
    public class SnippetsController : Controller
    {

        private SnippetsDBContext db = new SnippetsDBContext();

        //
        // GET: /Snippets/Create

        public ActionResult Create()
        {
            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label");
            return View();
        }

        //
        // POST: /Snippets/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                db.Snippets.Add(snippet);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label", snippet.VisibilityId);
            return View(snippet);
        }

    }
}
