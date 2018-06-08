using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheCakeMaker;
using TheCakeMaker.Models;

namespace TheCakeMaker.Controllers
{
    public class CakesController : Controller
    {
        private TheCakeMakerContext db = new TheCakeMakerContext();

        // GET: Cakes
        public ActionResult Index()
        {
            var cakes = db.Cakes.Include(c => c.Type);
            return View(cakes.ToList());
        }

        // GET: Cakes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cake cake = db.Cakes.Find(id);
            if (cake == null)
            {
                return HttpNotFound();
            }
            return View(cake);
        }

        // GET: Cakes/Create
        public ActionResult Create()
        {
            ViewBag.TypeId = new SelectList(db.Types, "id", "name");
            return View();
        }

        // POST: Cakes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,TypeId,detail,price,image")] Cake cake)
        {
            if (ModelState.IsValid)
            {
                db.Cakes.Add(cake);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeId = new SelectList(db.Types, "id", "name", cake.TypeId);
            return View(cake);
        }

        // GET: Cakes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cake cake = db.Cakes.Find(id);
            if (cake == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeId = new SelectList(db.Types, "id", "name", cake.TypeId);
            return View(cake);
        }

        // POST: Cakes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,TypeId,detail,price,image")] Cake cake)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cake).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeId = new SelectList(db.Types, "id", "name", cake.TypeId);
            return View(cake);
        }

        // GET: Cakes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cake cake = db.Cakes.Find(id);
            if (cake == null)
            {
                return HttpNotFound();
            }
            return View(cake);
        }

        // POST: Cakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cake cake = db.Cakes.Find(id);
            db.Cakes.Remove(cake);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public PartialViewResult CakePartial()
        {
            var listCake = db.Cakes.Take(16).ToList();
            return PartialView(listCake);
        }

        public ViewResult Detail(int id=0)
        {
            Cake cake = db.Cakes.SingleOrDefault(n=>n.id==id);
            if(cake == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(cake);
        }
    }
}
