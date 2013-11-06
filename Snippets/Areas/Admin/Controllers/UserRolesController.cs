﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Snippets.Areas.Admin.Models;

namespace Snippets.Areas.Admin.Controllers
{
    public class UserRolesController : Controller
    {
        //
        // GET: /Admin/UserRoles/

        public ActionResult Index()
        {
            List<UserRole> userRoles = new List<UserRole>();

            string[] roles = Roles.GetAllRoles();

            foreach (string role in roles)
            {
                userRoles.Add(new UserRole(role));
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
        public ActionResult Create(UserRole Role)
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
