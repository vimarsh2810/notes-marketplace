using InitialNotesMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InitialNotesMarketplace.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            User loggedInUser = (User) Session["user"];
            //System.Diagnostics.Debug.WriteLine(loggedInUser.Firstname);
            return View(loggedInUser);
        }
    }
}