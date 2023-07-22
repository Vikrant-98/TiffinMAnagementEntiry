﻿using CommonServices.ModelServices;
using Microsoft.EntityFrameworkCore;

namespace Repository.ApplicationDB
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<CustomerStocks> CustomerStocks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Stocks> Stocks { get; set; }

    }
}

