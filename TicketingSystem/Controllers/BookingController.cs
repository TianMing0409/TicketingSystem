using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Models;
using TicketingSystem.ViewModels;
using System.Net;
using System.Net.Mail;

namespace TicketingSystem.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        TicketsDbContext tripDB = new TicketsDbContext();

        const decimal ProcessingFee = (decimal)2.50;     // Fixed processing fee for all bus trips
        const decimal Discount = (decimal)0.25;          // Fixed discount
        const string PromoCode = "HAPPY25";              // Promo Code        

        // GET: Booking
        public ActionResult Index()
        {
            int numTickets = Convert.ToInt32(Request.QueryString["num_ticket"]);
            Session["numTickets"] = numTickets;
            return View();
        }


        //POST: Booking
        [HttpPost]
        public ActionResult Index(int id, Booking booking)
        {
            var userId = Session["UserId"] as string;

            DateTime currentDate = DateTime.Now;
            BusTrip busTrip = tripDB.BusTrips.Find(id);

            if (ModelState.IsValid)
            {
                var numTickets = Session["numTickets"];

                var bookingInfo = new Booking
                {
                    UserId = userId,
                    BusTripId = busTrip.BusTripId,
                    PassengerName = booking.PassengerName,
                    Email = booking.Email,
                    PhoneNo = booking.PhoneNo,
                    BookingDate = currentDate,
                    Total = busTrip.Price * Convert.ToInt32(numTickets)

                };
                tripDB.Bookings.Add(bookingInfo);
                tripDB.SaveChanges();

                ViewBag.BookingId = bookingInfo.BookingId;
                ViewBag.BusTripId = busTrip.BusTripId.ToString();

                return RedirectToAction("Payment", "Booking", new { bookingId = bookingInfo.BookingId, busTripId = busTrip.BusTripId });
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

            decimal totalAmount = booking.Total + ProcessingFee ;


            var viewModel = new PaymentViewModel
            {
                Booking = booking,
                Payment = payment,
                BusTrip = busTrip,
                TotalAmount = totalAmount,
                ProcessingFee = ProcessingFee
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("/apply-promo-code")]
        public JsonResult ApplyPromoCode(string promoCode,decimal totalAmount,PaymentViewModel viewModel)
        {

            decimal discount = (decimal)0.00;
            decimal discountAmt = (decimal)0.00;

            if (promoCode == PromoCode)
            {
                discount = Discount;
                discountAmt = totalAmount * discount;
                discountAmt = Math.Ceiling(discountAmt * 100) / 100;
                totalAmount = totalAmount - discountAmt;
                totalAmount = Math.Ceiling(totalAmount * 100) / 100;
                viewModel.Discount = discountAmt;
                Session["DiscountAmount"] = discountAmt;
                return Json(new { discountAmt, totalAmount,viewModel });
            }

            // Return the updated payment information to the client side
            return Json(new { error = "Invalid promo code" });
        }

        //POST: Payment
        [HttpPost]
        public ActionResult Payment(int bookingId, int busTripId, PaymentViewModel viewModel,Payment payment)
        {
            decimal discountAmt = (decimal)0.00;
            if (Session["DiscountAmount"] != null)
            {
                discountAmt = (decimal)Session["DiscountAmount"];
            }

            DateTime currentDate = DateTime.Now;
            BusTrip busTrip = tripDB.BusTrips.Find(busTripId);


            if (ModelState.IsValid)
            {
                int numTickets = Convert.ToInt32(Session["numTickets"]);

                var paymentInfo = new Payment
                {
                    //Id = userId,
                    BookingId = bookingId,
                    PaymentAmount = (busTrip.Price * numTickets) + ProcessingFee - discountAmt,
                    PaymentDate = currentDate,
                    CardHolderName = viewModel.Payment.CardHolderName,
                    CardNo = viewModel.Payment.CardNo,
                    ExpirationDate = viewModel.Payment.ExpirationDate,
                    Cvc = viewModel.Payment.Cvc
                };

                    tripDB.Payments.Add(paymentInfo);

                    tripDB.SaveChanges();

                    Session.Remove("DiscountAmount");
                    busTrip.SeatAvailable -= numTickets;
                    tripDB.Entry(busTrip).State = EntityState.Modified;
                    tripDB.SaveChanges();


                //Thread.Sleep(3000); // delay for 3 seconds
                return RedirectToAction("Complete", "Booking", new { paymentId = paymentInfo.PaymentId, bookingId = bookingId  });
            }

            return View();
        }

        public ActionResult Complete(int? paymentId, int? bookingId)
        {
            string userEmail = User.Identity.Name;

            // Get the booking information from the database
            var booking = tripDB.Bookings.Find(bookingId);
            var payment = tripDB.Payments.Find(paymentId);

            //string bookingInfo = "<html><body>" +
            //  "Your booking details are as follows:</p>" +

            //  "<p><strong>Departure Date:</strong> " + booking.BusTrip.DepartureDate.ToShortDateString() + "</p>" +
            //  "<p><strong>Departure Time:</strong> " + booking.BusTrip.DepartureTime + "</p>" +
            //  "<p><strong>Origin:</strong> " + booking.BusTrip.OriginPlace.OriginPlaceName + "</p>" +
            //  "<p><strong>Destination:</strong> " + booking.BusTrip.DestinationPlace.DestinationPlaceName + "</p>" +
            //  "<p><strong>Passenger Name:</strong> " + booking.PassengerName + "</p>" +
            //    "<p><strong>Passenger Name:</strong> " + booking.BookingDate + "</p>" +
            //  "<p><strong>Total:</strong> $" + payment.PaymentAmount + "</p>" +
            //  "</body></html>";

            // Format the booking information as a string
            string bookingInfo = "Departure Date: " + booking.BusTrip.DepartureDate.ToShortDateString() + "\n" +
                                 "Departure Time: " + booking.BusTrip.DepartureTime + "\n" +
                                 "Departure Location: " + booking.BusTrip.OriginPlace.OriginPlaceName + "\n" +
                                 "Arrival Location: " + booking.BusTrip.DestinationPlace.DestinationPlaceName + "\n" +
                                 "Passenger Name: " + booking.PassengerName + "\n" +
                                 "Booking Date: " + booking.BookingDate + "\n" +
                                 "Total Amount Paid: RM " + payment.PaymentAmount;

            string subject = "Confirmation for Booking [ Ref : " + bookingId + "]";
            // Add the booking information to the email body
            string body = "Dear " + User.Identity.Name + ",\n\nThanks for your booking. \n\nThis is your booking ID: " + bookingId + "\n\nBooking Details:\n============\n" + bookingInfo;

            // Create the MailMessage object with the modified email body
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("kohtm0409@gmail.com", "EasyBus");
            message.From = fromAddress;
            message.To.Add(new MailAddress(userEmail));
            message.Subject = subject;
            message.Body = body;

            // Send the email
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); // Replace with your SMTP server and port
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("kohtm0409@gmail.com", "ydndlgetdciqlzad"); // Replace with your email address and password
            client.Send(message);

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