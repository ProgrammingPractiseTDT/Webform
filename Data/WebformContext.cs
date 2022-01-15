#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webform.Models;

namespace Webform.Data
{
    public class WebformContext : DbContext
    {
        public WebformContext (DbContextOptions<WebformContext> options)
            : base(options)
        {
        }

        public DbSet<Webform.Models.Product> Product { get; set; }

        public DbSet<Webform.Models.Cart> Cart { get; set; }

        public DbSet<Webform.Models.SalesAgent> SalesAgent { get; set; }

        public DbSet<Webform.Models.ItemInCart> ItemInCart { get; set; }

        public DbSet<Webform.Models.DeliverySlip> DeliverySlip { get; set; }

        public DbSet<Webform.Models.Image> Image { get; set; }
    }
}
