using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(CompanyInsights.Startup))]

namespace CompanyInsights
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("kvaesdataapidb");
            builder.Services.AddDbContext<CompanyInsightsContext>(
                options => options.UseSqlServer(SqlConnection));
        }
    }
}
