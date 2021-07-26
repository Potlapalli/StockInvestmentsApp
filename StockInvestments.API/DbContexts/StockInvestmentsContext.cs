using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockInvestments.API.Entities;

namespace StockInvestments.API.DbContexts
{
    public class StockInvestmentsContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public StockInvestmentsContext(DbContextOptions<StockInvestmentsContext> options)
            : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<CurrentPosition> CurrentPositions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<SoldPosition> SoldPositions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ClosedPosition> ClosedPositions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<StockEarning> StockEarnings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentPosition>().HasData(new CurrentPosition
                {
                    Ticker = "NKLA",
                    Company = "Nikola",
                    PurchasePrice = 81.625,
                    TotalShares = 2,
                    TotalAmount = 163.25
                }, new CurrentPosition
                {
                    Ticker = "ETSY",
                    Company = "Etsy",
                    PurchasePrice = 204.79,
                    TotalShares = 2,
                    TotalAmount = 409.58
                }, new CurrentPosition
                {
                    Ticker = "AAPL",
                    Company = "Apple",
                    PurchasePrice = 142.09,
                    TotalShares = 5,
                    TotalAmount = 710.45

                }, new CurrentPosition
                {
                    Ticker = "OSTK",
                    Company = "Overstock",
                    PurchasePrice = 91.63,
                    TotalShares = 20,
                    TotalAmount = 1832.6
                }
            );

            modelBuilder.Entity<SoldPosition>().HasData(new SoldPosition
            {
                Number = 1,
                Ticker = "OSTK",
                SellingPrice = 91.77,
                TotalShares = 10,
                TotalAmount = 917.7
            }
            );


            modelBuilder.Entity<ClosedPosition>().HasData(new ClosedPosition
                {
                    Number = 1,
                    Ticker = "NVAX",
                    Company = "Novavax",
                    FinalValue = 185
                }, new ClosedPosition
                {
                    Number = 2,
                    Ticker = "W",
                    Company = "WayFair",
                    FinalValue = -3.8099
                }, new ClosedPosition
                {
                    Number = 3,
                    Ticker = "SHOP",
                    Company = "Shopify",
                    FinalValue = 10.54946
                }
            );
        }
    }
}
