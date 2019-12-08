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
                .Property(CF => CF.id)
                .HasDefaultValue(Guid.NewGuid());
        }
    }

    public class CompanyFinancials
    {
        [Key]
        public Guid id { get; set; }
        public string vat { get; set; }
        public string year { get; set; }
        public decimal employees { get; set; }
        public decimal turnover { get; set; }
        public decimal equity { get; set; }
        public decimal current_assets { get; set; }
        public decimal tangible_fixed_assets { get; set; }
        public decimal gain_loss_period { get; set; }
        // Ratios
        public decimal net_assets { get; set; }
        public decimal return_on_equity { get; set; }
        public decimal gross_operating_sales_margin { get; set; }
        public decimal net_operating_sales_margin { get; set; }
        public decimal rotation_fixed_assets { get; set; }
        public decimal total_assets_to_turnover { get; set; }
        public decimal current_ratio { get; set; }
        public decimal quick_ratio { get; set; }
        public decimal immediate_liquidity { get; set; }
        public decimal write_downs_to_added_value { get; set; }
        public decimal net_cash { get; set; }
        public decimal net_cash_ratio { get; set; }
        public decimal net_current_assets { get; set; }
        public decimal cash_flow { get; set; }
        public decimal number_days_customer_credit { get; set; }
        public decimal number_days_supplier_credit { get; set; }
        public decimal investments { get; set; }
        public decimal dfl { get; set; }
        public decimal own_assets_to_capital { get; set; }
        public decimal cash_flow_to_debt { get; set; }
        public decimal cash_flow_to_long_term_debt { get; set; }
        public decimal long_term_debt_to_short_term_debt { get; set; }
        public decimal debt_repayment { get; set; }
        public decimal self_financing_degree { get; set; }
        public decimal outstanding_tax_to_liabilities { get; set; }
        public decimal added_value { get; set; }
        public decimal payroll_costs_to_added_value { get; set; }
        public decimal financial_charges_to_added_value { get; set; }
        public decimal net_profit_to_added_value { get; set; }
        public decimal added_value_to_operating_income { get; set; }
        public decimal investment_ratio { get; set; }
        public decimal added_value_per_employee { get; set; }
    }

    public class Address
    {
        public object municipality { get; set; }
        public string street { get; set; }
        public object box { get; set; }
        public string country_code { get; set; }
        public string zip_code { get; set; }
        public string house_number { get; set; }
        public string full_address { get; set; }
    }

    public class Revisor
    {
        public string type { get; set; }
        public string company_name { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public object profession { get; set; }
        public string id_number { get; set; }
        public Address address { get; set; }
        public object mandate_code { get; set; }
        public object mandate_start_date { get; set; }
        public object mandate_end_date { get; set; }
        public string schema { get; set; }
    }

    public class Ratios
    {
        public object net_assets { get; set; }
        public object return_on_equity { get; set; }
        public object gross_operating_sales_margin { get; set; }
        public object net_operating_sales_margin { get; set; }
        public object rotation_fixed_assets { get; set; }
        public object total_assets_to_turnover { get; set; }
        public object current_ratio { get; set; }
        public object quick_ratio { get; set; }
        public object immediate_liquidity { get; set; }
        public object write_downs_to_added_value { get; set; }
        public object net_cash { get; set; }
        public object net_cash_ratio { get; set; }
        public object net_current_assets { get; set; }
        public object cash_flow { get; set; }
        public object number_days_customer_credit { get; set; }
        public object number_days_supplier_credit { get; set; }
        public object investments { get; set; }
        public object dfl { get; set; }
        public object own_assets_to_capital { get; set; }
        public object cash_flow_to_debt { get; set; }
        public object cash_flow_to_long_term_debt { get; set; }
        public object long_term_debt_to_short_term_debt { get; set; }
        public object debt_repayment { get; set; }
        public object self_financing_degree { get; set; }
        public object outstanding_tax_to_liabilities { get; set; }
        public object added_value { get; set; }
        public object payroll_costs_to_added_value { get; set; }
        public object financial_charges_to_added_value { get; set; }
        public object net_profit_to_added_value { get; set; }
        public object added_value_to_operating_income { get; set; }
        public object investment_ratio { get; set; }
        public object added_value_per_employee { get; set; }
    }

    public class SocialBalance
    {
        public object employees_worked_hours { get; set; }
        public object advantages_addition_wages { get; set; }
        public object professional_training_hours { get; set; }
        public object professional_training_net_costs { get; set; }
        public object average_number_of_employees_fte { get; set; }
        public object employees_end_of_year_management_fte { get; set; }
        public object employees_end_of_year { get; set; }
        public object employees_end_of_year_workers_fte { get; set; }
        public object employees_end_of_year_others_fte { get; set; }
        public object employees_new_fte { get; set; }
        public object employees_end_of_year_men_fte { get; set; }
        public object employees_end_of_year_women_fte { get; set; }
        public object employees_end_of_year_university_fte { get; set; }
        public object employees_end_of_year_non_university_fte { get; set; }
        public object employees_end_of_year_secondary_education_fte { get; set; }
        public object employees_end_of_year_primary_education_fte { get; set; }
        public object employees_contract_indefinite_period_fte { get; set; }
        public object employees_contract_definite_period_fte { get; set; }
        public object employees_contract_termination_fte { get; set; }
        public object employees_contract_termination_retirement_fte { get; set; }
        public object employees_contract_termination_early_retirement_fte { get; set; }
        public object employees_contract_termination_dismissal { get; set; }
        public object employees_contract_termination_other_reason { get; set; }
        public object hired_temporary_staff_fte { get; set; }
        public object hired_temporary_staff_worked_hours { get; set; }
        public object hired_temporary_staff_costs { get; set; }
    }

    public class FinancialValues
    {
        public object fixed_assets { get; set; }
        public object number_of_employees { get; set; }
        public object number_of_employees_men { get; set; }
        public object number_of_employees_women { get; set; }
        public object financial_fixed_assets { get; set; }
        public object currents_assets { get; set; }
        public object amounts_receivable_more_one_year { get; set; }
        public object trade_debtors_within_one_year { get; set; }
        public object equity { get; set; }
        public object amounts_payable_within_one_year { get; set; }
        public object trade_debts_payable_within_one_year { get; set; }
        public object turnover { get; set; }
        public object gross_operating_margin { get; set; }
        public object increase_decrease_stocks_work_contracts_progress { get; set; }
        public object contributions_gifts_legacies_grants { get; set; }
        public object remuneration_social_security_pensions { get; set; }
        public object depreciation_other_amounts_written_down_formation_expenses_objectangible_tangible_fixed_assets { get; set; }
        public object amounts_written_down_stocks_contracts_progress_trade_debtors_appropriations_write_backs { get; set; }
        public object provisions_risks_charges_appropriations_write_backs { get; set; }
        public object operating_charges { get; set; }
        public object operating_profit_loss { get; set; }
        public object financial_income { get; set; }
        public object financial_charges { get; set; }
        public object extraordinary_income { get; set; }
        public object extraordinary_charges { get; set; }
        public object income_taxes { get; set; }
        public object gain_loss_period { get; set; }
        public object tangible_fixed_assets_acquisition_including_produced_fixed_assets { get; set; }
        public object tangible_fixed_assets_revaluation_gains_third_parties { get; set; }
        public object tangible_fixed_assets_depreciations_amount_written_down_acquired_third_party { get; set; }
        public object outstanding_taxes_payable_due_tax_authorities { get; set; }
        public object remuneration_social_security_recipient_national_social_security_office { get; set; }
        public object amounts_receivable_within_one_year { get; set; }
        public object accrued_charges_deferred_income { get; set; }
        public object current_investments { get; set; }
        public object cash_bank_hand { get; set; }
        public object equity_liabilities { get; set; }
        public object operating_income { get; set; }
        public object other_operating_income { get; set; }
        public object operating_subsidies_compensatory_amounts { get; set; }
        public object capital_subsidies_granted_public_authorities_recorded_income_period { get; set; }
        public object raw_materials_consumables { get; set; }
        public object services_other_goods { get; set; }
        public object hired_temporary_staff_costs_enterprise { get; set; }
        public object persons_placed_enterprises_disposal_costs_enterprise { get; set; }
        public object employees_recorded_personnel_register_average_number_employees_calculated_full_time_equivalents { get; set; }
        public object stock_raw_materials_consumables { get; set; }
        public object stock_goods_purchased_resale { get; set; }
        public object stock_immovable_property_objectended_sale { get; set; }
        public object advance_payments_purchases_stocks { get; set; }
        public object stocks_contracts_progress { get; set; }
        public object deferred_charges_accrued_income { get; set; }
        public object own_construction_capitalised { get; set; }
        public object stock_work_progress { get; set; }
        public object stock_finished_goods { get; set; }
        public object contracts_progress { get; set; }
        public object personal_guarantees_provided_or_irrevocably_promised_by_enterprise_as_security_debts_commitments_third_parties_bills_exchange_circulation_endorsed_by_enterprise { get; set; }
        public object value_added_taxes_charged_by_enterprise { get; set; }
        public object purchases_raw_materials_consumables { get; set; }
        public object value_added_taxes_charged_to_enterprise { get; set; }
        public object amounts_written_down_current_assets { get; set; }
        public object provisions_financial_nature_appropriations { get; set; }
        public object provisions_financial_nature_uses_write_backs { get; set; }
        public object extraordinary_depreciation_extraordinary_amounts_written_down_formation_expenses_objectangible_tangible_fixed_assets { get; set; }
        public object amounts_written_down_financial_fixed_assets { get; set; }
        public object provisions_extraordinary_liabilities_charges_appropriations_uses { get; set; }
        public object write_back_depreciation_amounts_written_down_objectangible_tangible_fixed_assets { get; set; }
        public object write_back_amounts_written_down_financial_fixed_assets { get; set; }
        public object write_back_provisions_extraordinary_liabilities_charges { get; set; }
        public object losses_disposal_fixed_assets { get; set; }
        public object income_taxes_result_period { get; set; }
        public object transfer_from_deferred_taxes { get; set; }
        public object transfer_to_deferred_taxes { get; set; }
        public object assets { get; set; }
        public object objecterest_subsidies_granted_public_authorities_recorded_income_period { get; set; }
        public object charges_discounting_amounts_receivable { get; set; }
        public object debt_charges { get; set; }
        public object depreciation_loan_issue_expenses_reimbursement_premiums { get; set; }
        public object provisions_pensions_similar_obligations_appropriations_write_backs { get; set; }
        public object capital { get; set; }
        public object current_portion_amounts_payable_more_one_year_falling_due_within_one_year { get; set; }
        public object amounts_payable_more_one_year { get; set; }
        public object provisions_deferred_taxes { get; set; }
        public object amounts_payable { get; set; }
        public object reserves { get; set; }
        public object accumulated_profits_losses { get; set; }
        public object financial_debts_payable_within_one_year { get; set; }
        public object formation_expenses { get; set; }
        public object objectangible_fixed_assets { get; set; }
        public object tangible_fixed_assets { get; set; }
        public object land_buildings_acquisition_including_produced_fixed_assets { get; set; }
        public object plant_machinery_equipment_acquisition_including_produced_fixed_assets { get; set; }
        public object furniture_vehicles_acquisition_including_produced_fixed_assets { get; set; }
        public object leasing_other_similar_rights_acquisition_including_produced_fixed_assets { get; set; }
        public object other_tangible_fixed_assets_acquisition_including_produced_fixed_assets { get; set; }
        public object tangible_fixed_assets_under_construction_advance_payments_acquisition_including_produced_fixed_assets { get; set; }
        public object land_buildings_revaluation_gains_third_parties { get; set; }
        public object plant_machinery_equipment_revaluation_gains_third_parties { get; set; }
        public object furniture_vehicles_revaluation_gains_third_parties { get; set; }
        public object leasing_other_similar_rights_revaluation_gains_third_parties { get; set; }
        public object other_tangible_fixed_assets_revaluation_gains_third_parties { get; set; }
        public object tangible_fixed_assets_under_construction_advance_payments_revaluation_gains_third_parties { get; set; }
        public object land_buildings_depreciations_amount_written_down_acquired_third_party { get; set; }
        public object plant_machinery_equipment_depreciations_amount_written_down_acquired_third_party { get; set; }
        public object furniture_vehicles_depreciations_amount_written_down_acquired_third_party { get; set; }
        public object leasing_other_similar_rights_depreciations_amount_written_down_acquired_third_party { get; set; }
        public object other_tangible_fixed_assets_depreciations_amount_written_down_acquired_third_party { get; set; }
        public object tangible_fixed_assets_under_construction_advance_payments_depreciations_amount_written_down_acquired_third_party { get; set; }
        public object employees_recorded_personnel_register_total_number_closing_date { get; set; }
        public object number_employees_personnel_register_closing_date_financial_year_men_full_time { get; set; }
        public object number_employees_personnel_register_closing_date_financial_year_women_full_time { get; set; }
        public object number_employees_personnel_register_closing_date_financial_year_men_part_time { get; set; }
        public object number_employees_personnel_register_closing_date_financial_year_women_part_time { get; set; }
        public object investments { get; set; }
        public object added_value { get; set; }
    }

    public class CompanyFinancialsYear
    {
        public string uri { get; set; }
        public string identifier { get; set; }
        public object year { get; set; }
        public object employees { get; set; }
        public object turnover { get; set; }
        public object equity { get; set; }
        public List<Revisor> revisors { get; set; }
        public Ratios ratios { get; set; }
        public object account_date { get; set; }
        public object current_assets { get; set; }
        public object gross_operating_margin { get; set; }
        public object tangible_fixed_assets { get; set; }
        public object gain_loss_period { get; set; }
        public object current_ratio { get; set; }
        public object net_cash { get; set; }
        public object self_financing_degree { get; set; }
        public object return_on_equity { get; set; }
        public object added_value { get; set; }
        public object balance_type { get; set; }
        public SocialBalance social_balance { get; set; }
        public FinancialValues financial_values { get; set; }
    }

    public class CompanyFinancialsRoot
    {
        public object total { get; set; }
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
