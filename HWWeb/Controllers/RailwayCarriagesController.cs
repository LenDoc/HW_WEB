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
    public class RailwayCarriagesController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index(string searchBy, string searchValue)
        {
            try
            {
          
                if (db.RailwayCarriage.ToList().Count == 0)
                {
                    TempData["InfoMessage"] = "Currently Raiway Carriage not" +
                        "available in DB.";
                    return View(db.RailwayCarriage.ToList());
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Please provide search value";
                        return View(db.RailwayCarriage.ToList());
                    }
                    else
                    {
                        if (searchBy.ToLower() == "type")
                        {
                            var searchByType =
                               db.RailwayCarriage.ToList().Where(n => n.Type.ToLower().Contains(
                                    searchValue.ToLower()));
                            if (searchByType.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchByType);
                        }
      

                        if (searchBy.ToLower() == "numberseats")
                        {
                            var searchByNumber =
                               db.RailwayCarriage.ToList().Where(n => n.NumberSeats == int.Parse(searchValue));
                            if (searchByNumber.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchByNumber);
                        }

                    }

                }
                return View(db.RailwayCarriage.ToList());
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
            RailwayCarriage railwayCarriage = db.RailwayCarriage.Find(id);
            if (railwayCarriage == null)
            {
                return HttpNotFound();
            }
            return View(railwayCarriage);
        }

   
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RailwayCarriageID,Type,NumberSeats")] RailwayCarriage railwayCarriage)
        {
            if (ModelState.IsValid)
            {
                db.RailwayCarriage.Add(railwayCarriage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(railwayCarriage);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RailwayCarriage railwayCarriage = db.RailwayCarriage.Find(id);
            if (railwayCarriage == null)
            {
                return HttpNotFound();
            }
            return View(railwayCarriage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RailwayCarriageID,Type,NumberSeats")] RailwayCarriage railwayCarriage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(railwayCarriage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(railwayCarriage);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RailwayCarriage railwayCarriage = db.RailwayCarriage.Find(id);
            if (railwayCarriage == null)
            {
                return HttpNotFound();
            }
            return View(railwayCarriage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RailwayCarriage railwayCarriage = db.RailwayCarriage.Find(id);
            db.RailwayCarriage.Remove(railwayCarriage);
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
