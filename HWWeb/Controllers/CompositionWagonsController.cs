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
    public class CompositionWagonsController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index(string searchBy, string searchValue)
        {
              var compositionWagons = db.CompositionWagons.Include(c => c.RailwayCarriage);
            
            try
            {


                if (compositionWagons.ToList().Count == 0)
                {
                    TempData["InfoMessage"] = "Currently CompositionWagons not" +
                        "available in DB.";
                    return View(compositionWagons.ToList());
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Please provide the search value";
                        return View(compositionWagons.ToList());
                    }
                    else
                    {
                        if (searchBy.ToLower() == "number")
                        {
                            var searchByNumber =
                                compositionWagons.ToList().
                                Where(n => n.Number == int.Parse(searchValue));
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
                        if (searchBy.ToLower() == "railwaycarriageid")
                        {

                            var searchByRailwayCarriage =
                                compositionWagons.ToList().
                                Where(n => n.RailwayCarriageID == int.Parse(searchValue));
                            if (searchByRailwayCarriage.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchByRailwayCarriage);
                        }

                    }

                }
                return View(compositionWagons.ToList());

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
            CompositionWagons compositionWagons = db.CompositionWagons.Find(id);
            if (compositionWagons == null)
            {
                return HttpNotFound();
            }
            return View(compositionWagons);
        }
        public ActionResult Create()
        {
            ViewBag.RailwayCarriageID = new SelectList(db.RailwayCarriage, "RailwayCarriageID", "Type");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompositionWagonsID,RailwayCarriageID,Number")] CompositionWagons compositionWagons)
        {
            if (ModelState.IsValid)
            {
                db.CompositionWagons.Add(compositionWagons);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            ViewBag.RailwayCarriageID = new SelectList(db.RailwayCarriage, "RailwayCarriageID", "Type", compositionWagons.RailwayCarriageID);
            return View(compositionWagons);
        }

        // GET: CompositionWagons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompositionWagons compositionWagons = db.CompositionWagons.Find(id);
            if (compositionWagons == null)
            {
                return HttpNotFound();
            }
            ViewBag.RailwayCarriageID = new SelectList(db.RailwayCarriage, "RailwayCarriageID", "Type", compositionWagons.RailwayCarriageID);
            return View(compositionWagons);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompositionWagonsID,RailwayCarriageID,Number")] CompositionWagons compositionWagons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compositionWagons).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            ViewBag.RailwayCarriageID = new SelectList(db.RailwayCarriage, "RailwayCarriageID", "Type", compositionWagons.RailwayCarriageID);
      
            return View(compositionWagons);
            
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompositionWagons compositionWagons = db.CompositionWagons.Find(id);
            if (compositionWagons == null)
            {

                return HttpNotFound();
            }
            return View(compositionWagons);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompositionWagons compositionWagons = db.CompositionWagons.Find(id);
            db.CompositionWagons.Remove(compositionWagons);
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
