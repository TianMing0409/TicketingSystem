using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;

namespace TicketingSystem.Controllers
{
    public class HomeController : Controller
    {
        TicketsDbContext tripDB = new TicketsDbContext();

        public ActionResult Index()
        {
            BusTrip busTrip = new BusTrip();

            ViewBag.OriginPlaceId = new SelectList(tripDB.OriginPlaces, "OriginPlaceId", "OriginPlaceName");
            ViewBag.DestinationPlaceId = new SelectList(tripDB.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName");


            return View(busTrip);
        }

        public ActionResult SearchTrip(BusTrip search) {

            var trips = tripDB.BusTrips.Include(p => p.OriginPlace).Include(p => p.DestinationPlace);
            return View(trips.ToList());
        }

        public ActionResult Create()
        {

            ViewBag.OriginPlaceId = new SelectList(tripDB.OriginPlaces, "OriginPlaceId", "OriginPlaceName");
            ViewBag.DestinationPlaceId = new SelectList(tripDB.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(BusTrip trips)
        {
            if (ModelState.IsValid)
            {
                tripDB.BusTrips.Add(trips);
                tripDB.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OriginPlaceId = new SelectList(tripDB.OriginPlaces, "OriginPlaceId", "OriginPlaceName");
            ViewBag.DestinationPlaceId = new SelectList(tripDB.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName");

            return View(trips);
        }

        //GET: TripSearch/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BusTrip trip = tripDB.BusTrips.Find(id);

            if (trip == null)
            {
                return HttpNotFound();
            }

            return View(trip);
        }

        // GET: TripSearch/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusTrip trip = tripDB.BusTrips.Find(id);

            if (trip == null)
            {
                return HttpNotFound();
            }

            return View(trip);
        }

        // POST: TripSearch/Delete
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BusTrip trip = tripDB.BusTrips.Find(id);
            tripDB.BusTrips.Remove(trip);
            tripDB.SaveChanges();

            return RedirectToAction("Index");
        }

        //GET: TripSearch/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusTrip busTrip = tripDB.BusTrips.Find(id);
            if (busTrip == null)
            {
                return HttpNotFound();
            }

            ViewBag.OriginPlaceId = new SelectList(tripDB.OriginPlaces, "OriginPlaceId", "OriginPlaceName");
            ViewBag.DestinationPlaceId = new SelectList(tripDB.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName");

            ViewBag.DepartureDate = busTrip.DepartureDate.ToString("dd-MM-yyyy");
            ViewBag.ReturnDate = busTrip.ReturnDate?.ToString("dd-MM-yyyy");
            //if (busTrip.ReturnDate == null)
            //{
            //    ViewBag.ReturnDate = "";
            //}
            //else {
            //    ViewBag.ReturnDate = busTrip.ReturnDate.ToString("dd-MM-yyyy");
            //}


            return View(busTrip);

        }

        //POST: TripSearch/Edit
        [HttpPost]
        public ActionResult Edit( BusTrip busTrip)
        {
            if (ModelState.IsValid)
            {
                tripDB.Entry(busTrip).State = EntityState.Modified;
                tripDB.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OriginPlaceId = new SelectList(tripDB.OriginPlaces, "OriginPlaceId", "OriginPlaceName");
            ViewBag.DestinationPlaceId = new SelectList(tripDB.DestinationPlaces, "DestinationPlaceId", "DestinationPlaceName");

            return View(busTrip);
        }

        public ActionResult About()
        {
            return View();
        }

    }
}