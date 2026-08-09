-- ============================================================
-- Credit Account MS — PostgreSQL Schema
-- Generated from: Credit_Account_MS_ER-Diagram.drawio
-- Database: PostgreSQL
-- ORM: Entity Framework Core (Npgsql)
-- ============================================================

-- ============================================================
-- 1. MASTER DATA — Thai Address
-- ============================================================

CREATE TABLE thai_geographies (
    id          SERIAL PRIMARY KEY,
    name        VARCHAR(100) NOT NULL
);

CREATE TABLE thai_provinces (
    id              SERIAL PRIMARY KEY,
    name_th         VARCHAR(150) NOT NULL,
    name_en         VARCHAR(150),
    geography_id    INT NOT NULL REFERENCES thai_geographies(id),
    created_at      TIMESTAMP DEFAULT NOW(),
    updated_at      TIMESTAMP DEFAULT NOW()
);

CREATE TABLE thai_amphures (
    id              SERIAL PRIMARY KEY,
    name_th         VARCHAR(150) NOT NULL,
    name_en         VARCHAR(150),
    province_id     INT NOT NULL REFERENCES thai_provinces(id),
    created_at      TIMESTAMP DEFAULT NOW(),
    updated_at      TIMESTAMP DEFAULT NOW()
);

CREATE TABLE thai_tambons (
    id              SERIAL PRIMARY KEY,
    name_th         VARCHAR(150) NOT NULL,
    name_en         VARCHAR(150),
    zip_code        VARCHAR(10),
    amphure_id      INT NOT NULL REFERENCES thai_amphures(id),
    created_at      TIMESTAMP DEFAULT NOW(),
    updated_at      TIMESTAMP DEFAULT NOW()
);

-- ============================================================
-- 2. MASTER DATA — Country / Province (International)
-- ============================================================

CREATE TABLE country (
    country_id      SERIAL PRIMARY KEY,
    country_name_th VARCHAR(150),
    country_name_en VARCHAR(150),
    iso_alpha2      CHAR(2),
    iso_alpha3      CHAR(3),
    official_name   VARCHAR(200),
    region          VARCHAR(100),
    sub_region      VARCHAR(100),
    capital_city    VARCHAR(100)
);

CREATE TABLE province (
    province_id     SERIAL PRIMARY KEY,
    country_id      INT NOT NULL REFERENCES country(country_id),
    province_name_th VARCHAR(150),
    province_name_en VARCHAR(150),
    province_code   VARCHAR(20)
);

-- ============================================================
-- 3. MASTER DATA — Sales Organization
-- ============================================================

CREATE TABLE sale_org (
    id              SERIAL PRIMARY KEY,
    sale_org_code   VARCHAR(20) NOT NULL UNIQUE,
    sale_org_name   VARCHAR(200) NOT NULL,
    sale_org_des    TEXT
);

CREATE TABLE sale_group (
    id              SERIAL PRIMARY KEY,
    sale_gro_code   VARCHAR(20) NOT NULL UNIQUE,
    sale_gro_name   VARCHAR(200) NOT NULL,
    sale_gro_des    TEXT
);

CREATE TABLE sale_district (
    id              SERIAL PRIMARY KEY,
    saledis_code    VARCHAR(20) NOT NULL UNIQUE,
    saledis_name    VARCHAR(200) NOT NULL,
    saledis_des     TEXT
);

CREATE TABLE sale_master (
    id              SERIAL PRIMARY KEY,
    sale_group_code VARCHAR(20) NOT NULL UNIQUE,
    sale_group_name VARCHAR(200) NOT NULL,
    sale_group_des  TEXT
);

CREATE TABLE sale_person (
    id              SERIAL PRIMARY KEY,
    sale_person_code VARCHAR(20) NOT NULL UNIQUE,
    sale_person_name VARCHAR(200) NOT NULL,
    sale_person_des  TEXT
);

-- ============================================================
-- 4. MASTER DATA — Account & Finance
-- ============================================================

CREATE TABLE account_group (
    id              SERIAL PRIMARY KEY,
    acc_group_code  VARCHAR(20) NOT NULL UNIQUE,
    acc_group_name  VARCHAR(200) NOT NULL,
    acc_group_des   TEXT
);

CREATE TABLE account_code (
    id              SERIAL PRIMARY KEY,
    acc_code        VARCHAR(20) NOT NULL UNIQUE,
    acc_name        VARCHAR(200) NOT NULL,
    acc_des         TEXT
);

CREATE TABLE cash_group (
    id              SERIAL PRIMARY KEY,
    cash_code       VARCHAR(20) NOT NULL UNIQUE,
    cash_name       VARCHAR(200) NOT NULL,
    cash_des        TEXT
);

CREATE TABLE currency (
    id              SERIAL PRIMARY KEY,
    currency_code   VARCHAR(10) NOT NULL UNIQUE,
    currency_name   VARCHAR(100) NOT NULL,
    currency_des    TEXT
);

CREATE TABLE exch_rate_type (
    id              SERIAL PRIMARY KEY,
    er_type_code    VARCHAR(20) NOT NULL UNIQUE,
    er_type_name    VARCHAR(200) NOT NULL,
    er_type_des     TEXT
);

CREATE TABLE term_of_pay (
    id              SERIAL PRIMARY KEY,
    top_code        VARCHAR(20) NOT NULL UNIQUE,
    top_name        VARCHAR(200) NOT NULL,
    top_des         TEXT
);

CREATE TABLE payment_method (
    id              SERIAL PRIMARY KEY,
    pay_code        VARCHAR(20) NOT NULL UNIQUE,
    pay_name        VARCHAR(200) NOT NULL,
    pay_des         TEXT
);

CREATE TABLE incoterms (
    id              SERIAL PRIMARY KEY,
    incoterm_code   VARCHAR(20) NOT NULL UNIQUE,
    incoterm_name   VARCHAR(200) NOT NULL,
    incoterm_des    TEXT
);

CREATE TABLE price_list (
    id              SERIAL PRIMARY KEY,
    price_list_code VARCHAR(20) NOT NULL UNIQUE,
    price_list_name VARCHAR(200) NOT NULL,
    price_list_des  TEXT
);

CREATE TABLE cust_pric_proc (
    id              SERIAL PRIMARY KEY,
    cp_proc_code    VARCHAR(20) NOT NULL UNIQUE,
    cp_proc_name    VARCHAR(200) NOT NULL,
    cp_proc_des     TEXT
);

CREATE TABLE sort_key (
    id              SERIAL PRIMARY KEY,
    sortkey_code    VARCHAR(20) NOT NULL UNIQUE,
    sortkey_name    VARCHAR(200) NOT NULL,
    sortkey_des     TEXT
);

-- ============================================================
-- 5. MASTER DATA — Customer Classification
-- ============================================================

CREATE TABLE business_type (
    busitype_id         SERIAL PRIMARY KEY,
    busi_type_code      VARCHAR(20) NOT NULL UNIQUE,
    busi_type_name      VARCHAR(200) NOT NULL,
    busi_type_des       TEXT,
    registration_date   DATE,
    registered_capital  NUMERIC(18,2)
);

CREATE TABLE industry_type (
    id              SERIAL PRIMARY KEY,
    indu_type_code  VARCHAR(20) NOT NULL UNIQUE,
    indu_type_name  VARCHAR(200) NOT NULL,
    indu_type_des   TEXT
);

CREATE TABLE shop_type (
    id              SERIAL PRIMARY KEY,
    shop_code       VARCHAR(20) NOT NULL UNIQUE,
    shop_name       VARCHAR(200) NOT NULL,
    shop_des        TEXT
);

CREATE TABLE cust_group_type (
    id                  SERIAL PRIMARY KEY,
    custgro_type_code   VARCHAR(20) NOT NULL UNIQUE,
    custgro_type_name   VARCHAR(200) NOT NULL,
    custgro_type_des    TEXT
);

CREATE TABLE cust_group_country (
    id                      SERIAL PRIMARY KEY,
    custgro_country_code    VARCHAR(20) NOT NULL UNIQUE,
    custgro_country_name    VARCHAR(200) NOT NULL,
    custgro_country_des     TEXT
);

-- ============================================================
-- 6. MASTER DATA — Company & Employees
-- ============================================================

CREATE TABLE company (
    company_id      SERIAL PRIMARY KEY,
    company_code    VARCHAR(20) NOT NULL UNIQUE,
    company_name    VARCHAR(200) NOT NULL,
    company_addr    TEXT
);

CREATE TABLE employees (
    id              SERIAL PRIMARY KEY,
    emp_code        VARCHAR(20) NOT NULL UNIQUE,
    emp_name        VARCHAR(200) NOT NULL,
    emp_department  VARCHAR(100)
);

-- ============================================================
-- 7. TRANSACTION — Customer Core
-- ============================================================

CREATE TABLE generals (
    general_id      SERIAL PRIMARY KEY,
    general_name1   VARCHAR(200) NOT NULL,   -- ชื่อบริษัท (ภาษาไทย)
    general_name2   VARCHAR(200),            -- ชื่อบริษัท (ภาษาอังกฤษ)
    general_tel     VARCHAR(50),
    general_fax     VARCHAR(50),
    general_email   VARCHAR(200),
    general_line    VARCHAR(100),
    general_tax     VARCHAR(20),             -- เลขภาษี
    general_branch  VARCHAR(100)             -- สาขา
);

CREATE TABLE addresses (
    address_id      SERIAL PRIMARY KEY,
    general_id      INT NOT NULL REFERENCES generals(general_id),
    addr_type       VARCHAR(50),             -- เช่น 'billing', 'shipping'
    addr_line1      VARCHAR(255),
    addr_line2      VARCHAR(255),
    sub_district    VARCHAR(100),
    district        VARCHAR(100),
    province        VARCHAR(100),
    postal_code     VARCHAR(10),
    country         VARCHAR(100),
    created_date    TIMESTAMP DEFAULT NOW()
);

CREATE TABLE shipping (
    shipping_id     SERIAL PRIMARY KEY,
    general_id      INT REFERENCES generals(general_id),
    addr_line1      VARCHAR(255),
    addr_line2      VARCHAR(255),
    sub_district    VARCHAR(100),
    district        VARCHAR(100),
    province        VARCHAR(100),
    postal_code     VARCHAR(10),
    country         VARCHAR(100)
);

CREATE TABLE credit_info (
    creditinfo_id       SERIAL PRIMARY KEY,
    estimated_purchase  NUMERIC(18,2),       -- ยอดซื้อประมาณการ/ปี
    time_required       VARCHAR(50),         -- เครดิตเทอมที่ขอ เช่น '30', '60'
    credit_limit        NUMERIC(18,2)        -- วงเงินที่ขอ
);

CREATE TABLE document_credit (
    doccredit_id            SERIAL PRIMARY KEY,
    company_certificate     BOOLEAN DEFAULT FALSE,   -- หนังสือรับรองบริษัท
    copy_of_pp20            BOOLEAN DEFAULT FALSE,   -- สำเนา บอจ.5
    copy_of_co_regis        BOOLEAN DEFAULT FALSE,   -- สำเนาใบทะเบียนการค้า
    copy_of_id_card         BOOLEAN DEFAULT FALSE,   -- สำเนาบัตรประชาชน
    company_location_map    BOOLEAN DEFAULT FALSE,   -- แผนที่ตั้งบริษัท
    other_specify           TEXT                     -- เอกสารอื่นๆ
);

CREATE TABLE customer_signs (
    custsign_id         SERIAL PRIMARY KEY,
    custsign_firstname  VARCHAR(100) NOT NULL,
    custsign_lastname   VARCHAR(100) NOT NULL,
    custsign_tel        VARCHAR(50),
    custsign_email      VARCHAR(200),
    custsign_line       VARCHAR(100)
);

-- ============================================================
-- 8. TRANSACTION — Register Form (ใบสมัครหลัก)
-- ============================================================

CREATE TABLE register_from (
    register_id         SERIAL PRIMARY KEY,

    -- Customer Info
    general_id          INT NOT NULL REFERENCES generals(general_id),
    address_id          INT REFERENCES addresses(address_id),
    shipping_id         INT REFERENCES shipping(shipping_id),
    busitype_id         INT REFERENCES business_type(busitype_id),
    creditinfo_id       INT REFERENCES credit_info(creditinfo_id),
    doccredit_id        INT REFERENCES document_credit(doccredit_id),
    custsign_id         INT REFERENCES customer_signs(custsign_id),

    -- Classification
    shop_type           INT REFERENCES shop_type(id),
    industry_type       INT REFERENCES industry_type(id),
    cust_group_type     INT REFERENCES cust_group_type(id),
    cust_group_country  INT REFERENCES cust_group_country(id),

    -- Company & Account
    company             INT REFERENCES company(company_id),
    account_group       INT REFERENCES account_group(id),
    account_code        INT REFERENCES account_code(id),
    cash_group          INT REFERENCES cash_group(id),
    sort_key            INT REFERENCES sort_key(id),

    -- Sales Organization
    sale_org            INT REFERENCES sale_org(id),
    sale_group          INT REFERENCES sale_group(id),
    sale_district       INT REFERENCES sale_district(id),
    sale_person         INT REFERENCES sale_person(id),
    sale_manager        INT REFERENCES sale_master(id),

    -- Finance & Terms
    payment_method      INT REFERENCES payment_method(id),
    term_of_pay         INT REFERENCES term_of_pay(id),
    currency            INT REFERENCES currency(id),
    exch_rate_type      INT REFERENCES exch_rate_type(id),
    incoterms            INT REFERENCES incoterms(id),
    price_list          INT REFERENCES price_list(id),
    cust_pric_proc      INT REFERENCES cust_pric_proc(id),

    -- Contact Person
    contact_person_name VARCHAR(200),
    contact_person_tel  VARCHAR(50),

    -- Metadata
    pl_type             VARCHAR(50),
    created_at          TIMESTAMP DEFAULT NOW(),
    updated_at          TIMESTAMP DEFAULT NOW()
);

-- ============================================================
-- INDEX — สำหรับ Query ที่ใช้บ่อย
-- ============================================================

CREATE INDEX idx_register_general    ON register_from(general_id);
CREATE INDEX idx_register_sale_dist  ON register_from(sale_district);
CREATE INDEX idx_register_sale_person ON register_from(sale_person);
CREATE INDEX idx_addresses_general   ON addresses(general_id);
CREATE INDEX idx_thai_provinces_geo  ON thai_provinces(geography_id);
CREATE INDEX idx_thai_amphures_prov  ON thai_amphures(province_id);
CREATE INDEX idx_thai_tambons_amph   ON thai_tambons(amphure_id);
