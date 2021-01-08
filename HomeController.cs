using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication15.Models;

namespace WebApplication15.Controllers
{
    public class HomeController : Controller
    {
        newDBLibraryEntities DB = new newDBLibraryEntities();
        // GET: Home
        public ActionResult Index()
        {
            var members = DB.Members.ToList();
            return View(members);
        }

        public ActionResult Create()
        {
            var list = DB.Countries.ToList();
            var countries = list.Select(a => new SelectListItem { Text = a.Country1, Value = a.ID.ToString() }).ToList();
            ViewBag.Countries = countries;
            return View();

        }

        [HttpPost]
        public ActionResult Create(Member newmember)
        {
            if (ModelState.IsValid)
            {
                DB.Members.Add(newmember);
                DB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Details(string memberid)
        {
            var books = DB.Borroweds.Where(a => a.memb_no == memberid).ToList();
            ViewBag.Books = books.Select(a => new SelectListItem { Text = a.Book.title }).ToList();

            var mem = DB.Members.Where(a => a.memb_no == memberid).SingleOrDefault();
            ViewBag.Name = mem != null ? mem.name.ToUpper() : "N/A";
            return View();


        }
    }
}