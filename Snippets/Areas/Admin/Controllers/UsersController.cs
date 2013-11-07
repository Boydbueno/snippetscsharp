using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snippets.Areas.Admin.Models;
using Snippets.Areas.Admin.Models.ViewModels;
using Snippets.Models;
using WebMatrix.WebData;

namespace Snippets.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Admin/Users/

        public ActionResult Index()
        {
            return View(db.UserProfiles.ToList());
        }

        //
        // GET: /Admin/Users/Details/5

        public ActionResult Details(int id = 0)
        {
            UserRoles userRoles = new UserRoles();

            userRoles.userProfile = db.UserProfiles.Find(id);
            if (userRoles.userProfile == null)
            {
                return RedirectToAction("Index");
            }

            userRoles.roles = Roles.GetRolesForUser(userRoles.userProfile.UserName);

            return View(userRoles);
        }

        //
        // GET: /Admin/Users/Create

        public ActionResult Create()
        {

            CreateUser createUser = new CreateUser();

            createUser.registerModel = new RegisterModel();
            createUser.roles = Roles.GetAllRoles();

            return View(createUser);
        }

        //
        // POST: /Admin/Users/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUser createUser)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(createUser.registerModel.UserName, createUser.registerModel.Password);

                Roles.AddUserToRoles(createUser.registerModel.UserName, createUser.roles);
                
                return RedirectToAction("Index");
            }

            return View(createUser);
        }

        //
        // GET: /Admin/Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Admin/Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        //
        // GET: /Admin/Users/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Admin/Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);

            if (Roles.GetRolesForUser(userprofile.UserName).Length > 0)
            {
                DeleteUserRoles(userprofile.UserName);
            }

            db.UserProfiles.Remove(userprofile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void DeleteUserRoles(string username)
        {
            foreach (string role in Roles.GetRolesForUser(username))
            {
                Roles.RemoveUserFromRole(username, role);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}