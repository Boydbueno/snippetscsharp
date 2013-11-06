using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snippets.Areas.Admin.Models;
using Snippets.Filters;

namespace Snippets.Areas.Admin.Controllers
{
    [InitializeSimpleMembership]
    public class RolesController : Controller
    {
        //
        // GET: /Admin/UserRoles/

        public ActionResult Index()
        {
            List<Role> userRoles = new List<Role>();

            string[] roles = Roles.GetAllRoles();

            foreach (string role in roles)
            {
                userRoles.Add(new Role(role));
            }

            return View(userRoles);
        }

        // 
        // GET: /Admin/UserRoles/Create

        public ActionResult Create()
        {
            return View();
        }

        // 
        // POST: /Admin/UserRoles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Role Role)
        {
            if (Roles.RoleExists(Role.Name))
            {
                // TODO: Error message
                return View();
            }

            Roles.CreateRole(Role.Name);

            return RedirectToAction("Index");
        }

    }
}
