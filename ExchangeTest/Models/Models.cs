using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ExchangeTest.Models {
    public class Tool {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Participant {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Transaction {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }

        public int BuyerId { get; set; }
        public  Participant Buyer { get; set; }

        public int SellerId { get; set; }
        public  Participant Seller { get; set; }

        public int ToolId { get; set; }
        public  Tool Tool { get; set; }
    }

    public class ExchangeContext : DbContext {
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Transaction>()
            .HasRequired(s => s.Buyer)
            .WithMany()
    .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}