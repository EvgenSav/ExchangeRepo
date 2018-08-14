using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Newtonsoft.Json;

namespace ExchangeTest.Models {
    public class Tool {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
    public class Participant {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }

    public class Transaction {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }

        public int BuyerId { get; set; }
        public virtual Participant Buyer { get; set; }

        public int SellerId { get; set; }
        public virtual Participant Seller { get; set; }

        public int ToolId { get; set; }
        public virtual Tool Tool { get; set; }
    }

    public class ExchangeContext : DbContext {
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Transaction>() //entity Transaction"
            .HasRequired(t => t.Tool)          // has nav. field Tool (one Tool - many transactions)
            .WithMany(x=>x.Transactions)       // with nav. field Transactions in entity Tool in the other side
            .HasForeignKey(k=>k.ToolId)        // by foreign key ToolId
            .WillCascadeOnDelete(false);       // no casacade delete

            modelBuilder.Entity<Participant>()  //entity Participant
           .HasMany(t => t.Transactions)        // has nav field Transactions
           .WithRequired(y=>y.Buyer)            // with nav field Buyer in entity Transaction in the other side
           .HasForeignKey(k => k.BuyerId)       // by foreign key BuyerId
           .WillCascadeOnDelete(false);         // no casacade delete

            modelBuilder.Entity<Participant>()
           .HasMany(t => t.Transactions)
           .WithRequired(y => y.Seller)
           .HasForeignKey(k => k.SellerId)
           .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}