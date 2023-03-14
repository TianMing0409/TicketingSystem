using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;
using TicketingSystem.ViewModels;

namespace TicketingSystem.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        TicketsDbContext tripDB = new TicketsDbContext();

        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }

        // POST: Booking
        [HttpPost]
        public ActionResult Index(int id, Booking booking)
        {
            var userId = Session["UserId"] as string;

            DateTime currentDate = DateTime.Now;
            BusTrip busTrip = tripDB.BusTrips.Find(id);

            if (ModelState.IsValid)
            {
                var bookingInfo = new Booking
                {
                    UserId = userId,
                    BusTripId = busTrip.BusTripId,
                    PassengerName = booking.PassengerName,
                    Email = booking.Email,
                    PhoneNo = booking.PhoneNo,
                    BookingDate = currentDate,
                    Total = busTrip.Price
                };

                tripDB.Bookings.Add(bookingInfo);
                tripDB.SaveChanges();

                return RedirectToAction("Payment","Booking", new { bookingId = bookingInfo.BookingId, busTripId = busTrip.BusTripId} );
            }
            return View();
        }

        //GET: Payment
        public ActionResult Payment(int? bookingId, int? busTripId, Payment payment)
        {


            var busTrip = tripDB.BusTrips.Find(busTripId);
            var booking = tripDB.Bookings.Find(bookingId);

            if (booking == null)
            {
                return RedirectToAction("Index", "Home"); 
            }

            var viewModel = new PaymentViewModel
            {
                Booking = booking,
                Payment = payment,
                BusTrip = busTrip
            };

            return View(viewModel);
        }

        ////POST: Payment
        //[HttpPost]
        //public ActionResult Payment(int bookingId, int busTripId, Payment payment)
        //{
        //    DateTime currentDate = DateTime.Now;
        //    BusTrip busTrip = tripDB.BusTrips.Find(busTripId);

        //    if (ModelState.IsValid)
        //    {
        //        var paymentInfo = new Payment
        //        {
        //            BookingId = bookingId,
        //            PaymentAmount = busTripId,
        //            PaymentDate = currentDate,
        //            CardHolderName = payment.CardHolderName,
        //            CardNo = payment.CardNo,
        //            ExpirationDate = payment.ExpirationDate,
        //            Cvc = payment.Cvc
        //        };

        //        tripDB.Payments.Add(paymentInfo);

        //        tripDB.SaveChanges();

        //        busTrip.SeatAvailable -= 1;
        //        tripDB.Entry(busTrip).State = EntityState.Modified;
        //        tripDB.SaveChanges();



        //        return RedirectToAction("Complete", "Booking", new { id = payment.PaymentId });
        //    }

        //    return View();
        //}
        //POST: Payment
        [HttpPost]
        public ActionResult Payment(int bookingId, int busTripId, PaymentViewModel viewModel,Payment payment)
        {
           

            DateTime currentDate = DateTime.Now;
            BusTrip busTrip = tripDB.BusTrips.Find(busTripId);


                if (ModelState.IsValid)
                {
                    var paymentInfo = new Payment
                    {
                        //Id = userId,
                        BookingId = bookingId,
                        PaymentAmount = busTrip.Price,
                        PaymentDate = currentDate,
                        CardHolderName = viewModel.Payment.CardHolderName,
                        CardNo = viewModel.Payment.CardNo,
                        ExpirationDate = viewModel.Payment.ExpirationDate,
                        Cvc = viewModel.Payment.Cvc
                    };

                    tripDB.Payments.Add(paymentInfo);

                    tripDB.SaveChanges();

                    busTrip.SeatAvailable -= 1;
                    tripDB.Entry(busTrip).State = EntityState.Modified;
                    tripDB.SaveChanges();



                return RedirectToAction("Complete", "Booking", new { id = paymentInfo.PaymentId });
            }
            


            return View();
        }

        public ActionResult Complete(int? id)
        {

            return View();
        }

        [ChildActionOnly]
        public ActionResult BusTripInfo(int? id)
        {
            var busTrip = tripDB.BusTrips.Find(id);
            return PartialView(busTrip);
        }

        public ActionResult BookingHistory()
        {
            string userId = User.Identity.GetUserId();

            //var bookings = tripDB.Bookings
            //                 .Include(b => b.BusTrip)
            //                 .Join(tripDB.Payments,
            //                       b => b.BookingId,
            //                       p => p.BookingId,
            //                       (b, p) => new { Booking = b, Payment = p })
            //                 .Where(bp => bp.Booking.UserId == userId)
            //                 .Select(bp => bp.Booking)
            //                 .ToList();

            //return View(bookings);
            var bookings = tripDB.Bookings
                 .Include(b => b.BusTrip)
                 .Where(b => b.UserId == userId)
                 .ToList();

            var bookingIds = bookings.Select(b => b.BookingId).ToList();

            var payments = tripDB.Payments
                              .Where(p => bookingIds.Contains(p.BookingId))
                              .ToList();

            var viewModel = bookings.Join(
                               payments,
                               b => b.BookingId,
                               p => p.BookingId,
                               (b, p) => new BookingHistoryViewModel
                               {
                                   BookingId = b.BookingId,
                                   BusTripId = b.BusTripId,
                                   OriginPlace = b.BusTrip.OriginPlace.OriginPlaceName,
                                   DestinationPlace = b.BusTrip.DestinationPlace.DestinationPlaceName,
                                   DepartureTime = b.BusTrip.DepartureTime,
                                   DepartureDate = b.BusTrip.DepartureDate,
                                   BusCompanyName = b.BusTrip.BusInfos.BusCompanyName,
                                   BusCompanyLogo = b.BusTrip.BusInfos.BusCompanyLogo,
                                   Rating = b.BusTrip.Rating,
                                   UserId = b.UserId,
                                   BookingDate = b.BookingDate,
                                   PassengerName = b.PassengerName,
                                   PhoneNo = b.PhoneNo,
                                   Email = b.Email,
                                   Total = b.Total,
                                   PaymentAmount = p.PaymentAmount,
                                   PaymentDate = p.PaymentDate
                               })
                               .OrderBy(b => b.BookingDate) // Sort by BookingDate
                               .ToList();


            return View(viewModel);

        }

    }
}