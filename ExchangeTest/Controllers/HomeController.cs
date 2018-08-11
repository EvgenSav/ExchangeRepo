using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExchangeTest.Models;

namespace ExchangeTest.Controllers {
    public class HomeController : Controller {
        ExchangeContext db = new ExchangeContext();
        public ActionResult Index() {
            IEnumerable<Tool> tools = db.Tools;
            IEnumerable<Participant> participants = db.Participants;
            IEnumerable<Transaction> transactions = db.Transactions;
            ViewBag.Tools = tools;
            ViewBag.Participants = participants;
            ViewBag.Transactions = transactions;
            return View(db.Tools);
        }
        public ActionResult AddTool(string toolName) {
            db.Tools.Add(new Tool {Name = toolName });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing) {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}