﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippets.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public abstract class AdminController : Controller {}
}
