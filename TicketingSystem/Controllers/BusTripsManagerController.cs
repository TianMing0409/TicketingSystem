using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class BusTripsManagerController : Controller
    {
        private TicketsDbContext db = new TicketsDbContext();

        // GET: BusTripsManager
        public ActionResult Index()
        {
            var busTrips = db.BusTrips.Include(b => b.BusInfos).Include(b => b.DestinationPlace).Include(b => b.OriginPlace);
            return View(busTrips.ToList());
        }

        // GET: BusTripsManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusTrip busTrip = db.BusTrips.Find(id);
            if (busTrip == null)
            {
                return HttpNotFound();
            }
            return View(busTrip);
        }

        // GET: BusTripsManager/Create
        public ActionResult Create()
        {
            ViewBag.BusId = new SelectList(db.BusInfos, "BusId", "BusCompanyName");
            ViewBag.DestinationPlaceId = new SelectList(db.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName");
            ViewBag.OriginPlaceId = new SelectList(db.OriginPlaces, "OriginPlaceId", "OriginPlaceName");
            return View();
        }

        // POST: BusTripsManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusTripId,BusId,OriginPlaceId,DestinationPlaceId,DepartureDate,ReturnDate,DepartureTime,SeatAvailable,Price")] BusTrip busTrip)
        {
            if (ModelState.IsValid)
            {
                db.BusTrips.Add(busTrip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusId = new SelectList(db.BusInfos, "BusId", "BusCompanyName", busTrip.BusId);
            ViewBag.DestinationPlaceId = new SelectList(db.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName", busTrip.DestinationPlaceId);
            ViewBag.OriginPlaceId = new SelectList(db.OriginPlaces, "OriginPlaceId", "OriginPlaceName", busTrip.OriginPlaceId);
            return View(busTrip);
        }

        // GET: BusTripsManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusTrip busTrip = db.BusTrips.Find(id);
            if (busTrip == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusId = new SelectList(db.BusInfos, "BusId", "BusCompanyName", busTrip.BusId);
            ViewBag.DestinationPlaceId = new SelectList(db.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName", busTrip.DestinationPlaceId);
            ViewBag.OriginPlaceId = new SelectList(db.OriginPlaces, "OriginPlaceId", "OriginPlaceName", busTrip.OriginPlaceId);
            return View(busTrip);
        }

        // POST: BusTripsManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusTripId,BusId,OriginPlaceId,DestinationPlaceId,DepartureDate,ReturnDate,DepartureTime,SeatAvailable,Price")] BusTrip busTrip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(busTrip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusId = new SelectList(db.BusInfos, "BusId", "BusCompanyName", busTrip.BusId);
            ViewBag.DestinationPlaceId = new SelectList(db.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName", busTrip.DestinationPlaceId);
            ViewBag.OriginPlaceId = new SelectList(db.OriginPlaces, "OriginPlaceId", "OriginPlaceName", busTrip.OriginPlaceId);
            return View(busTrip);
        }

        // GET: BusTripsManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusTrip busTrip = db.BusTrips.Find(id);
            if (busTrip == null)
            {
                return HttpNotFound();
            }
            return View(busTrip);
        }

        // POST: BusTripsManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusTrip busTrip = db.BusTrips.Find(id);
            db.BusTrips.Remove(busTrip);
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
