using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class ResistrationController : Controller
    {
        private StudentDBEntities db = new StudentDBEntities();

        // GET: Resistration
        public ActionResult Index()
        {
            var resistrations = db.resistrations.Include(r => r.batch).Include(r => r.course);
            return View(resistrations.ToList());
        }

        // GET: Resistration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resistration resistration = db.resistrations.Find(id);
            if (resistration == null)
            {
                return HttpNotFound();
            }
            return View(resistration);
        }

        // GET: Resistration/Create
        public ActionResult Create()
        {
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1");
            ViewBag.course_id = new SelectList(db.courses, "id", "course1");
            return View();
        }

        // POST: Resistration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] resistration resistration)
        {
            if (ModelState.IsValid)
            {
                db.resistrations.Add(resistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", resistration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", resistration.course_id);
            return View(resistration);
        }

        // GET: Resistration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resistration resistration = db.resistrations.Find(id);
            if (resistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", resistration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", resistration.course_id);
            return View(resistration);
        }

        // POST: Resistration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firstname,lastname,nic,course_id,batch_id,telno")] resistration resistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batch_id = new SelectList(db.batches, "id", "batch1", resistration.batch_id);
            ViewBag.course_id = new SelectList(db.courses, "id", "course1", resistration.course_id);
            return View(resistration);
        }

        // GET: Resistration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            resistration resistration = db.resistrations.Find(id);
            if (resistration == null)
            {
                return HttpNotFound();
            }
            return View(resistration);
        }

        // POST: Resistration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            resistration resistration = db.resistrations.Find(id);
            db.resistrations.Remove(resistration);
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
    }
}
