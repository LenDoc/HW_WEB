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
    public class TripsController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index(string searchBy, string searchValue)
        {
            var trip = db.Trip.Include(t => t.Station).Include(t => t.Train);
            try
            {

                if (trip.ToList().Count == 0)
                {
                    TempData["InfoMessage"] = "Currently Train not" +
                        "available in DB.";
                    return View(trip.ToList());
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Please provide search value";
                        return View(trip.ToList());
                    }
                    else
                    {
                        if (searchBy.ToLower() == "name")
                        {
                            var searchByName =
                            trip.ToList().Where(n => n.Name.ToLower().Contains(
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


                        if (searchBy.ToLower() == "distance")
                        {
                            var searchByDist =
                              trip.ToList().Where(n => n.Distance == int.Parse(searchValue));
                            if (searchByDist.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchByDist);
                        }

                    }

                }
                return View(trip.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            return View(trip.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }
        public ActionResult Create()
        {
            ViewBag.StationID = new SelectList(db.Station, "StationID", "Name");
            ViewBag.TrainID = new SelectList(db.Train, "TrainID", "Type");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TripID,StationID,TrainID,Name,Distance,DatetimeDeparture,DatetimeArrival")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trip.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StationID = new SelectList(db.Station, "StationID", "Name", trip.StationID);
            ViewBag.TrainID = new SelectList(db.Train, "TrainID", "Type", trip.TrainID);
            return View(trip);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.StationID = new SelectList(db.Station, "StationID", "Name", trip.StationID);
            ViewBag.TrainID = new SelectList(db.Train, "TrainID", "Type", trip.TrainID);
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TripID,StationID,TrainID,Name,Distance,DatetimeDeparture,DatetimeArrival")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StationID = new SelectList(db.Station, "StationID", "Name", trip.StationID);
            ViewBag.TrainID = new SelectList(db.Train, "TrainID", "Type", trip.TrainID);
            return View(trip);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trip.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trip.Find(id);
            db.Trip.Remove(trip);
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
