using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class RegisterFrom
{
    public int RegisterId { get; set; }

    public int GeneralId { get; set; }

    public int? AddressId { get; set; }

    public int? ShippingId { get; set; }

    public int? BusitypeId { get; set; }

    public int? CreditinfoId { get; set; }

    public int? DoccreditId { get; set; }

    public int? CustsignId { get; set; }

    public int? ShopType { get; set; }

    public int? IndustryType { get; set; }

    public int? CustGroupType { get; set; }

    public int? CustGroupCountry { get; set; }

    public int? Company { get; set; }

    public int? AccountGroup { get; set; }

    public int? AccountCode { get; set; }

    public int? CashGroup { get; set; }

    public int? SortKey { get; set; }

    public int? SaleOrg { get; set; }

    public int? SaleGroup { get; set; }

    public int? SaleDistrict { get; set; }

    public int? SalePerson { get; set; }

    public int? SaleManager { get; set; }

    public int? PaymentMethod { get; set; }

    public int? TermOfPay { get; set; }

    public int? Currency { get; set; }

    public int? ExchRateType { get; set; }

    public int? Incoterms { get; set; }

    public int? PriceList { get; set; }

    public int? CustPricProc { get; set; }

    public string? ContactPersonName { get; set; }

    public string? ContactPersonTel { get; set; }

    public string? PlType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual AccountCode? AccountCodeNavigation { get; set; }

    public virtual AccountGroup? AccountGroupNavigation { get; set; }

    public virtual Address? Address { get; set; }

    public virtual BusinessType? Busitype { get; set; }

    public virtual CashGroup? CashGroupNavigation { get; set; }

    public virtual Company? CompanyNavigation { get; set; }

    public virtual CreditInfo? Creditinfo { get; set; }

    public virtual Currency? CurrencyNavigation { get; set; }

    public virtual CustGroupCountry? CustGroupCountryNavigation { get; set; }

    public virtual CustGroupType? CustGroupTypeNavigation { get; set; }

    public virtual CustPricProc? CustPricProcNavigation { get; set; }

    public virtual CustomerSign? Custsign { get; set; }

    public virtual DocumentCredit? Doccredit { get; set; }

    public virtual ExchRateType? ExchRateTypeNavigation { get; set; }

    public virtual General General { get; set; } = null!;

    public virtual Incoterm? IncotermsNavigation { get; set; }

    public virtual IndustryType? IndustryTypeNavigation { get; set; }

    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }

    public virtual PriceList? PriceListNavigation { get; set; }

    public virtual SaleDistrict? SaleDistrictNavigation { get; set; }

    public virtual SaleGroup? SaleGroupNavigation { get; set; }

    public virtual SaleMaster? SaleManagerNavigation { get; set; }

    public virtual SaleOrg? SaleOrgNavigation { get; set; }

    public virtual SalePerson? SalePersonNavigation { get; set; }

    public virtual Shipping? Shipping { get; set; }

    public virtual ShopType? ShopTypeNavigation { get; set; }

    public virtual SortKey? SortKeyNavigation { get; set; }

    public virtual TermOfPay? TermOfPayNavigation { get; set; }
}
