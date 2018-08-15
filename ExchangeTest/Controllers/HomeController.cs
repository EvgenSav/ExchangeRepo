using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExchangeTest.Models;
using Newtonsoft.Json;

namespace ExchangeTest.Controllers {
    public class HomeController : Controller {
        public static int SelectedToolId = 0;
        ExchangeContext db = new ExchangeContext();
        public ActionResult Index() {
            IEnumerable<Tool> tools = db.Tools.ToList();
            IEnumerable<Participant> participants = db.Participants.ToList();
            IEnumerable<Transaction> transactions = db.Transactions.ToList();

            ViewBag.Tools = tools;
            ViewBag.Participants = participants;
            ViewBag.Transactions = transactions;
            List<DateTime> times = new List<DateTime>();
            List<decimal> prices = new List<decimal>(); ;

            if (SelectedToolId == 0) {
                times = transactions.Select(t => t.Time.Date).ToList();
                prices = transactions.Select(p => p.Price).ToList();
                ViewBag.dataY = JsonConvert.SerializeObject(0);
                ViewBag.dataX = JsonConvert.SerializeObject(0);
            } else {
                times = db.Transactions.Where(t => t.ToolId == SelectedToolId).Select(t => t.Time).ToList();
                prices = db.Transactions.Where(t => t.ToolId == SelectedToolId).Select(p => p.Price).ToList();
                ViewBag.dataY = JsonConvert.SerializeObject(prices);
                ViewBag.dataX = JsonConvert.SerializeObject(times);
            }
            SelectList ToolsList = new SelectList(tools, "Id", "Name", SelectedToolId);
            ViewBag.ToolsList = ToolsList;

            return View();
        }

        public ActionResult SelectTool(Tool selected) {
            if (selected.Id != 0) {
                SelectedToolId = selected.Id;
            }
            return RedirectToAction("Index");
        }
        public ActionResult AjaxView() {
            return View();
        }
        public ActionResult ModifyView() {
            return View();
        }

        //API for working with DB-------------------
        public string AddTool(string name) {
            if (name != null) {
                db.Tools.Add(new Tool { Name = name });
                db.SaveChanges();
            }
            List<Tool> tools = db.Tools.ToList();
            return JsonConvert.SerializeObject(tools);
        }

        public string AddParticipant(string name) {
            if (name != null) {
                db.Participants.Add(new Participant { Name = name });
                db.SaveChanges();
            }
            List<Participant> participants = db.Participants.ToList();
            return JsonConvert.SerializeObject(participants);
        }

        public string AddTransaction(Transaction transaction) {
            if (transaction.Seller != null && transaction.Buyer != null && transaction.Tool != null && transaction.Buyer.Id != transaction.Seller.Id) {
                db.Transactions.Add(new Transaction {
                    ToolId = transaction.Tool.Id,
                    //Tool = new Tool { Name = transaction.Tool.Name },
                    BuyerId = transaction.Buyer.Id,
                    //Buyer = new Participant { Name = transaction.Buyer.Name },
                    SellerId = transaction.Seller.Id,
                    //Seller = new Participant { Name = transaction.Seller.Name },
                    Price = transaction.Price,
                    Amount = transaction.Amount,
                    Time = DateTime.Now
                });
                db.SaveChanges();
            }
            List<Transaction> res = db.Transactions.ToList();
            return JsonConvert.SerializeObject(res);
        }

        public string GetData() {
            List<Tool> tools = db.Tools.ToList();
            List<Participant> participants = db.Participants.ToList();
            List<Transaction> transactions = db.Transactions.ToList();
            return JsonConvert.SerializeObject(new { Tools = tools, Participants = participants, Transactions = transactions }); ;
        }
        //------------------------------------------


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