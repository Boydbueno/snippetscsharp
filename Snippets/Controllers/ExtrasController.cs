using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippets.Areas.Admin.Models.ViewModels;
using Snippets.Models;
using Snippets.Models.ViewModels;

namespace Snippets.Controllers
{
    public class ExtrasController : Controller
    {
        private SnippetsDBContext db = new SnippetsDBContext();

        //
        // GET: /Extras/

        public ActionResult Index()
        {
            ExtraVM extraVM = new ExtraVM();

            List<SelectedTagData> selectedTagData = new List<SelectedTagData>();

            // For the sake of the 'demo' we'll just deselect all checkboxes
            foreach (Tag tag in db.Tags.ToList())
            {
                selectedTagData.Add(new SelectedTagData
                {
                    TagId = tag.ID,
                    Label = tag.Label,
                    Selected = false
                });
            }

            extraVM.Tags = selectedTagData;
            extraVM.Visibilies = db.Visibilities.ToList();

            return View(extraVM);
        }

    }
}
