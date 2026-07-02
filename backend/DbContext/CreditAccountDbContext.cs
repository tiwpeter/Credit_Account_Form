using System;
using System.Collections.Generic;
using CreditAccountApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreditAccountApi.DbContext;

public partial class CreditAccountDbContext : DbContext
{
    public CreditAccountDbContext(DbContextOptions<CreditAccountDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountCode> AccountCodes { get; set; }

    public virtual DbSet<AccountGroup> AccountGroups { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BusinessType> BusinessTypes { get; set; }

    public virtual DbSet<CashGroup> CashGroups { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CreditInfo> CreditInfos { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CustGroupCountry> CustGroupCountries { get; set; }

    public virtual DbSet<CustGroupType> CustGroupTypes { get; set; }

    public virtual DbSet<CustPricProc> CustPricProcs { get; set; }

    public virtual DbSet<CustomerSign> CustomerSigns { get; set; }

    public virtual DbSet<DocumentCredit> DocumentCredits { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<ExchRateType> ExchRateTypes { get; set; }

    public virtual DbSet<General> Generals { get; set; }

    public virtual DbSet<Incoterm> Incoterms { get; set; }

    public virtual DbSet<IndustryType> IndustryTypes { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PriceList> PriceLists { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<RegisterFrom> RegisterFroms { get; set; }

    public virtual DbSet<SaleDistrict> SaleDistricts { get; set; }

    public virtual DbSet<SaleGroup> SaleGroups { get; set; }

    public virtual DbSet<SaleMaster> SaleMasters { get; set; }

    public virtual DbSet<SaleOrg> SaleOrgs { get; set; }

    public virtual DbSet<SalePerson> SalePeople { get; set; }

    public virtual DbSet<Shipping> Shippings { get; set; }

    public virtual DbSet<ShopType> ShopTypes { get; set; }

    public virtual DbSet<SortKey> SortKeys { get; set; }

    public virtual DbSet<TermOfPay> TermOfPays { get; set; }

    public virtual DbSet<ThaiAmphure> ThaiAmphures { get; set; }

    public virtual DbSet<ThaiGeography> ThaiGeographies { get; set; }

    public virtual DbSet<ThaiProvince> ThaiProvinces { get; set; }

    public virtual DbSet<ThaiTambon> ThaiTambons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_code_pkey");

            entity.ToTable("account_code");

            entity.HasIndex(e => e.AccCode, "account_code_acc_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccCode)
                .HasMaxLength(20)
                .HasColumnName("acc_code");
            entity.Property(e => e.AccDes).HasColumnName("acc_des");
            entity.Property(e => e.AccName)
                .HasMaxLength(200)
                .HasColumnName("acc_name");
        });

        modelBuilder.Entity<AccountGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_group_pkey");

            entity.ToTable("account_group");

            entity.HasIndex(e => e.AccGroupCode, "account_group_acc_group_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccGroupCode)
                .HasMaxLength(20)
                .HasColumnName("acc_group_code");
            entity.Property(e => e.AccGroupDes).HasColumnName("acc_group_des");
            entity.Property(e => e.AccGroupName)
                .HasMaxLength(200)
                .HasColumnName("acc_group_name");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("addresses_pkey");

            entity.ToTable("addresses");

            entity.HasIndex(e => e.GeneralId, "idx_addresses_general");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.AddrLine1)
                .HasMaxLength(255)
                .HasColumnName("addr_line1");
            entity.Property(e => e.AddrLine2)
                .HasMaxLength(255)
                .HasColumnName("addr_line2");
            entity.Property(e => e.AddrType)
                .HasMaxLength(50)
                .HasColumnName("addr_type");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.GeneralId).HasColumnName("general_id");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .HasColumnName("province");
            entity.Property(e => e.SubDistrict)
                .HasMaxLength(100)
                .HasColumnName("sub_district");

            entity.HasOne(d => d.General).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.GeneralId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("addresses_general_id_fkey");
        });

        modelBuilder.Entity<BusinessType>(entity =>
        {
            entity.HasKey(e => e.BusitypeId).HasName("business_type_pkey");

            entity.ToTable("business_type");

            entity.HasIndex(e => e.BusiTypeCode, "business_type_busi_type_code_key").IsUnique();

            entity.Property(e => e.BusitypeId).HasColumnName("busitype_id");
            entity.Property(e => e.BusiTypeCode)
                .HasMaxLength(20)
                .HasColumnName("busi_type_code");
            entity.Property(e => e.BusiTypeDes).HasColumnName("busi_type_des");
            entity.Property(e => e.BusiTypeName)
                .HasMaxLength(200)
                .HasColumnName("busi_type_name");
            entity.Property(e => e.RegisteredCapital)
                .HasPrecision(18, 2)
                .HasColumnName("registered_capital");
            entity.Property(e => e.RegistrationDate).HasColumnName("registration_date");
        });

        modelBuilder.Entity<CashGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cash_group_pkey");

            entity.ToTable("cash_group");

            entity.HasIndex(e => e.CashCode, "cash_group_cash_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CashCode)
                .HasMaxLength(20)
                .HasColumnName("cash_code");
            entity.Property(e => e.CashDes).HasColumnName("cash_des");
            entity.Property(e => e.CashName)
                .HasMaxLength(200)
                .HasColumnName("cash_name");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("company_pkey");

            entity.ToTable("company");

            entity.HasIndex(e => e.CompanyCode, "company_company_code_key").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyAddr).HasColumnName("company_addr");
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(20)
                .HasColumnName("company_code");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(200)
                .HasColumnName("company_name");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("country_pkey");

            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CapitalCity)
                .HasMaxLength(100)
                .HasColumnName("capital_city");
            entity.Property(e => e.CountryNameEn)
                .HasMaxLength(150)
                .HasColumnName("country_name_en");
            entity.Property(e => e.CountryNameTh)
                .HasMaxLength(150)
                .HasColumnName("country_name_th");
            entity.Property(e => e.IsoAlpha2)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("iso_alpha2");
            entity.Property(e => e.IsoAlpha3)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("iso_alpha3");
            entity.Property(e => e.OfficialName)
                .HasMaxLength(200)
                .HasColumnName("official_name");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.SubRegion)
                .HasMaxLength(100)
                .HasColumnName("sub_region");
        });

        modelBuilder.Entity<CreditInfo>(entity =>
        {
            entity.HasKey(e => e.CreditinfoId).HasName("credit_info_pkey");

            entity.ToTable("credit_info");

            entity.Property(e => e.CreditinfoId).HasColumnName("creditinfo_id");
            entity.Property(e => e.CreditLimit)
                .HasPrecision(18, 2)
                .HasColumnName("credit_limit");
            entity.Property(e => e.EstimatedPurchase)
                .HasPrecision(18, 2)
                .HasColumnName("estimated_purchase");
            entity.Property(e => e.TimeRequired)
                .HasMaxLength(50)
                .HasColumnName("time_required");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("currency_pkey");

            entity.ToTable("currency");

            entity.HasIndex(e => e.CurrencyCode, "currency_currency_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(10)
                .HasColumnName("currency_code");
            entity.Property(e => e.CurrencyDes).HasColumnName("currency_des");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(100)
                .HasColumnName("currency_name");
        });

        modelBuilder.Entity<CustGroupCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cust_group_country_pkey");

            entity.ToTable("cust_group_country");

            entity.HasIndex(e => e.CustgroCountryCode, "cust_group_country_custgro_country_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustgroCountryCode)
                .HasMaxLength(20)
                .HasColumnName("custgro_country_code");
            entity.Property(e => e.CustgroCountryDes).HasColumnName("custgro_country_des");
            entity.Property(e => e.CustgroCountryName)
                .HasMaxLength(200)
                .HasColumnName("custgro_country_name");
        });

        modelBuilder.Entity<CustGroupType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cust_group_type_pkey");

            entity.ToTable("cust_group_type");

            entity.HasIndex(e => e.CustgroTypeCode, "cust_group_type_custgro_type_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustgroTypeCode)
                .HasMaxLength(20)
                .HasColumnName("custgro_type_code");
            entity.Property(e => e.CustgroTypeDes).HasColumnName("custgro_type_des");
            entity.Property(e => e.CustgroTypeName)
                .HasMaxLength(200)
                .HasColumnName("custgro_type_name");
        });

        modelBuilder.Entity<CustPricProc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cust_pric_proc_pkey");

            entity.ToTable("cust_pric_proc");

            entity.HasIndex(e => e.CpProcCode, "cust_pric_proc_cp_proc_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CpProcCode)
                .HasMaxLength(20)
                .HasColumnName("cp_proc_code");
            entity.Property(e => e.CpProcDes).HasColumnName("cp_proc_des");
            entity.Property(e => e.CpProcName)
                .HasMaxLength(200)
                .HasColumnName("cp_proc_name");
        });

        modelBuilder.Entity<CustomerSign>(entity =>
        {
            entity.HasKey(e => e.CustsignId).HasName("customer_signs_pkey");

            entity.ToTable("customer_signs");

            entity.Property(e => e.CustsignId).HasColumnName("custsign_id");
            entity.Property(e => e.CustsignEmail)
                .HasMaxLength(200)
                .HasColumnName("custsign_email");
            entity.Property(e => e.CustsignFirstname)
                .HasMaxLength(100)
                .HasColumnName("custsign_firstname");
            entity.Property(e => e.CustsignLastname)
                .HasMaxLength(100)
                .HasColumnName("custsign_lastname");
            entity.Property(e => e.CustsignLine)
                .HasMaxLength(100)
                .HasColumnName("custsign_line");
            entity.Property(e => e.CustsignTel)
                .HasMaxLength(50)
                .HasColumnName("custsign_tel");
        });

        modelBuilder.Entity<DocumentCredit>(entity =>
        {
            entity.HasKey(e => e.DoccreditId).HasName("document_credit_pkey");

            entity.ToTable("document_credit");

            entity.Property(e => e.DoccreditId).HasColumnName("doccredit_id");
            entity.Property(e => e.CompanyCertificate)
                .HasDefaultValue(false)
                .HasColumnName("company_certificate");
            entity.Property(e => e.CompanyLocationMap)
                .HasDefaultValue(false)
                .HasColumnName("company_location_map");
            entity.Property(e => e.CopyOfCoRegis)
                .HasDefaultValue(false)
                .HasColumnName("copy_of_co_regis");
            entity.Property(e => e.CopyOfIdCard)
                .HasDefaultValue(false)
                .HasColumnName("copy_of_id_card");
            entity.Property(e => e.CopyOfPp20)
                .HasDefaultValue(false)
                .HasColumnName("copy_of_pp20");
            entity.Property(e => e.OtherSpecify).HasColumnName("other_specify");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.HasIndex(e => e.EmpCode, "employees_emp_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmpCode)
                .HasMaxLength(20)
                .HasColumnName("emp_code");
            entity.Property(e => e.EmpDepartment)
                .HasMaxLength(100)
                .HasColumnName("emp_department");
            entity.Property(e => e.EmpName)
                .HasMaxLength(200)
                .HasColumnName("emp_name");
        });

        modelBuilder.Entity<ExchRateType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("exch_rate_type_pkey");

            entity.ToTable("exch_rate_type");

            entity.HasIndex(e => e.ErTypeCode, "exch_rate_type_er_type_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ErTypeCode)
                .HasMaxLength(20)
                .HasColumnName("er_type_code");
            entity.Property(e => e.ErTypeDes).HasColumnName("er_type_des");
            entity.Property(e => e.ErTypeName)
                .HasMaxLength(200)
                .HasColumnName("er_type_name");
        });

        modelBuilder.Entity<General>(entity =>
        {
            entity.HasKey(e => e.GeneralId).HasName("generals_pkey");

            entity.ToTable("generals");

            entity.Property(e => e.GeneralId).HasColumnName("general_id");
            entity.Property(e => e.GeneralBranch)
                .HasMaxLength(100)
                .HasColumnName("general_branch");
            entity.Property(e => e.GeneralEmail)
                .HasMaxLength(200)
                .HasColumnName("general_email");
            entity.Property(e => e.GeneralFax)
                .HasMaxLength(50)
                .HasColumnName("general_fax");
            entity.Property(e => e.GeneralLine)
                .HasMaxLength(100)
                .HasColumnName("general_line");
            entity.Property(e => e.GeneralName1)
                .HasMaxLength(200)
                .HasColumnName("general_name1");
            entity.Property(e => e.GeneralName2)
                .HasMaxLength(200)
                .HasColumnName("general_name2");
            entity.Property(e => e.GeneralTax)
                .HasMaxLength(20)
                .HasColumnName("general_tax");
            entity.Property(e => e.GeneralTel)
                .HasMaxLength(50)
                .HasColumnName("general_tel");
        });

        modelBuilder.Entity<Incoterm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("incoterms_pkey");

            entity.ToTable("incoterms");

            entity.HasIndex(e => e.IncotermCode, "incoterms_incoterm_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IncotermCode)
                .HasMaxLength(20)
                .HasColumnName("incoterm_code");
            entity.Property(e => e.IncotermDes).HasColumnName("incoterm_des");
            entity.Property(e => e.IncotermName)
                .HasMaxLength(200)
                .HasColumnName("incoterm_name");
        });

        modelBuilder.Entity<IndustryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("industry_type_pkey");

            entity.ToTable("industry_type");

            entity.HasIndex(e => e.InduTypeCode, "industry_type_indu_type_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InduTypeCode)
                .HasMaxLength(20)
                .HasColumnName("indu_type_code");
            entity.Property(e => e.InduTypeDes).HasColumnName("indu_type_des");
            entity.Property(e => e.InduTypeName)
                .HasMaxLength(200)
                .HasColumnName("indu_type_name");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_method_pkey");

            entity.ToTable("payment_method");

            entity.HasIndex(e => e.PayCode, "payment_method_pay_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PayCode)
                .HasMaxLength(20)
                .HasColumnName("pay_code");
            entity.Property(e => e.PayDes).HasColumnName("pay_des");
            entity.Property(e => e.PayName)
                .HasMaxLength(200)
                .HasColumnName("pay_name");
        });

        modelBuilder.Entity<PriceList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("price_list_pkey");

            entity.ToTable("price_list");

            entity.HasIndex(e => e.PriceListCode, "price_list_price_list_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PriceListCode)
                .HasMaxLength(20)
                .HasColumnName("price_list_code");
            entity.Property(e => e.PriceListDes).HasColumnName("price_list_des");
            entity.Property(e => e.PriceListName)
                .HasMaxLength(200)
                .HasColumnName("price_list_name");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("province_pkey");

            entity.ToTable("province");

            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(20)
                .HasColumnName("province_code");
            entity.Property(e => e.ProvinceNameEn)
                .HasMaxLength(150)
                .HasColumnName("province_name_en");
            entity.Property(e => e.ProvinceNameTh)
                .HasMaxLength(150)
                .HasColumnName("province_name_th");

            entity.HasOne(d => d.Country).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("province_country_id_fkey");
        });

        modelBuilder.Entity<RegisterFrom>(entity =>
        {
            entity.HasKey(e => e.RegisterId).HasName("register_from_pkey");

            entity.ToTable("register_from");

            entity.HasIndex(e => e.GeneralId, "idx_register_general");

            entity.HasIndex(e => e.SaleDistrict, "idx_register_sale_dist");

            entity.HasIndex(e => e.SalePerson, "idx_register_sale_person");

            entity.Property(e => e.RegisterId).HasColumnName("register_id");
            entity.Property(e => e.AccountCode).HasColumnName("account_code");
            entity.Property(e => e.AccountGroup).HasColumnName("account_group");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.BusitypeId).HasColumnName("busitype_id");
            entity.Property(e => e.CashGroup).HasColumnName("cash_group");
            entity.Property(e => e.Company).HasColumnName("company");
            entity.Property(e => e.ContactPersonName)
                .HasMaxLength(200)
                .HasColumnName("contact_person_name");
            entity.Property(e => e.ContactPersonTel)
                .HasMaxLength(50)
                .HasColumnName("contact_person_tel");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreditinfoId).HasColumnName("creditinfo_id");
            entity.Property(e => e.Currency).HasColumnName("currency");
            entity.Property(e => e.CustGroupCountry).HasColumnName("cust_group_country");
            entity.Property(e => e.CustGroupType).HasColumnName("cust_group_type");
            entity.Property(e => e.CustPricProc).HasColumnName("cust_pric_proc");
            entity.Property(e => e.CustsignId).HasColumnName("custsign_id");
            entity.Property(e => e.DoccreditId).HasColumnName("doccredit_id");
            entity.Property(e => e.ExchRateType).HasColumnName("exch_rate_type");
            entity.Property(e => e.GeneralId).HasColumnName("general_id");
            entity.Property(e => e.Incoterms).HasColumnName("incoterms");
            entity.Property(e => e.IndustryType).HasColumnName("industry_type");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.PlType)
                .HasMaxLength(50)
                .HasColumnName("pl_type");
            entity.Property(e => e.PriceList).HasColumnName("price_list");
            entity.Property(e => e.SaleDistrict).HasColumnName("sale_district");
            entity.Property(e => e.SaleGroup).HasColumnName("sale_group");
            entity.Property(e => e.SaleManager).HasColumnName("sale_manager");
            entity.Property(e => e.SaleOrg).HasColumnName("sale_org");
            entity.Property(e => e.SalePerson).HasColumnName("sale_person");
            entity.Property(e => e.ShippingId).HasColumnName("shipping_id");
            entity.Property(e => e.ShopType).HasColumnName("shop_type");
            entity.Property(e => e.SortKey).HasColumnName("sort_key");
            entity.Property(e => e.TermOfPay).HasColumnName("term_of_pay");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AccountCodeNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.AccountCode)
                .HasConstraintName("register_from_account_code_fkey");

            entity.HasOne(d => d.AccountGroupNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.AccountGroup)
                .HasConstraintName("register_from_account_group_fkey");

            entity.HasOne(d => d.Address).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("register_from_address_id_fkey");

            entity.HasOne(d => d.Busitype).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.BusitypeId)
                .HasConstraintName("register_from_busitype_id_fkey");

            entity.HasOne(d => d.CashGroupNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.CashGroup)
                .HasConstraintName("register_from_cash_group_fkey");

            entity.HasOne(d => d.CompanyNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.Company)
                .HasConstraintName("register_from_company_fkey");

            entity.HasOne(d => d.Creditinfo).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.CreditinfoId)
                .HasConstraintName("register_from_creditinfo_id_fkey");

            entity.HasOne(d => d.CurrencyNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.Currency)
                .HasConstraintName("register_from_currency_fkey");

            entity.HasOne(d => d.CustGroupCountryNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.CustGroupCountry)
                .HasConstraintName("register_from_cust_group_country_fkey");

            entity.HasOne(d => d.CustGroupTypeNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.CustGroupType)
                .HasConstraintName("register_from_cust_group_type_fkey");

            entity.HasOne(d => d.CustPricProcNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.CustPricProc)
                .HasConstraintName("register_from_cust_pric_proc_fkey");

            entity.HasOne(d => d.Custsign).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.CustsignId)
                .HasConstraintName("register_from_custsign_id_fkey");

            entity.HasOne(d => d.Doccredit).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.DoccreditId)
                .HasConstraintName("register_from_doccredit_id_fkey");

            entity.HasOne(d => d.ExchRateTypeNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.ExchRateType)
                .HasConstraintName("register_from_exch_rate_type_fkey");

            entity.HasOne(d => d.General).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.GeneralId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("register_from_general_id_fkey");

            entity.HasOne(d => d.IncotermsNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.Incoterms)
                .HasConstraintName("register_from_incoterms_fkey");

            entity.HasOne(d => d.IndustryTypeNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.IndustryType)
                .HasConstraintName("register_from_industry_type_fkey");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.PaymentMethod)
                .HasConstraintName("register_from_payment_method_fkey");

            entity.HasOne(d => d.PriceListNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.PriceList)
                .HasConstraintName("register_from_price_list_fkey");

            entity.HasOne(d => d.SaleDistrictNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.SaleDistrict)
                .HasConstraintName("register_from_sale_district_fkey");

            entity.HasOne(d => d.SaleGroupNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.SaleGroup)
                .HasConstraintName("register_from_sale_group_fkey");

            entity.HasOne(d => d.SaleManagerNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.SaleManager)
                .HasConstraintName("register_from_sale_manager_fkey");

            entity.HasOne(d => d.SaleOrgNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.SaleOrg)
                .HasConstraintName("register_from_sale_org_fkey");

            entity.HasOne(d => d.SalePersonNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.SalePerson)
                .HasConstraintName("register_from_sale_person_fkey");

            entity.HasOne(d => d.Shipping).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.ShippingId)
                .HasConstraintName("register_from_shipping_id_fkey");

            entity.HasOne(d => d.ShopTypeNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.ShopType)
                .HasConstraintName("register_from_shop_type_fkey");

            entity.HasOne(d => d.SortKeyNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.SortKey)
                .HasConstraintName("register_from_sort_key_fkey");

            entity.HasOne(d => d.TermOfPayNavigation).WithMany(p => p.RegisterFroms)
                .HasForeignKey(d => d.TermOfPay)
                .HasConstraintName("register_from_term_of_pay_fkey");
        });

        modelBuilder.Entity<SaleDistrict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_district_pkey");

            entity.ToTable("sale_district");

            entity.HasIndex(e => e.SaledisCode, "sale_district_saledis_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SaledisCode)
                .HasMaxLength(20)
                .HasColumnName("saledis_code");
            entity.Property(e => e.SaledisDes).HasColumnName("saledis_des");
            entity.Property(e => e.SaledisName)
                .HasMaxLength(200)
                .HasColumnName("saledis_name");
        });

        modelBuilder.Entity<SaleGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_group_pkey");

            entity.ToTable("sale_group");

            entity.HasIndex(e => e.SaleGroCode, "sale_group_sale_gro_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SaleGroCode)
                .HasMaxLength(20)
                .HasColumnName("sale_gro_code");
            entity.Property(e => e.SaleGroDes).HasColumnName("sale_gro_des");
            entity.Property(e => e.SaleGroName)
                .HasMaxLength(200)
                .HasColumnName("sale_gro_name");
        });

        modelBuilder.Entity<SaleMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_master_pkey");

            entity.ToTable("sale_master");

            entity.HasIndex(e => e.SaleGroupCode, "sale_master_sale_group_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SaleGroupCode)
                .HasMaxLength(20)
                .HasColumnName("sale_group_code");
            entity.Property(e => e.SaleGroupDes).HasColumnName("sale_group_des");
            entity.Property(e => e.SaleGroupName)
                .HasMaxLength(200)
                .HasColumnName("sale_group_name");
        });

        modelBuilder.Entity<SaleOrg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_org_pkey");

            entity.ToTable("sale_org");

            entity.HasIndex(e => e.SaleOrgCode, "sale_org_sale_org_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SaleOrgCode)
                .HasMaxLength(20)
                .HasColumnName("sale_org_code");
            entity.Property(e => e.SaleOrgDes).HasColumnName("sale_org_des");
            entity.Property(e => e.SaleOrgName)
                .HasMaxLength(200)
                .HasColumnName("sale_org_name");
        });

        modelBuilder.Entity<SalePerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_person_pkey");

            entity.ToTable("sale_person");

            entity.HasIndex(e => e.SalePersonCode, "sale_person_sale_person_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SalePersonCode)
                .HasMaxLength(20)
                .HasColumnName("sale_person_code");
            entity.Property(e => e.SalePersonDes).HasColumnName("sale_person_des");
            entity.Property(e => e.SalePersonName)
                .HasMaxLength(200)
                .HasColumnName("sale_person_name");
        });

        modelBuilder.Entity<Shipping>(entity =>
        {
            entity.HasKey(e => e.ShippingId).HasName("shipping_pkey");

            entity.ToTable("shipping");

            entity.Property(e => e.ShippingId).HasColumnName("shipping_id");
            entity.Property(e => e.AddrLine1)
                .HasMaxLength(255)
                .HasColumnName("addr_line1");
            entity.Property(e => e.AddrLine2)
                .HasMaxLength(255)
                .HasColumnName("addr_line2");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.GeneralId).HasColumnName("general_id");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .HasColumnName("province");
            entity.Property(e => e.SubDistrict)
                .HasMaxLength(100)
                .HasColumnName("sub_district");

            entity.HasOne(d => d.General).WithMany(p => p.Shippings)
                .HasForeignKey(d => d.GeneralId)
                .HasConstraintName("shipping_general_id_fkey");
        });

        modelBuilder.Entity<ShopType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shop_type_pkey");

            entity.ToTable("shop_type");

            entity.HasIndex(e => e.ShopCode, "shop_type_shop_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShopCode)
                .HasMaxLength(20)
                .HasColumnName("shop_code");
            entity.Property(e => e.ShopDes).HasColumnName("shop_des");
            entity.Property(e => e.ShopName)
                .HasMaxLength(200)
                .HasColumnName("shop_name");
        });

        modelBuilder.Entity<SortKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sort_key_pkey");

            entity.ToTable("sort_key");

            entity.HasIndex(e => e.SortkeyCode, "sort_key_sortkey_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SortkeyCode)
                .HasMaxLength(20)
                .HasColumnName("sortkey_code");
            entity.Property(e => e.SortkeyDes).HasColumnName("sortkey_des");
            entity.Property(e => e.SortkeyName)
                .HasMaxLength(200)
                .HasColumnName("sortkey_name");
        });

        modelBuilder.Entity<TermOfPay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("term_of_pay_pkey");

            entity.ToTable("term_of_pay");

            entity.HasIndex(e => e.TopCode, "term_of_pay_top_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TopCode)
                .HasMaxLength(20)
                .HasColumnName("top_code");
            entity.Property(e => e.TopDes).HasColumnName("top_des");
            entity.Property(e => e.TopName)
                .HasMaxLength(200)
                .HasColumnName("top_name");
        });

        modelBuilder.Entity<ThaiAmphure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("thai_amphures_pkey");

            entity.ToTable("thai_amphures");

            entity.HasIndex(e => e.ProvinceId, "idx_thai_amphures_prov");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(150)
                .HasColumnName("name_th");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Province).WithMany(p => p.ThaiAmphures)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("thai_amphures_province_id_fkey");
        });

        modelBuilder.Entity<ThaiGeography>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("thai_geographies_pkey");

            entity.ToTable("thai_geographies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ThaiProvince>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("thai_provinces_pkey");

            entity.ToTable("thai_provinces");

            entity.HasIndex(e => e.GeographyId, "idx_thai_provinces_geo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.GeographyId).HasColumnName("geography_id");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(150)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Geography).WithMany(p => p.ThaiProvinces)
                .HasForeignKey(d => d.GeographyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("thai_provinces_geography_id_fkey");
        });

        modelBuilder.Entity<ThaiTambon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("thai_tambons_pkey");

            entity.ToTable("thai_tambons");

            entity.HasIndex(e => e.AmphureId, "idx_thai_tambons_amph");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AmphureId).HasColumnName("amphure_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameTh)
                .HasMaxLength(150)
                .HasColumnName("name_th");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("zip_code");

            entity.HasOne(d => d.Amphure).WithMany(p => p.ThaiTambons)
                .HasForeignKey(d => d.AmphureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("thai_tambons_amphure_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
