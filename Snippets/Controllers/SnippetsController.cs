using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippets.Models;
using Snippets.Models.ViewModels;

namespace Snippets.Controllers
{
    public class SnippetsController : Controller
    {
        private SnippetsDBContext db = new SnippetsDBContext();

        //
        // GET: /Snippets/

        public ActionResult Index()
        {
            var snippets = db.Snippets.Include(s => s.Visibility);
            return View(snippets.ToList());
        }

        //
        // GET: /Snippets/Details/5

        public ActionResult Details(int id = 0)
        {
            var viewModel = new SnippetDetails();

            viewModel.Snippet = db.Snippets.Find(id);

            if (viewModel.Snippet == null)
            {
                return HttpNotFound();
            }

            viewModel.Tags = viewModel.Snippet.Tags;

            return View(viewModel);
        }

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
                return RedirectToAction("Index");
            }

            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label", snippet.VisibilityId);
            return View(snippet);
        }

        //
        // GET: /Snippets/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Snippet snippet = db.Snippets.Find(id);
            if (snippet == null)
            {
                return HttpNotFound();
            }
            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label", snippet.VisibilityId);
            return View(snippet);
        }

        //
        // POST: /Snippets/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(snippet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label", snippet.VisibilityId);
            return View(snippet);
        }

        //
        // GET: /Snippets/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Snippet snippet = db.Snippets.Find(id);
            if (snippet == null)
            {
                return HttpNotFound();
            }
            return View(snippet);
        }

        //
        // POST: /Snippets/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Snippet snippet = db.Snippets.Find(id);
            db.Snippets.Remove(snippet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}