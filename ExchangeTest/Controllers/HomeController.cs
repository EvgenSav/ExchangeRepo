using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExchangeTest.Models;
using ExchangeTest.ViewModels;

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

            List<TransactionViewModel> transactionsView = new List<TransactionViewModel>();
            //foreach (var ts in transactions) {
            //    string tooLname = tools.FirstOrDefault(t => t.Id == ts.ToolId).Name;
            //    transactionsView.Add(new TransactionViewModel {
            //        Id = ts.Id,
            //        BuyerName = participants.FirstOrDefault(b => b.Id == ts.BuyerId).Name,
            //        SellerName = participants.FirstOrDefault(s => s.Id == ts.SellerId).Name,
            //        Price = ts.Price + tooLname,
            //        Amount = ts.Amount + tooLname,
            //        Time = ts.Time.ToShortTimeString()
            //    });
            //}
            //ViewBag.TransactionsViews = transactionsView;
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

        public JsonResult AddTransaction(Transaction transaction) {
            if (transaction != null) {
                db.Transactions.Add(new Transaction {
                    ToolId = transaction.ToolId,
                    BuyerId = transaction.BuyerId,
                    SellerId = transaction.SellerId,
                    Price = transaction.Price,
                    Amount = transaction.Amount,
                    Time = DateTime.Now
                });
                db.SaveChanges();
            }
            return Json(db.Transactions.OrderByDescending(a => a.Id).FirstOrDefault());
        }
        public JsonResult GetData() {
            JsonResult res = new JsonResult();
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            res.Data = new { Tools = db.Tools, Participants = db.Participants, Transactions = db.Transactions.OrderBy(new Func<Transaction, DateTime>(key => key.Time)) };
            return res;
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