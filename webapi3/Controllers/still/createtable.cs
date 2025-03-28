using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace CreateTable.Controllers
{
    [ApiController]
    [Route("api/createtable")]
    public class TestController : ControllerBase
    {
        private readonly string connectionString = "Data Source=test.db";

        // POST: api/table/createTable
        [HttpPost("createTable")]
        public IActionResult CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
                PRAGMA foreign_keys = ON;

                CREATE TABLE IF NOT EXISTS Company (
                    company_id INTEGER PRIMARY KEY,
                    companyCode TEXT,
                    companyName TEXT,
                    companyAddr TEXT
                );
/* โครงสร้าง ประเทศตำบล */
                CREATE TABLE IF NOT EXISTS Country (
                    country_id INTEGER PRIMARY KEY,
                    country_nameTh TEXT,
                    country_nameEn TEXT,
                    iso_alpha2 TEXT,
                    iso_alpha3 TEXT,
                    official_name TEXT,
                    region TEXT,
                    sub_region TEXT,
                    capital_city TEXT
                );

                CREATE TABLE IF NOT EXISTS Province (
                    province_id INTEGER PRIMARY KEY,
                    country_id INTEGER,
                    province_nameTh TEXT,
                    province_nameEn TEXT,
                    province_code TEXT,
                    FOREIGN KEY (country_id) REFERENCES Country(country_id)
                );



-- โครงสร้าง เกี่ยวข้องกับภูมิศาสตร์

-- สร้างตาราง thai_geographies
CREATE TABLE IF NOT EXISTS thai_geographies (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL
);

-- สร้างตาราง thai_provinces
CREATE TABLE IF NOT EXISTS thai_provinces (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name_th TEXT NOT NULL,
    name_en TEXT NOT NULL,
    geography_id INTEGER,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (geography_id) REFERENCES thai_geographies(id)
);

-- สร้างตาราง thai_amphures
CREATE TABLE IF NOT EXISTS thai_amphures (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name_th TEXT NOT NULL,
    name_en TEXT NOT NULL,
    province_id INTEGER,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (province_id) REFERENCES thai_provinces(id)
);

-- สร้างตาราง thai_tambons
CREATE TABLE IF NOT EXISTS thai_tambons (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    zip_code TEXT NOT NULL,
    name_th TEXT NOT NULL,
    name_en TEXT NOT NULL,
    amphure_id INTEGER,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (amphure_id) REFERENCES thai_amphures(id)
);

---FK1.ตารางที่เก็บข้อมูลทั่วไปของผู้ใช้หรือบุคคล เช่น ชื่อ, นามสกุล, เพศ, 
CREATE TABLE IF NOT EXISTS Generals (
    general_id INTEGER PRIMARY KEY AUTOINCREMENT,
    generalName1 VARCHAR(255),
    generalTel VARCHAR(255),
    generalFax VARCHAR(255),
    generalEmail VARCHAR(255),
    generalLine VARCHAR(255),
    generalTax VARCHAR(255),
    generalBranch VARCHAR(255)
);

CREATE TABLE IF NOT EXISTS Addresses (
    address_id INTEGER PRIMARY KEY AUTOINCREMENT,
    general_id INT,
    addrType VARCHAR(255),
    addrLine1 VARCHAR(255),
    addrLine2 VARCHAR(255),
    subDistrict VARCHAR(255),
    district VARCHAR(255),
    province VARCHAR(255),
    postalCode VARCHAR(255),
    country VARCHAR(255),
    FOREIGN KEY (general_id) REFERENCES Generals(general_id)
);

--รหัสการจัดส่ง
CREATE TABLE IF NOT EXISTS Shipping (
    shipping_id INTEGER PRIMARY KEY AUTOINCREMENT,  -- เพิ่ม AUTOINCREMENT สำหรับ shipping_id
    DeliveryName NVARCHAR(250),
    address1 NVARCHAR(250),
    district NVARCHAR(250),
    province NVARCHAR(250),
    postalCode NVARCHAR(250)
);
--ตารางที่ใช้เก็บข้อมูลประเภทของร้านค้า
CREATE TABLE IF NOT EXISTS ShopType (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    shopCode VARCHAR(255),
    shopName VARCHAR(255),
    shopDes TEXT,
    accGroupName VARCHAR(255)
);
--ประเภทอุตสาหกรรม 
CREATE TABLE IF NOT EXISTS IndustryType (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    InduTypeCode VARCHAR(255),
    InduTypeName VARCHAR(255),
    InduTypeDes TEXT
);
--บริษัท
CREATE TABLE IF NOT EXISTS Company (
    company_id INTEGER PRIMARY KEY AUTOINCREMENT,
    companyCode VARCHAR(255),
    companyName VARCHAR(255),
    companyAddr TEXT
);
----
---องค์กรการขาย
CREATE TABLE SaleOrg (
    id INT NOT NULL PRIMARY KEY,
    saleOrgCode VARCHAR(255),
    saleOrgName VARCHAR(255),
    saleOrgDes TEXT
);

--กลุ่มบัญชี
CREATE TABLE AccountGroup (
    id INT NOT NULL,                -- Primary Key
    accGroupCode VARCHAR(255),      -- Group code
    accGroupName VARCHAR(255),      -- Group name
    accGroupDes TEXT,               -- Group description
    PRIMARY KEY (id)                -- Define the primary key
);


--ประเภทธุรกิจ
CREATE TABLE BusinessType (
    busiTypeID INT NOT NULL PRIMARY KEY,
    busiTypeCode VARCHAR(255),
    busiTypeName VARCHAR(255),
    busiTypeDes TEXT,
    registrationDate DATE,
    registeredCapital DECIMAL(15, 2)
);

--ซึ่งใช้เพื่อแสดงข้อมูลต่าง ๆ ในแผนภาพหรือกราฟของ mxGraphModel โดย mxCell จะเป็นตัวแทนของเซลล์
CREATE TABLE mxCells (
  id INT PRIMARY KEY,
  value TEXT,
  style TEXT,
  vertex BOOLEAN,
  parent INT,
  FOREIGN KEY (parent) REFERENCES mxCells(id) -- assuming parent links to another cell
);

--ตราสารเครดิต
CREATE TABLE DocumentCredit (
    doccredit_id INT NOT NULL,
    CompanyCertificate BIT,
    CopyOfPP_20 BIT,
    CopyOfCoRegis BIT,
    CopyOfIDCard BIT,
    CompanyLocationMap BIT,
    OtherSpecify TEXT
);
-- สร้างตารางสำหรับเก็บข้อมูลลูกค้า (CustomerSigns)
CREATE TABLE CustomerSigns (
    custsign_id INT NOT NULL PRIMARY KEY,
    custsignFirstName VARCHAR(255) NOT NULL,
    custsignLastName VARCHAR(255) NOT NULL,
    custsignTel VARCHAR(255) NOT NULL,
    custsignEmail VARCHAR(255) NOT NULL,
    custsignLine VARCHAR(255) NOT NULL
);

--AccountCode
CREATE TABLE AccountCode (   id INT NOT NULL,   accCode VARCHAR(255),   accName VARCHAR(255),   accDes VARCHAR(255),   PRIMARY KEY (id) );`

--SortKey
CREATE TABLE SortKey (
    id INT NOT NULL PRIMARY KEY,
    sortkeyCode VARCHAR(255),
    sortkeyName VARCHAR(255),
    sortkeyDes VARCHAR(255)
);

--CashGroup
CREATE TABLE CashGroup (   id int NOT NULL,   cashCode VARCHAR(255),   cashName VARCHAR(255),   PRIMARY KEY (id) );















--PaymentMethod
CREATE TABLE PaymentMethod (
    id INT NOT NULL,
    payCode VARCHAR(255),
    payName VARCHAR(255),
    payDes VARCHAR(255),
    PRIMARY KEY (id)
);

--TermOfPay
CREATE TABLE TermOfPay (
    id INT NOT NULL,
    topCode VARCHAR(255),
    topName VARCHAR(255),
    topDes TEXT,
    PRIMARY KEY (id)
);

--SaleDistrict
CREATE TABLE SaleDistrict (
    id INT NOT NULL PRIMARY KEY,
    saledisCode VARCHAR(255),
    saledisName VARCHAR(255),
    saledisDes VARCHAR(255)
);

--SaleGroup
CREATE TABLE SaleGroup (
    id INT NOT NULL,
    saleGroCode VARCHAR(255),
    saleGroName VARCHAR(255),
    PRIMARY KEY (id)
);

---CustGroupType
CREATE TABLE CustGroupType (
    id INT NOT NULL PRIMARY KEY,
    custgroTypeCode VARCHAR(255),
    custgroTypeName VARCHAR(255),
    custgroTypeDes VARCHAR(255)
);





--Currency
CREATE TABLE Currency (
    id INT NOT NULL PRIMARY KEY,
    currencyCode VARCHAR(255),
    currencyName VARCHAR(255),
    currencyDes VARCHAR(255)
);

--ExchRateType
CREATE TABLE ExchRateType (
    id INT NOT NULL PRIMARY KEY,
    erTypeCode VARCHAR(255),
    erTypeName VARCHAR(255),
    erTypeDes VARCHAR(255)
);

--CustPricProc
CREATE TABLE CustPricProc (
    id INT NOT NULL PRIMARY KEY,
    cpProcCode VARCHAR(255),
    cpProcName VARCHAR(255),
    cpProcDes TEXT
);

--PriceList
CREATE TABLE PriceList (
    id INT NOT NULL PRIMARY KEY,
    priceListCode VARCHAR(255),
    priceListName VARCHAR(255),
    priceListDes VARCHAR(255)
);

--Incoterms
CREATE TABLE Incoterms (
    id INT NOT NULL PRIMARY KEY,
    incotermCode VARCHAR(50),
    incotermName VARCHAR(100),
    incotermDes TEXT
);

--SalePerson
CREATE TABLE SalePerson (
  id INT NOT NULL PRIMARY KEY,
  incotermCode VARCHAR(255),
  incotermName VARCHAR(255),
  incotermDes VARCHAR(255)
);

--SaleMaster
CREATE TABLE SaleMaster (
    id INT NOT NULL PRIMARY KEY,
    saleGroupCode VARCHAR(255),
    saleGroupName VARCHAR(255),
    saleGroupDes VARCHAR(255)
);

--CustGroupContry
CREATE TABLE CustGroupContry (
    id INT NOT NULL PRIMARY KEY,
    custgroContryCode VARCHAR(255),
    custgroContryName VARCHAR(255),
    custgroContryDes VARCHAR(255)
);



--- โครงสร้าง RegisterFrom

             CREATE TABLE IF NOT EXISTS RegisterFrom (
    register_id INTEGER PRIMARY KEY AUTOINCREMENT,
    general_id INTEGER,
    shipping_id INTEGER,  
    shopCode VARCHAR(255),  -- เพิ่มคอลัมน์นี้เพื่อเชื่อมกับ ShopType
    company_id INTEGER,     -- เพิ่มคอลัมน์ company_id ที่นี่
    industry_id INTEGER,    -- เพิ่มคอลัมน์ industry_id ที่นี่
    FOREIGN KEY (general_id) REFERENCES Generals(general_id),
    FOREIGN KEY (shipping_id) REFERENCES Shipping(shipping_id),
    FOREIGN KEY (company_id) REFERENCES Company(company_id),  -- การเชื่อมโยงกับ Company
    FOREIGN KEY (industry_id) REFERENCES IndustryType(id)    -- การเชื่อมโยงกับ IndustryType
);


                ";

                command.ExecuteNonQuery();
            }

            return Ok("Tables created successfully.");
        }
    }
}