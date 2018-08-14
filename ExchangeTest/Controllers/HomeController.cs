using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExchangeTest.Models;
using Newtonsoft.Json;

namespace ExchangeTest.Controllers {
    public class HomeController : Controller {
        ExchangeContext db = new ExchangeContext();
        public ActionResult Index() {
            IEnumerable<Tool> tools = db.Tools.ToList();
            IEnumerable<Participant> participants = db.Participants.ToList();
            IEnumerable<Transaction> transactions = db.Transactions.ToList();

            ViewBag.Tools = tools;
            ViewBag.Participants = participants;
            ViewBag.Transactions = transactions;
            List<DateTime> times = transactions.Select(t => t.Time.Date).ToList();
            List<decimal> prices = transactions.Select(p => p.Price).ToList();

            ViewBag.dataY = JsonConvert.SerializeObject(prices);
            ViewBag.dataX = JsonConvert.SerializeObject(times);

            return View();
        }
        public ActionResult AjaxView() {
            return View();
        }
        public JsonResult AddTool(string name) {
            if (name != null) {
                db.Tools.Add(new Tool { Name = name });
                db.SaveChanges();
            }
            return Json(db.Tools);
        }

        public JsonResult AddParticipant(string name) {
            if (name != null) {
                db.Participants.Add(new Participant { Name = name });
                db.SaveChanges();
            }
            return Json(db.Participants);
        }

        public string AddTransaction(Transaction transaction) {
            if (transaction.Seller != null && transaction.Buyer != null && transaction.Tool != null && transaction.Buyer.Id != transaction.Seller.Id) {
                db.Transactions.Add(new Transaction {
                    ToolId = transaction.Tool.Id,
                    BuyerId = transaction.Buyer.Id,
                    SellerId = transaction.Seller.Id,
                    Price = transaction.Price,
                    Amount = transaction.Amount,
                    Time = DateTime.Now
                });
                db.SaveChanges();
            }
            return JsonConvert.SerializeObject(db.Transactions.ToList());
        }

        public string GetData() {
            List<Tool> tools = db.Tools.ToList();
            List<Participant> participants = db.Participants.ToList();
            List<Transaction> transactions = db.Transactions.ToList();
            return JsonConvert.SerializeObject(new { Tools = tools, Participants = participants, Transactions = transactions }); ;
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