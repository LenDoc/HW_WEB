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
    public class TicketsController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index(string searchBy, string searchValue)
        {
            var ticket = db.Ticket.Include(t => t.Passenger).Include(t => t.Trip);

            try
            {
              
                if (ticket.ToList().Count == 0)
                {
                    TempData["InfoMessage"] = "Currently Ticket not" +
                        "available in DB.";
                    return View(ticket.ToList());
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Please provide search value";
                        return View(ticket.ToList());
                    }
                    else
                    {
                        if (searchBy.ToLower() == "availabilitybenefits")
                        {
                            var searchByBen =
                               ticket.ToList().Where(n => n.AvailabilityBenefits.ToLower().Contains(
                                    searchValue.ToLower()));
                            if (searchByBen.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchByBen);
                        }


                        if (searchBy.ToLower() == "seat")
                        {
                            var searchBySeat =
                               ticket.ToList().Where(n => n.Seat == int.Parse(searchValue));
                            if (searchBySeat.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchBySeat);
                        }
                        if (searchBy.ToLower() == "price")
                        {
                            var searchByPrice =
                               ticket.ToList().Where(n => n.Price == decimal.Parse(searchValue));
                            if (searchByPrice.ToList().Count == 0)
                            {
                                TempData["ErrorMessage"] = "There is no value in search :(";
                            }
                            else
                            {
                                TempData["SuccessMessage"] = "We found what you search!";
                            }
                            return View(searchByPrice);
                        }

                    }

                }
                return View(ticket.ToList());
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            return View(ticket.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        public ActionResult Create()
        {
            ViewBag.PassengerID = new SelectList(db.Passenger, "PassengerID", "FullName");
            ViewBag.TripID = new SelectList(db.Trip, "TripID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketID,TripID,PassengerID,Seat,AvailabilityBenefits,Price")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Ticket.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PassengerID = new SelectList(db.Passenger, "PassengerID", "FullName", ticket.PassengerID);
            ViewBag.TripID = new SelectList(db.Trip, "TripID", "Name", ticket.TripID);
            return View(ticket);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.PassengerID = new SelectList(db.Passenger, "PassengerID", "FullName", ticket.PassengerID);
            ViewBag.TripID = new SelectList(db.Trip, "TripID", "Name", ticket.TripID);
            return View(ticket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketID,TripID,PassengerID,Seat,AvailabilityBenefits,Price")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PassengerID = new SelectList(db.Passenger, "PassengerID", "FullName", ticket.PassengerID);
            ViewBag.TripID = new SelectList(db.Trip, "TripID", "Name", ticket.TripID);
            return View(ticket);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            db.Ticket.Remove(ticket);
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
