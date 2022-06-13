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
    public class TrainsController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index(string searchBy, string searchValue)
        {
            var train = db.Train.Include(t => t.CompositionWagons);
            try
            {

                if (train.ToList().Count == 0)
                {
                    TempData["InfoMessage"] = "Currently Train not" +
                        "available in DB.";
                    return View(train.ToList());
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Please provide search value";
                        return View(train.ToList());
                    }
                    else
                    {
                        if (searchBy.ToLower() == "type")
                        {
                            var searchByType =
                            train.ToList().Where(n => n.Type.ToLower().Contains(
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


                        if (searchBy.ToLower() == "number")
                        {

                            var searchByNumber =
                              train.ToList().Where(n => n.Number == int.Parse(searchValue));
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
                return View(train.ToList());
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
            Train train = db.Train.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            return View(train);
        }

   
        public ActionResult Create()
        {
            ViewBag.CompositionWagonsID = new SelectList(db.CompositionWagons, "CompositionWagonsID", "CompositionWagonsID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainID,CompositionWagonsID,Type,Number")] Train train)
        {
            if (ModelState.IsValid)
            {
                db.Train.Add(train);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompositionWagonsID = new SelectList(db.CompositionWagons, "CompositionWagonsID", "CompositionWagonsID", train.CompositionWagonsID);
            return View(train);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train train = db.Train.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompositionWagonsID = new SelectList(db.CompositionWagons, "CompositionWagonsID", "CompositionWagonsID", train.CompositionWagonsID);
            return View(train);
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainID,CompositionWagonsID,Type,Number")] Train train)
        {
            if (ModelState.IsValid)
            {
                db.Entry(train).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompositionWagonsID = new SelectList(db.CompositionWagons, "CompositionWagonsID", "CompositionWagonsID", train.CompositionWagonsID);
            return View(train);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train train = db.Train.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            return View(train);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Train train = db.Train.Find(id);
            db.Train.Remove(train);
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
