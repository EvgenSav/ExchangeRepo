using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ExchangeTest.Models {
    public class ExchangeDbInitializer:DropCreateDatabaseAlways<ExchangeContext> {
        protected override void Seed(ExchangeContext context) {
            context.Tools.Add(new Tool { Id = 0, Name = "USD" });
            context.Tools.Add(new Tool { Id = 1, Name = "EUR" });
            context.Tools.Add(new Tool { Id = 2, Name = "RUR" });
            context.Participants.Add(new Participant { Id = 0, Name = "Vasya" });
            context.Participants.Add(new Participant { Id = 1, Name = "Petya" });
            context.Transactions.Add(new Transaction { Id = 0, ToolId = 0, Time = DateTime.Now.Date, BuyerId = 0, SellerId = 1, Price = 1000m, Amount = 1500m });
            context.Transactions.Add(new Transaction { Id = 1, ToolId = 1, Time = DateTime.Now.Date, BuyerId = 1, SellerId = 0, Price = 2000m, Amount = 3000m });
            base.Seed(context);
        }
    }
}