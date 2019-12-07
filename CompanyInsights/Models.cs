using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace CompanyInsights
{
    public class CompanyInsightsContext : DbContext
    {
        public CompanyInsightsContext(DbContextOptions<CompanyInsightsContext> options)
            : base(options)
        { }

        public DbSet<CompanyFinancials> CompanyFinancials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyFinancials>()
                .Property(CF => CF.ID)
                .HasDefaultValue(Guid.NewGuid());
        }
    }

    public class CompanyFinancials
    {
        [Key]
        public Guid ID { get; set; }
        public string VAT { get; set; }
        public string Year { get; set; }
        public decimal Employees { get; set; }
        public decimal Turnover { get; set; }
        public decimal Equity { get; set; }
        public decimal CurrentAssets { get; set; }
        public decimal GrossOperatingMargin { get; set; }
        public decimal TangibleFixedAssets { get; set; }
        public decimal GainLossPeriod { get; set; }
        public decimal CurrentRatio { get; set; }
        public decimal NetCash { get; set; }
        public decimal SelfFinancingDegree { get; set; }
        public decimal ReturnOnEquity { get; set; }
        public decimal AddedValue { get; set; }
    }

    public class CompanyFinancialsYear
    {
        public string uri { get; set; }
        public string identifier { get; set; }
        public int year { get; set; }
        public int? employees { get; set; }
        public object turnover { get; set; }
        public int equity { get; set; }
        public List<object> revisors { get; set; }
        public string account_date { get; set; }
        public int current_assets { get; set; }
        public int gross_operating_margin { get; set; }
        public int? tangible_fixed_assets { get; set; }
        public int gain_loss_period { get; set; }
        public object current_ratio { get; set; }
        public object net_cash { get; set; }
        public double? self_financing_degree { get; set; }
        public double return_on_equity { get; set; }
        public object added_value { get; set; }
    }

    public class CompanyFinancialsRoot
    {
        public int total { get; set; }
        public bool vat_valid { get; set; }
        public List<CompanyFinancialsYear> list { get; set; }
        public string vat_input { get; set; }
        public string vat_clean { get; set; }
        public string vat_formatted { get; set; }
    }

    public class CompanyInsightsContextFactory : IDesignTimeDbContextFactory<CompanyInsightsContext>
    {
        public CompanyInsightsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CompanyInsightsContext>();
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("kvaesdataapidb"));

            return new CompanyInsightsContext(optionsBuilder.Options);
        }
    }
}
