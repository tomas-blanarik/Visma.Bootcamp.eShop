﻿using Microsoft.EntityFrameworkCore;
using Visma.Bootcamp.eShop.ApplicationCore.Entities.Domain;

namespace Visma.Bootcamp.eShop.ApplicationCore.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        protected ApplicationContext()
        { }

        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
