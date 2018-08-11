using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ExchangeTest.Models {
    public class Tool {
        public int  Id { get; set; }
        public string Name { get; set; }
    }
    public class Participant {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Transaction {
        public int Id { get; set; }
        public int ToolId { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
    }
    public class ExchangeContext : DbContext {
        public ExchangeContext() {

        }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}