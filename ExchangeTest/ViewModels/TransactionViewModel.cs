using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExchangeTest.ViewModels {
    public class TransactionViewModel {
        public int Id { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string Time { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
    }
}