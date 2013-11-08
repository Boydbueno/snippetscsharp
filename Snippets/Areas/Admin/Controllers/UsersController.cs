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
    public class UsersController : AdminController
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

            UserProfile userProfile = db.UserProfiles.Find(id);

            if (userProfile == null)
            {
                return HttpNotFound();
            }

            ViewBag.roles = getSelectedRoleData(userProfile.UserName);

            return View(userProfile);
        }

        private List<SelectedRoleData> getSelectedRoleData(string username)
        {
            List<SelectedRoleData> viewModel = new List<SelectedRoleData>();

            string[] roles = Roles.GetAllRoles();
            string[] userRoles = Roles.GetRolesForUser(username);

            foreach (string role in roles)
            {
                viewModel.Add(new SelectedRoleData
                {
                    Role = role,
                    Selected = userRoles.Contains(role)
                });
            }
            return viewModel;
        }

        //
        // POST: /Admin/Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userProfile, string[] roles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userProfile).State = EntityState.Modified;
                UpdateUserRoles(userProfile.UserName, roles);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(userProfile);
        }

        private void UpdateUserRoles(string username, string[] roles)
        {

            if (roles == null)
            {
                roles = new string[0];
            }

            // Loop through all existing roles.
            foreach (string role in Roles.GetAllRoles())
            {

                // If the role is in the checked array and user doesn't have it yet.
                if (roles.Contains(role))
                {
                    if (!Roles.GetRolesForUser(username).Contains(role))
                    {
                        // Add the role to the user
                        Roles.AddUserToRole(username, role);
                    }
                }
                else
                {
                    if (Roles.GetRolesForUser(username).Contains(role))
                    {
                        // remove it
                        Roles.RemoveUserFromRole(username, role);
                    }
                }

            }
        }

        //
        // GET: /Admin/Users/Delete/5

        public ActionResult Delete(int id = 0)
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