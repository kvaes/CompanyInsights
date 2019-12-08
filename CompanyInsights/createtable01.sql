/****** Object:  Table [dbo].[CompanyFinancials]    Script Date: 08/12/2019 20:27:38 ******/
DROP TABLE [dbo].[CompanyFinancials]
GO

/****** Object:  Table [dbo].[CompanyFinancials]    Script Date: 08/12/2019 20:27:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CompanyFinancials](
	[id] [uniqueidentifier] NOT NULL,
	[vat] [nvarchar](50) NOT NULL,
	[year] [nvarchar](50) NOT NULL,
	[employees] [numeric](18, 0) NULL,
	[turnover] [numeric](18, 0) NULL,
	[equity] [numeric](18, 0) NULL,
	[current_assets] [numeric](18, 0) NULL,
	[tangible_fixed_assets] [numeric](18, 0) NULL,
	[gain_loss_period] [numeric](18, 0) NULL,

	[net_assets] [numeric](18, 0) NULL,
    [return_on_equity] [numeric](18, 0) NULL,
    [gross_operating_sales_margin] [numeric](18, 0) NULL,
    [net_operating_sales_margin] [numeric](18, 0) NULL,
    [rotation_fixed_assets] [numeric](18, 0) NULL,
    [total_assets_to_turnover] [numeric](18, 0) NULL,
    [current_ratio] [numeric](18, 0) NULL,
    [quick_ratio] [numeric](18, 0) NULL,
    [immediate_liquidity] [numeric](18, 0) NULL,
    [write_downs_to_added_value] [numeric](18, 0) NULL,
    [net_cash] [numeric](18, 0) NULL,
    [net_cash_ratio] [numeric](18, 0) NULL,
    [net_current_assets] [numeric](18, 0) NULL,
    [cash_flow] [numeric](18, 0) NULL,
    [number_days_customer_credit] [numeric](18, 0) NULL,
    [number_days_supplier_credit] [numeric](18, 0) NULL,
    [investments] [numeric](18, 0) NULL,
    [dfl] [numeric](18, 0) NULL,
    [own_assets_to_capital] [numeric](18, 0) NULL,
    [cash_flow_to_debt] [numeric](18, 0) NULL,
    [cash_flow_to_long_term_debt] [numeric](18, 0) NULL,
    [long_term_debt_to_short_term_debt] [numeric](18, 0) NULL,
    [debt_repayment] [numeric](18, 0) NULL,
    [self_financing_degree] [numeric](18, 0) NULL,
    [outstanding_tax_to_liabilities] [numeric](18, 0) NULL,
    [added_value] [numeric](18, 0) NULL,
    [payroll_costs_to_added_value] [numeric](18, 0) NULL,
    [financial_charges_to_added_value] [numeric](18, 0) NULL,
    [net_profit_to_added_value] [numeric](18, 0) NULL,
    [added_value_to_operating_income] [numeric](18, 0) NULL,
    [investment_ratio] [numeric](18, 0) NULL,
    [added_value_per_employee] [numeric](18, 0) NULL,
 CONSTRAINT [PK_CompanyFinancials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


