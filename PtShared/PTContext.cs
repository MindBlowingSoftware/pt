﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PtShared
{
    public partial class PTContext : DbContext
    {
        public PTContext()
        {
        }

        public PTContext(DbContextOptions<PTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ib> Ib { get; set; }
        public virtual DbSet<Icicinre> Icicinre { get; set; }
        public virtual DbSet<IciciNro> IciciNro { get; set; }
        public virtual DbSet<Savings> Savings { get; set; }
        public virtual DbSet<Ticker> Ticker { get; set; }
        public virtual DbSet<TickerHistory> TickerHistory { get; set; }
        public virtual DbSet<YesBankWm> YesBankWm { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=TRAPPIST-PC\\SQLEXPRESS;Initial Catalog=PT;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
        .Query<CombinedView>().ToView("CombinedView");

            modelBuilder.Entity<Ib>(entity =>
            {
                entity.Property(e => e.ClosePrice).HasColumnName("Close Price");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CostBasis).HasColumnName("Cost Basis");

                entity.Property(e => e.CostPrice).HasColumnName("Cost Price");

                entity.Property(e => e.Dateimported)
                    .HasColumnName("dateimported")
                    .HasColumnType("datetime");

                entity.Property(e => e.Mult).HasMaxLength(20);

                entity.Property(e => e.Symbol).HasMaxLength(20);

                entity.Property(e => e.UnrealizedPL).HasColumnName("Unrealized P/L");
            });

            modelBuilder.Entity<Icicinre>(entity =>
            {
                entity.Property(e => e.AverageCostPrice)
                    .HasColumnName("Average Cost Price")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ChangeOverPrevClose)
                    .HasColumnName("% Change over prev close")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Column12)
                    .HasColumnName("Column 12")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasColumnName("Company Name")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentMarketPrice)
                    .HasColumnName("Current Market Price")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Dateimported).HasColumnType("datetime");

                entity.Property(e => e.IsinCode)
                    .HasColumnName("ISIN Code")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Qty)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.RealizedProfitLoss)
                    .HasColumnName("Realized Profit   Loss")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.StockSymbol)
                    .HasColumnName("Stock Symbol")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UnrealizedProfitLoss)
                    .HasColumnName("Unrealized Profit Loss")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UnrealizedProfitLoss1)
                    .HasColumnName("Unrealized Profit Loss %")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ValueAtCost)
                    .HasColumnName("Value At Cost")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ValueAtMarketPrice)
                    .HasColumnName("Value At Market Price")
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IciciNro>(entity =>
            {
                entity.Property(e => e.AverageCostPrice)
                    .HasColumnName("Average Cost Price")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Category)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Column15)
                    .HasColumnName("Column 15")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Fund)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LastRecordedNav)
                    .HasColumnName("Last Recorded NAV *")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.LastRecordedNavOn)
                    .HasColumnName("Last Recorded NAV On")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ProfitLoss)
                    .HasColumnName("Profit  Loss")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ProfitLoss1)
                    .HasColumnName("Profit  Loss %")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Rating)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.RealizedProfitLoss)
                    .HasColumnName("Realized Profit Loss")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ResearchRecommends)
                    .HasColumnName("Research Recommends")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Scheme)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.SubCategory)
                    .HasColumnName("Sub Category")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UnitsHeld)
                    .HasColumnName("Units Held")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ValueAtCost)
                    .HasColumnName("Value At Cost")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ValueAtNav)
                    .HasColumnName("Value at NAV")
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Savings>(entity =>
            {
                entity.Property(e => e.Account).IsRequired();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<Ticker>(entity =>
            {
                entity.Property(e => e.SchemeCode)
                    .HasColumnName("scheme_code")
                    .HasMaxLength(20);

                entity.Property(e => e.SchemeName)
                    .HasColumnName("scheme_name")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TickerHistory>(entity =>
            {
                entity.Property(e => e.Daterecorded)
                    .HasColumnName("daterecorded")
                    .HasColumnType("datetime");

                entity.Property(e => e.SchemeCode)
                    .HasColumnName("scheme_code")
                    .HasMaxLength(20);

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("decimal(18, 9)");
            });

            modelBuilder.Entity<YesBankWm>(entity =>
            {
                entity.Property(e => e.AmountInvested)
                    .HasColumnName("Amount_Invested")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryCode)
                    .HasColumnName("category_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentValue)
                    .HasColumnName("current_value")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dividend)
                    .HasColumnName("dividend")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HniCatIrr)
                    .HasColumnName("hni_cat_irr")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HniCd)
                    .HasColumnName("hni_cd")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.HniNm)
                    .HasColumnName("hni_nm")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HniProdIrr)
                    .HasColumnName("hni_prod_irr")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IrrPercentage)
                    .HasColumnName("irr_percentage")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nav)
                    .HasColumnName("NAV")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.PerTotal)
                    .HasColumnName("Per_Total")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProdDesc)
                    .HasColumnName("Prod_Desc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProdDescCd)
                    .HasColumnName("Prod_Desc_Cd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RealisedGainLoss)
                    .HasColumnName("realised_gain_loss")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SchemeCode)
                    .HasColumnName("scheme_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SchemeName)
                    .HasColumnName("scheme_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Units)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UnrealisedGainLoss)
                    .HasColumnName("unrealised_gain_loss")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValDate)
                    .HasColumnName("Val_date")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WeightNav)
                    .HasColumnName("Weight_NAV")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
