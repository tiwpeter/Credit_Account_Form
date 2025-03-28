using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using RegisteersTable.Models; // ต้องตรงกับ namespace ของ RegisterModel

namespace RegisteersTable.Registeers
{
    [ApiController]
    [Route("api/register")]
    public class RegisterController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=test.db"; // เปลี่ยนเป็นพาธของไฟล์ฐานข้อมูลของคุณ

        // POST: api/register
        [HttpPost]
        public IActionResult AddRegister([FromBody] RegisterModel register)
        {
            try
            {
                // ตรวจสอบค่าที่ส่งเข้ามา
                Console.WriteLine("Received RegisterModel:");
                Console.WriteLine($"General Name: {register.General.GeneralName1}");
                Console.WriteLine($"General Tel: {register.General.GeneralTel}");
                Console.WriteLine($"Shipping Address: {register.Shipping.address1}, {register.Shipping.district}, {register.Shipping.province}");
                Console.WriteLine($"Shop Code: {register.ShopType.shopCode}");

                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // เพิ่มข้อมูลในตาราง Generals
                            long generalId;
                            using (var command = new SqliteCommand("INSERT INTO Generals (generalName1, generalTel, generalFax, generalEmail, generalLine, generalTax, generalBranch) VALUES (@name, @tel, @fax, @email, @line, @tax, @branch)", connection))
                            {
                                // General = model
                                command.Parameters.AddWithValue("@name", register.General.GeneralName1);
                                command.Parameters.AddWithValue("@tel", register.General.GeneralTel);
                                command.Parameters.AddWithValue("@fax", register.General.GeneralFax);
                                command.Parameters.AddWithValue("@email", register.General.GeneralEmail);
                                command.Parameters.AddWithValue("@line", register.General.GeneralLine);
                                command.Parameters.AddWithValue("@tax", register.General.GeneralTax);
                                command.Parameters.AddWithValue("@branch", register.General.GeneralBranch);

                                command.Transaction = transaction;
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception("ไม่สามารถเพิ่มข้อมูลในตาราง Generals");
                                }

                                // ดึง general_id ที่เพิ่มเข้าไป
                                using (var idCommand = new SqliteCommand("SELECT last_insert_rowid()", connection))
                                {
                                    idCommand.Transaction = transaction;
                                    generalId = (long)idCommand.ExecuteScalar();
                                }
                            }

                            // สรุปคือ โค้ดนี้จะทำการแทรก general_id ที่ได้จากการเพิ่มข้อมูลในตาราง Generals ลงในตาราง RegisterFrom
                            using (var command = new SqliteCommand("INSERT INTO RegisterFrom (general_id) VALUES (@generalId)", connection))
                            {
                                command.Parameters.AddWithValue("@generalId", generalId);
                                command.Transaction = transaction;

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception("ไม่สามารถเพิ่มข้อมูลในตาราง RegisterFrom");
                                }
                            }

                            // เพิ่มข้อมูลในตาราง Addresses โดยใช้ข้อมูลที่อยู่จาก RegisterModel
                            using (var command = new SqliteCommand("INSERT INTO Addresses (general_id, addrType, addrLine1, addrLine2, subDistrict, district, province, postalCode, country) VALUES (@generalId, @addrType, @addrLine1, @addrLine2, @subDistrict, @district, @province, @postalCode, @country)", connection))
                            {
                                command.Parameters.AddWithValue("@generalId", generalId);
                                command.Parameters.AddWithValue("@addrType", register.Address.AddrType);
                                command.Parameters.AddWithValue("@addrLine1", register.Address.AddrLine1);
                                command.Parameters.AddWithValue("@addrLine2", register.Address.AddrLine2);
                                command.Parameters.AddWithValue("@subDistrict", register.Address.SubDistrict);
                                command.Parameters.AddWithValue("@district", register.Address.District);
                                command.Parameters.AddWithValue("@province", register.Address.Province);
                                command.Parameters.AddWithValue("@postalCode", register.Address.PostalCode);
                                command.Parameters.AddWithValue("@country", register.Address.Country);

                                command.Transaction = transaction;

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception("ไม่สามารถเพิ่มข้อมูลในตาราง Addresses");
                                }
                            }


                            // เพิ่มข้อมูลในตาราง Addresses โดยใช้ข้อมูลที่อยู่จาก RegisterModel
                            long shippingId;
                            using (var command = new SqliteCommand("INSERT INTO Shipping (DeliveryName, address1, district, province, postalCode) VALUES (@deliveryName, @address1, @district, @province, @postalCode)", connection))
                            {
                                // กำหนดพารามิเตอร์
                                command.Parameters.AddWithValue("@deliveryName", register.Shipping.DeliveryName);
                                command.Parameters.AddWithValue("@address1", register.Shipping.address1);
                                command.Parameters.AddWithValue("@district", register.Shipping.district);
                                command.Parameters.AddWithValue("@province", register.Shipping.province);
                                command.Parameters.AddWithValue("@postalCode", register.Shipping.postalCode);

                                command.Transaction = transaction;

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception("ไม่สามารถเพิ่มข้อมูลในตาราง Shipping");
                                }

                                // ดึง shipping_id ที่เพิ่งแทรกเข้าไป
                                using (var idCommand = new SqliteCommand("SELECT last_insert_rowid()", connection))
                                {
                                    idCommand.Transaction = transaction;
                                    shippingId = (long)idCommand.ExecuteScalar();
                                }
                            }

                            // เพิ่มข้อมูลในตาราง ShopType
                            using (var command = new SqliteCommand("INSERT INTO ShopType (shopCode, shopName, shopDes, accGroupName) VALUES (@shopCode, @shopName, @shopDes, @accGroupName)", connection))
                            {
                                command.Parameters.AddWithValue("@shopCode", register.ShopType.shopCode);
                                command.Parameters.AddWithValue("@shopName", register.ShopType.shopName);
                                command.Parameters.AddWithValue("@shopDes", register.ShopType.shopDes);
                                command.Parameters.AddWithValue("@accGroupName", register.ShopType.accGroupName);

                                command.Transaction = transaction;
                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception("ไม่สามารถเพิ่มข้อมูลในตาราง ShopType");
                                }
                            }

                            // เพิ่มข้อมูลในตาราง Company
                            long company_id;
                            using (var command = new SqliteCommand("INSERT INTO Company (companyCode, companyName, companyAddr) VALUES (@companyCode, @companyName, @companyAddr)", connection))
                            {
                                // กำหนดพารามิเตอร์ให้ตรงกับคอลัมน์ในตาราง
                                command.Parameters.AddWithValue("@companyCode", register.Company.companyCode);
                                command.Parameters.AddWithValue("@companyName", register.Company.companyName);
                                command.Parameters.AddWithValue("@companyAddr", register.Company.companyAddr);

                                command.Transaction = transaction;

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception("ไม่สามารถเพิ่มข้อมูลในตาราง Company");
                                }

                                // ดึง company_id ที่เพิ่งแทรกเข้าไป
                                using (var idCommand = new SqliteCommand("SELECT last_insert_rowid()", connection))
                                {
                                    idCommand.Transaction = transaction;
                                    company_id = (long)idCommand.ExecuteScalar();
                                }
                            }

                            // เพิ่มข้อมูลในตาราง IndustryType
                            long IndustryTypeId;
                            using (var command = new SqliteCommand("INSERT INTO IndustryType (InduTypeCode, InduTypeName, InduTypeDes) VALUES (@InduTypeCode, @InduTypeName, @InduTypeDes)", connection))
                            {
                                // กำหนดพารามิเตอร์ให้ตรงกับคอลัมน์ในตาราง
                                command.Parameters.AddWithValue("@InduTypeCode", register.IndustryType.InduTypeCode);
                                command.Parameters.AddWithValue("@InduTypeName", register.IndustryType.InduTypeName);
                                command.Parameters.AddWithValue("@InduTypeDes", register.IndustryType.InduTypeDes);

                                command.Transaction = transaction;

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception("ไม่สามารถเพิ่มข้อมูลในตาราง IndustryType");
                                }

                                // ดึง IndustryTypeId ที่เพิ่งแทรกเข้าไป
                                using (var idCommand = new SqliteCommand("SELECT last_insert_rowid()", connection))
                                {
                                    idCommand.Transaction = transaction;
                                    IndustryTypeId = (long)idCommand.ExecuteScalar();
                                }
                            }

                            // คอมมิทการทำงานทั้งหมด
                            transaction.Commit();

                            // Return the generated general_id along with a success message
                            return Ok(new { message = "ข้อมูลถูกเพิ่มเรียบร้อยแล้ว", generalId = generalId });
                        }
                        catch (Exception ex)
                        {
                            // ยกเลิกการทำงานทั้งหมดหากเกิดข้อผิดพลาด
                            transaction.Rollback();
                            return StatusCode(500, $"เกิดข้อผิดพลาด: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ข้อผิดพลาดที่ไม่คาดคิด: {ex.Message}");
            }
        }
    }
}
