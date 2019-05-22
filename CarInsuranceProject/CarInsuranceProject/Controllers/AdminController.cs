using CarInsuranceProject.Models;
using CarInsuranceProject.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsuranceProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                var quotes = (from c in db.Quotes
                               where c.Removed == null
                               select c).ToList();
                var quoteVms = new List<QuoteVm>();
                foreach (var quote in quotes)
                {
                    var quoteVm = new QuoteVm();
                    quoteVm.Id = quote.Id;
                    quoteVm.FirstName = quote.FirstName;
                    quoteVm.LastName = quote.LastName;
                    quoteVm.EmailAddress = quote.EmailAddress;
                    quoteVm.CustomerQuote = quote.CustomerQuote;
                    quoteVms.Add(quoteVm);
                }
                return View(quoteVms);
            }
        }

        public ActionResult Unsubscribe(int Id)
        {
            using (InsuranceEntities db = new InsuranceEntities())
            {
                var quote = db.Quotes.Find(Id);
                quote.Removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}