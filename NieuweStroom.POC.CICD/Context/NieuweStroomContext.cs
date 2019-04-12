using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NieuweStroom.POC.CICD.Models;

namespace NieuweStroom.POC.CICD.Context
{
    public class NieuweStroomContext : DbContext
    {
        public NieuweStroomContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
    }
}
