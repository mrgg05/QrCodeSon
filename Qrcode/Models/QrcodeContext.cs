using BarkodSistem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Qrcode.Models
{
    public class QrcodeContext:DbContext
    {

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<QrCodeInfo> QrcodeInfo { get; set; }
     

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Category>()
        //        .HasMany(e => e.Product)
        //        .WithRequired(e => e.Category)
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Product>()
        //        .HasOptional(e => e.QrcodeInfo)
        //        .WithRequired(e => e.Product);
        //}
    }
}