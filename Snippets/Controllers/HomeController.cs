using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippets.Models;

namespace Snippets.Controllers
{
    public class HomeController : Controller
    {
        private SnippetsDBContext db = new SnippetsDBContext();

        public ActionResult Index()
        {
            List<Snippet> snippets = db.Snippets.ToList();

            return View(snippets);
        }


    }
}
