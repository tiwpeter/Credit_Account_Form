using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace CreateTable.Controllers
{
    [ApiController]
    [Route("api/shipping")]
    public class ShippingController : ControllerBase
    {
        private readonly string connectionString = "Data Source=test.db";

        [HttpGet]
        public ActionResult<IEnumerable<Shipping>> GetShippingInfo()
        {
            var shippingList = new List<Shipping>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand("SELECT shipping_id, DeliveryName, address1, district, province, postalCode FROM Shipping", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var shipping = new Shipping
                        {
                            ShippingId = reader.GetInt32(0),
                            DeliveryName = reader.GetString(1),
                            Address1 = reader.GetString(2),
                            District = reader.GetString(3),
                            Province = reader.GetString(4),
                            PostalCode = reader.GetString(5)
                        };

                        shippingList.Add(shipping);
                    }
                }
            }

            return Ok(shippingList);
        }
    }

    public class Shipping
    {
        public int ShippingId { get; set; }
        public string DeliveryName { get; set; }
        public string Address1 { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
}
