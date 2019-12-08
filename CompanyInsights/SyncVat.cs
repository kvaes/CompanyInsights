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
            var companiesArray = _context.CompanyFinancials.Where(CF => CF.vat == InputVAT).OrderBy(cf => cf.vat).ToArray();
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
            var companiesArray = _context.CompanyFinancials.OrderBy(cf => cf.vat).ToArray();
            return new OkObjectResult(companiesArray);
        }

        public async Task RetrieveCompanyFinancialsFromSourceAsync(ILogger log, string InputVAT) {
            log.LogInformation("RetrieveCompanyFinancialsFromSourceAsync");
            string CompanyFinancialsApiBase = Environment.GetEnvironmentVariable("CompanyFinancialsApiBase");
            string CompanyFinancialsApiPathPrefix = Environment.GetEnvironmentVariable("CompanyFinancialsApiPathPrefix");
            string CompanyFinancialsApiPathSuffix = Environment.GetEnvironmentVariable("CompanyFinancialsApiPathSuffix");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(CompanyFinancialsApiBase);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            client.DefaultRequestHeaders.Add("API_ID", Environment.GetEnvironmentVariable("api-appid"));
            client.DefaultRequestHeaders.Add("API_KEY", Environment.GetEnvironmentVariable("api-appkey"));
            CancellationToken cts;
            HttpResponseMessage response = await client.GetAsync(CompanyFinancialsApiPathPrefix+InputVAT+CompanyFinancialsApiPathSuffix);
            //HttpResponseMessage response = await client.GetAsync("https://kvaesdataapi.blob.core.windows.net/sample/response.json");
            if (response.IsSuccessStatusCode)
            {
                CompanyFinancialsRoot result = await response.Content.ReadAsAsync<CompanyFinancialsRoot>();
                string ResultYear;
                foreach (CompanyFinancialsYear details in result.list) { 
                    if (!DoesCompanyFinancialsYearExist(log, InputVAT, details.year.ToString())) {
                        ResultYear = details.year.ToString();
                        log.LogInformation($"Syncing year {ResultYear} of VAT {InputVAT}");
                        Ratios ratios = details.ratios;
                        CompanyFinancials CF = new CompanyFinancials
                        {
                            id = Guid.NewGuid(),
                            vat = InputVAT,
                            year = ResultYear,
                            employees = ParseCompanyFinancialsDetail(details.employees),
                            turnover = ParseCompanyFinancialsDetail(details.turnover),
                            equity = ParseCompanyFinancialsDetail(details.equity),
                            current_assets = ParseCompanyFinancialsDetail(details.current_assets),
                            tangible_fixed_assets = ParseCompanyFinancialsDetail(details.tangible_fixed_assets),
                            gain_loss_period = ParseCompanyFinancialsDetail(details.gain_loss_period),

                            net_assets = ParseCompanyFinancialsDetail(ratios.net_assets),
                            return_on_equity = ParseCompanyFinancialsDetail(ratios.return_on_equity),
                            gross_operating_sales_margin = ParseCompanyFinancialsDetail(ratios.gross_operating_sales_margin),
                            net_operating_sales_margin = ParseCompanyFinancialsDetail(ratios.net_operating_sales_margin),
                            rotation_fixed_assets = ParseCompanyFinancialsDetail(ratios.rotation_fixed_assets),
                            total_assets_to_turnover = ParseCompanyFinancialsDetail(ratios.total_assets_to_turnover),
                            current_ratio = ParseCompanyFinancialsDetail(ratios.current_ratio),
                            quick_ratio = ParseCompanyFinancialsDetail(ratios.quick_ratio),
                            immediate_liquidity = ParseCompanyFinancialsDetail(ratios.immediate_liquidity),
                            write_downs_to_added_value = ParseCompanyFinancialsDetail(ratios.write_downs_to_added_value),
                            net_cash = ParseCompanyFinancialsDetail(ratios.net_cash),
                            net_cash_ratio = ParseCompanyFinancialsDetail(ratios.net_cash_ratio),
                            net_current_assets = ParseCompanyFinancialsDetail(ratios.net_current_assets),
                            cash_flow = ParseCompanyFinancialsDetail(ratios.cash_flow),
                            number_days_customer_credit = ParseCompanyFinancialsDetail(ratios.number_days_customer_credit),
                            number_days_supplier_credit = ParseCompanyFinancialsDetail(ratios.number_days_supplier_credit),
                            investments = ParseCompanyFinancialsDetail(ratios.investments),
                            dfl = ParseCompanyFinancialsDetail(ratios.dfl),
                            own_assets_to_capital = ParseCompanyFinancialsDetail(ratios.own_assets_to_capital),
                            cash_flow_to_debt = ParseCompanyFinancialsDetail(ratios.cash_flow_to_debt),
                            cash_flow_to_long_term_debt = ParseCompanyFinancialsDetail(ratios.cash_flow_to_long_term_debt),
                            long_term_debt_to_short_term_debt = ParseCompanyFinancialsDetail(ratios.long_term_debt_to_short_term_debt),
                            debt_repayment = ParseCompanyFinancialsDetail(ratios.debt_repayment),
                            self_financing_degree = ParseCompanyFinancialsDetail(ratios.self_financing_degree),
                            outstanding_tax_to_liabilities = ParseCompanyFinancialsDetail(ratios.outstanding_tax_to_liabilities),
                            added_value = ParseCompanyFinancialsDetail(ratios.added_value),
                            payroll_costs_to_added_value = ParseCompanyFinancialsDetail(ratios.payroll_costs_to_added_value),
                            financial_charges_to_added_value = ParseCompanyFinancialsDetail(ratios.financial_charges_to_added_value),
                            net_profit_to_added_value = ParseCompanyFinancialsDetail(ratios.net_profit_to_added_value),
                            added_value_to_operating_income = ParseCompanyFinancialsDetail(ratios.added_value_to_operating_income),
                            investment_ratio = ParseCompanyFinancialsDetail(ratios.investment_ratio),
                            added_value_per_employee = ParseCompanyFinancialsDetail(ratios.added_value_per_employee)
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
            CompanyFinancials cf = _context.CompanyFinancials.Where(CF => CF.vat == InputVAT).Where(CF => CF.year == InputYear).FirstOrDefault();
            if (cf != null) {
                log.LogInformation($"VAT {InputVAT} FOUND in year {InputYear}");
                return true;
            }
            log.LogInformation($"VAT {InputVAT} NOT FOUND in year {InputYear}");
            return false;
        }

        public bool DoesCompanyFinancialsExist(ILogger log, string InputVAT)
        {
            CompanyFinancials cf = _context.CompanyFinancials.Where(CF => CF.vat == InputVAT).FirstOrDefault();
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
