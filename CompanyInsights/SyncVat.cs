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
            CancellationToken cts;
            HttpResponseMessage response = await client.GetAsync(CompanyFinancialsApiPath);
            if (response.IsSuccessStatusCode)
            {
                CompanyFinancialsRoot result = await response.Content.ReadAsAsync<CompanyFinancialsRoot>();
                string ResultYear;
                foreach (CompanyFinancialsYear details in result.list) { 
                    if (!DoesCompanyFinancialsYearExist(log, InputVAT, details.year.ToString())) {
                        ResultYear = details.year.ToString();
                        log.LogInformation($"Syncing year {ResultYear} of VAT {InputVAT}");
                        /*log.LogInformation(ParseCompanyFinancialsDetail(details.employees).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.turnover).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.equity).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.current_assets).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.gross_operating_margin).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.tangible_fixed_assets).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.gain_loss_period).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.current_ratio).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.net_cash).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.self_financing_degree).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.return_on_equity).ToString());
                        log.LogInformation(ParseCompanyFinancialsDetail(details.added_value).ToString());*/
                        CompanyFinancials CF = new CompanyFinancials
                        {
                            ID = Guid.NewGuid(),
                            VAT = InputVAT,
                            Year = ResultYear,
                            Employees = ParseCompanyFinancialsDetail(details.employees),
                            Turnover = ParseCompanyFinancialsDetail(details.turnover),
                            Equity = ParseCompanyFinancialsDetail(details.equity),
                            CurrentAssets = ParseCompanyFinancialsDetail(details.current_assets),
                            GrossOperatingMargin = ParseCompanyFinancialsDetail(details.gross_operating_margin),
                            TangibleFixedAssets = ParseCompanyFinancialsDetail(details.tangible_fixed_assets),
                            GainLossPeriod = ParseCompanyFinancialsDetail(details.gain_loss_period),
                            CurrentRatio = ParseCompanyFinancialsDetail(details.current_ratio),
                            NetCash = ParseCompanyFinancialsDetail(details.net_cash),
                            SelfFinancingDegree = ParseCompanyFinancialsDetail(details.self_financing_degree),
                            ReturnOnEquity = ParseCompanyFinancialsDetail(details.return_on_equity),
                            AddedValue = ParseCompanyFinancialsDetail(details.added_value)
                        };
                        var entity = await _context.CompanyFinancials.AddAsync(CF, cts);
                        await _context.SaveChangesAsync(cts);
                    }
                }
            }
        }

        public Decimal ParseCompanyFinancialsDetail(dynamic detail) {
            if (detail != null) {
                return Decimal.Parse(Convert.ToString(detail));
            } else {
                return 0;
            }
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
