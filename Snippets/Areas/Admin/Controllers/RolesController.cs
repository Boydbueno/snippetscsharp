using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snippets.Areas.Admin.Models;
using Snippets.Areas.Admin.Models.ViewModels;
using Snippets.Filters;

namespace Snippets.Areas.Admin.Controllers
{
    [InitializeSimpleMembership]
    public class RolesController : AdminController
    {
        //
        // GET: /Admin/Roles/

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
        // GET: /Admin/Roles/Create

        public ActionResult Create()
        {
            return View();
        }

        // 
        // POST: /Admin/Roles/Create

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

        //
        // GET: /Admin/Roles/Delete/rolename

        public ActionResult Delete(string id = null)
        {

            if (!Roles.RoleExists(id))
            {
                return RedirectToAction("Index");
            }


            RoleUsers roleUsers = new RoleUsers();
            
            roleUsers.Role = new Role(id);
            roleUsers.Users = Roles.GetUsersInRole(id);

            return View(roleUsers);
        }

        //
        // POST: /Admin/Roles/Delete/rolename

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string name)
        {

            if (!Roles.RoleExists(name))
            {
                ViewBag.Error = "Role has not been found";
                return RedirectToAction("Index");
            }

            if (Roles.GetUsersInRole(name).Length > 0)
            {
                ViewBag.Error = "There are users associated to this role. Change their roles first";
                RoleUsers roleUsers = new RoleUsers();
                roleUsers.Role = new Role(name);
                return View(roleUsers);
            }

            Roles.DeleteRole(name);

            return RedirectToAction("Index");
        }

    }
}
