using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using RegisteersTable.Models; // ใช้ RegisterModel ที่ประกาศไว้

namespace GetRegisteersTable.Registeers
{
    [ApiController]
    [Route("api/getregister")]
    public class GetRegisterController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=test.db"; // เปลี่ยนเป็นพาธของไฟล์ฐานข้อมูลของคุณ

        // GET: api/getregister
        [HttpGet]
        public IActionResult GetRegisters()
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    // คำสั่ง SQL สำหรับดึงข้อมูลจากตาราง Generals
                    var query = @"
                       SELECT 
    g.general_id, 
    g.generalName1, 
    g.generalTel, 
    g.generalFax, 
    g.generalEmail, 
    g.generalLine, 
    g.generalTax, 
    g.generalBranch, 
    a.addrType, 
    a.addrLine1, 
    a.addrLine2, 
    a.subDistrict, 
    a.district, 
    a.province, 
    a.postalCode, 
    a.country,
    s.DeliveryName,
    st.shopCode,
    it.InduTypeCode,    -- เพิ่มข้อมูลจาก IndustryType
    it.InduTypeName,    -- เพิ่มข้อมูลจาก IndustryType
    c.companyCode,      -- เพิ่มข้อมูลจาก Company
    c.companyName,      -- เพิ่มข้อมูลจาก Company
    c.companyAddr       -- เพิ่มข้อมูลจาก Company
FROM Generals g
LEFT JOIN Addresses a ON g.general_id = a.general_id
LEFT JOIN Shipping s ON s.shipping_id = s.shipping_id
LEFT JOIN ShopType st ON st.shopCode = st.shopCode
LEFT JOIN Company c ON c.company_id = c.company_id  -- เชื่อม Company กับ Generals
LEFT JOIN IndustryType it ON it.id = it.id  -- เชื่อม IndustryType กับ Company
                    ";


                    var command = new SqliteCommand(query, connection);
                    var registers = new List<RegisterModel>();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var register = new RegisterModel
                            {
                                General = new GeneralModel
                                {
                                    GeneralName1 = reader.GetString(1),
                                    GeneralTel = reader.GetString(2),
                                    GeneralFax = reader.GetString(3),
                                    GeneralEmail = reader.GetString(4),
                                    GeneralLine = reader.GetString(5),
                                    GeneralTax = reader.GetString(6),
                                    GeneralBranch = reader.GetString(7)
                                },
                                Address = new AddressModel
                                {
                                    AddrType = reader.GetString(8),
                                    AddrLine1 = reader.GetString(9),
                                    AddrLine2 = reader.GetString(10),
                                    SubDistrict = reader.GetString(11),
                                    District = reader.GetString(12),
                                    Province = reader.GetString(13),
                                    PostalCode = reader.GetString(14),
                                    Country = reader.GetString(15)
                                },
                                Shipping = new ShippingModel
                                {
                                    DeliveryName = reader.GetString(16),

                                },
                                ShopType = new ShopTypeModel
                                {
                                    shopCode = reader.GetString(17),

                                },
                                IndustryType = new IndustryTypeModel
                                {
                                    InduTypeCode = reader.GetString(18),
                                    InduTypeName = reader.GetString(19)
                                },
                                Company = new CompanyModel
                                {
                                    companyCode = reader.GetString(20),
                                    companyName = reader.GetString(21),
                                    companyAddr = reader.GetString(22)
                                }
                            };
                            registers.Add(register);
                        }
                    }

                    // ส่งข้อมูลกลับในรูปแบบ JSON
                    return Ok(registers);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"เกิดข้อผิดพลาด: {ex.Message}");
            }
        }
    }
}