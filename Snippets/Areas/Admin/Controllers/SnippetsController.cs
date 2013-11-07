using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippets.Models;
using Snippets.Areas.Admin.Models.ViewModels;

namespace Snippets.Areas.Admin.Controllers
{
    public class SnippetsController : AdminController
    {
        private SnippetsDBContext db = new SnippetsDBContext();

        //
        // GET: /Admin/Snippets/

        public ActionResult Index()
        {
            var snippets = db.Snippets.Include(s => s.Visibility);
            return View(snippets.ToList());
        }

        //
        // GET: /Admin/Snippets/Details/5

        public ActionResult Details(int id = 0)
        {
            Snippet snippet = db.Snippets.Find(id);

            if (snippet == null)
            {
                return HttpNotFound();
            }

            return View(snippet);
        }

        //
        // GET: /Admin/Snippets/Create

        public ActionResult Create()
        {
            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label");
            return View();
        }

        //
        // POST: /Admin/Snippets/Create

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
        // GET: /Admin/Snippets/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Snippet snippet = db.Snippets.Find(id);
            if (snippet == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label", snippet.VisibilityId);
            ViewBag.Tags = getSelectedTagData(snippet);

            return View(snippet);
        }

        private List<SelectedTagData> getSelectedTagData(Snippet snippet)
        {
            List<Tag> allTags = db.Tags.ToList();
            var snippetTags = snippet.Tags.Select(t => t.ID);
            var viewModel = new List<SelectedTagData>();
            foreach (var tag in allTags)
            {
                viewModel.Add(new SelectedTagData
                {
                    TagId = tag.ID,
                    Label = tag.Label,
                    Selected = snippetTags.Contains(tag.ID)
                });
            }
            return viewModel;
        }

        //
        // POST: /Admin/Snippets/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formCollection, string[] selectedTags)
        {
            // Eager load snippet with it's tags
            Snippet snippet = db.Snippets
                    .Include(s => s.Tags)
                    .Where(s => s.ID == id)
                    .Single();

            if (ModelState.IsValid)
            {

                UpdateModel(snippet);

                UpdateSnippetTags(selectedTags, snippet);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.VisibilityId = new SelectList(db.Visibilities, "ID", "Label", snippet.VisibilityId);
            ViewBag.Tags = getSelectedTagData(snippet);

            return View(db.Snippets.Find(id));
        }

        private void UpdateSnippetTags(string[] selectedTags, Snippet snippetToUpdate)
        {
            if (selectedTags == null)
            {
                snippetToUpdate.Tags = new List<Tag>();
                return;
            }

            var tagIds = snippetToUpdate.Tags.Select(t => t.ID);

            foreach (Tag tag in db.Tags)
            {
                if (selectedTags.Contains(tag.ID.ToString()))
                {
                    if (!tagIds.Contains(tag.ID))
                    {
                        snippetToUpdate.Tags.Add(tag);
                    }
                }
                else
                {
                    if (tagIds.Contains(tag.ID))
                    {
                        snippetToUpdate.Tags.Remove(tag);
                    }
                }
   
            }

        }

        //
        // GET: /Admin/Snippets/Delete/5

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
        // POST: /Admin/Snippets/Delete/5

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