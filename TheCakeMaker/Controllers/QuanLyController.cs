using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheCakeMaker.Models;

namespace TheCakeMaker.Controllers
{
    public class QuanLyController : Controller
    {
        TheCakeMakerContext db = new TheCakeMakerContext();
        // GET: QuanLy
        public ActionResult Index()
        {
            return View(db.Cakes.ToList());
        }

        
    }
}