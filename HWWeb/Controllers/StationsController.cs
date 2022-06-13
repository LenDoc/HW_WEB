using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HWWeb.Models;

namespace HWWeb.Controllers
{
    public class StationsController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index(string searchBy, string searchValue)
        {
           
            try
            {
             
                if (db.Station.ToList().Count == 0)
                {
                    TempData["InfoMessage"] = "Currently Station not" +
                        "available in DB.";
                    return View(db.Station.ToList());
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Please provide search value";
                        return View(db.Station.ToList());
                    }
                    else
                    {
                        if (searchBy.ToLower() == "name")
                        {
                            var searchByName =
                            db.Station.ToList().Where(n => n.Name.ToLower().Contains(
                                    searchValue.ToLower()));
                            if (searchByName.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchByName);
                        }

                    }

                }
                return View(db.Station.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Station.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StationID,Name")] Station station)
        {
            if (ModelState.IsValid)
            {
                db.Station.Add(station);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(station);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Station.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StationID,Name")] Station station)
        {
            if (ModelState.IsValid)
            {
                db.Entry(station).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station station = db.Station.Find(id);
            if (station == null)
            {
                return HttpNotFound();
            }
            return View(station);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Station station = db.Station.Find(id);
            db.Station.Remove(station);
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
