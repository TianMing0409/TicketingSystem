﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;
using System.IO;
using System.Drawing;

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

            var trips = tripDB.BusTrips.Include(p => p.OriginPlace).Include(p => p.DestinationPlace)
                .Where(t => t.OriginPlaceId == search.OriginPlaceId &&
            t.DestinationPlaceId == search.DestinationPlaceId &&
            t.DepartureDate == search.DepartureDate &&
            t.ReturnDate == search.ReturnDate &&
            t.SeatAvailable > 0);

            if (search.OriginPlaceId == search.DestinationPlaceId)
            {

            }

            if (!trips.Any())
            {
                return View("NoResultsFound");
            }


            return View(trips.ToList());
        }

        public ActionResult ShowAllBusTrip()
        {
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
        public ActionResult Create(BusTrip trips, HttpPostedFileBase busCompanyLogo)
        {
            if (ModelState.IsValid)
            {
                var busInfo = new BusInfo
                {
                    BusCompanyName = trips.BusInfos.BusCompanyName,
                    BusPlateNo = trips.BusInfos.BusPlateNo
                };

                if (busCompanyLogo != null && busCompanyLogo.ContentLength > 0)
                {
                    byte[] imageData = new byte[busCompanyLogo.ContentLength];
                    busCompanyLogo.InputStream.Read(imageData, 0, busCompanyLogo.ContentLength);
                    busInfo.BusCompanyLogo = imageData;
                }

                trips.BusInfos = busInfo;

                tripDB.BusInfos.Add(busInfo);
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