﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ExchangeTest.Models {
    public class ExchangeDbInitializer:DropCreateDatabaseAlways<ExchangeContext> {
        protected override void Seed(ExchangeContext context) {
            context.Tools.Add(new Tool { Name = "USD" });
            context.Tools.Add(new Tool { Name = "EUR" });
            context.Tools.Add(new Tool { Name = "RUR" });

            context.Participants.Add(new Participant { Name = "Vasya" }); 
            context.Participants.Add(new Participant { Name = "Petya" }); 

            context.Transactions.Add(new Transaction { ToolId = 1, Time = new DateTime(2018, 8, 5, 23, 05, 18), BuyerId = 1, SellerId = 2, Price = 1000m, Amount = 1500m });
            context.Transactions.Add(new Transaction { ToolId = 2, Time = new DateTime(2018, 8, 8, 18, 00, 00), BuyerId = 2, SellerId = 1, Price = 500m, Amount = 3000m });
            context.Transactions.Add(new Transaction { ToolId = 3, Time = new DateTime(2018, 8, 10, 13, 00, 15), BuyerId = 2, SellerId = 1, Price = 750m, Amount = 3000m });
            context.Transactions.Add(new Transaction { ToolId = 2, Time = new DateTime(2018, 8, 12, 18, 25, 06), BuyerId = 1, SellerId = 2, Price = 2000m, Amount = 3000m });
            base.Seed(context);
        }
    }
}