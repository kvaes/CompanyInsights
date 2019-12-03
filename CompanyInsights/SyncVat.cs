using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CompanyInsights
{
    public class HttpTrigger
    {
        private readonly CompanyInsightsContext _context;
        public HttpTrigger(CompanyInsightsContext context)
        {
            _context = context;
        }

        [FunctionName("GetCompanyFinancials")]
        public IActionResult GetCompanyFinancials(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "getcompanyfinancials/{InputVAT}")] HttpRequest req,
        string InputVAT,
        ILogger log)
        {
            log.LogInformation("GetCompanyFinancials");
            var companiesArray = _context.CompanyFinancials.Where(CF => CF.VAT == InputVAT).OrderBy(cf => cf.VAT).ToArray();
            return new OkObjectResult(companiesArray);
        }

        [FunctionName("SyncCompanyFinancials")]
        public async Task<IActionResult> PostPostAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "synccompanyfinancials/{InputVAT}")] HttpRequest req,
        string InputVAT,
        ILogger log)
        {
            log.LogInformation("SyncCompanyFinancials");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // Post data = JsonConvert.DeserializeObject<Post>(requestBody);
            if (!DoesCompanyFinancialsExist(log, InputVAT)) { 
                await RetrieveCompanyFinancialsFromSourceAsync(log, InputVAT);
            }
            var companiesArray = _context.CompanyFinancials.OrderBy(cf => cf.VAT).ToArray();
            return new OkObjectResult(companiesArray);
        }

        public async Task RetrieveCompanyFinancialsFromSourceAsync(ILogger log, string InputVAT) {
            log.LogInformation("RetrieveCompanyFinancialsFromSourceAsync");
            string CompanyFinancialsApiBase = Environment.GetEnvironmentVariable("CompanyFinancialsApiBase");
            string CompanyFinancialsApiPath = Environment.GetEnvironmentVariable("CompanyFinancialsApiPath");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CompanyFinancialsApiBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            HttpResponseMessage response = await client.GetAsync(CompanyFinancialsApiPath);
            if (response.IsSuccessStatusCode)
            {
                CompanyFinancialsRoot result = await response.Content.ReadAsAsync<CompanyFinancialsRoot>();
                foreach (CompanyFinancialsYear details in result.list) { 
                    log.LogInformation(result.ToString());
                }
            }
            //DoesCompanyFinancialsExist(log, "test", "2018");
            //DoesCompanyFinancialsExist(log, "test", "2019");
        }

        public bool DoesCompanyFinancialsYearExist(ILogger log, string InputVAT, string InputYear) {
            CompanyFinancials cf = _context.CompanyFinancials.Where(CF => CF.VAT == InputVAT).Where(CF => CF.Year == InputYear).FirstOrDefault();
            if (cf != null) {
                log.LogInformation($"VAT {InputVAT} FOUND in year {InputYear}");
                return true;
            }
            log.LogInformation($"VAT {InputVAT} NOT FOUND in year {InputYear}");
            return false;
        }

        public bool DoesCompanyFinancialsExist(ILogger log, string InputVAT)
        {
            CompanyFinancials cf = _context.CompanyFinancials.Where(CF => CF.VAT == InputVAT).FirstOrDefault();
            if (cf != null)
            {
                log.LogInformation($"VAT {InputVAT} FOUND");
                return true;
            }
            log.LogInformation($"VAT {InputVAT} NOT FOUND");
            return false;
        }
    }
}
