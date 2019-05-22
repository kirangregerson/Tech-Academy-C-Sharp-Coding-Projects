using CarInsuranceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private double MakeQuote(string birthDate, int carYear, string carMake, string carModel,
                                int numberOfTickets, string hasDui, string insuranceType)
        {
            double quote = 50;

            //adds to rate based on driver age
            var age = DateTime.Now - Convert.ToDateTime(birthDate);
            if (age.TotalDays < 6570)
            {
                quote += 100;
            }
            else if (age.TotalDays < 9125 || age.TotalDays >= 36500)
            {
                quote += 25;
            }

            //adds to rate based on car age
            if(carYear < 2000 || carYear > 2015)
            {
                quote += 25;
            }

            //adds to rate based on car make and model
            if (carMake.ToLower().Equals("porsche"))
            {
                if(carModel.ToLower().Equals("911 carrera"))
                {
                    quote += 50;
                }
                else
                {
                    quote += 25;
                }
            }

            //adds to rate based on speeding tickets
            quote += (10 * numberOfTickets);

            //adds to rate if driver has has a DUI
            if(hasDui == "Yes")
            {
                quote = 1.25 * quote;
            }

            //adds to rate based on coverage
            if(insuranceType == "Full Coverage")
            {
                quote = 1.5 * quote;
            }
            return quote;
        }

        [HttpPost]
        public ActionResult MakeQuote(string firstName, string lastName, string emailAddress,
                                        string birthDate, int carYear, string carMake, string carModel,
                                        int numberOfTickets)
        {
            if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) ||
                string.IsNullOrEmpty(birthDate) || string.IsNullOrEmpty(Convert.ToString(carYear)) || string.IsNullOrEmpty(carMake) ||
                string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(Convert.ToString(numberOfTickets))
                || !(Request.Form["HaveDui"] != null) || !(Request.Form["InsuranceType"] != null))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                using (InsuranceEntities db = new InsuranceEntities())
                {
                    var quote = new Quote();
                    quote.FirstName = firstName;
                    quote.LastName = lastName;
                    quote.EmailAddress = emailAddress;

                    quote.DateOfBirth = Convert.ToDateTime(birthDate);

                    quote.CarYear = carYear;
                    quote.CarMake = carMake;
                    quote.CarModel = carModel;
                    quote.NumberOfTickets = numberOfTickets;
                    string hasDui = Request.Form["HaveDui"].ToString();
                    string insuranceType = Request.Form["InsuranceType"].ToString();
                    quote.HasDui = hasDui;
                    quote.InsuranceType = insuranceType;
                    decimal customerQuote = (decimal)MakeQuote(birthDate, carYear, carMake, carModel, numberOfTickets, hasDui, insuranceType);
                    quote.CustomerQuote = Math.Round(customerQuote, 2);
                    db.Quotes.Add(quote);
                    db.SaveChanges();
                    ViewBag.Quote = String.Format(customerQuote % 1 == 0 ? "{0:F0}" : "{0:F2}", customerQuote);
                }
                return View("Success");
            }
        }
    }
}