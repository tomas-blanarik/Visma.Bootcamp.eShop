using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.eShop.ApplicationCore.Database;

namespace Visma.Bootcamp.eShop.Tests.Database
{
    public class TestApplicationContext : ApplicationContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseInMemoryDatabase($"{nameof(TestApplicationContext)}-{Guid.NewGuid()}");
        }
    }
}