using Microsoft.AspNetCore.Mvc;
using YourApp.Models; // 👈 นำเข้า model Item

namespace YourApp.Controllers
{
    [ApiController]
    [Route("api/Testfect")]
    public class CustomerController : ControllerBase
    {
        // สมมุติว่ามีรายการสินค้า
        private static readonly List<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name = "Item 1", Description = "Description for item 1", Price = 10.5 },
            new Item { Id = 2, Name = "Item 2", Description = "Description for item 2", Price = 20.75 },
            new Item { Id = 3, Name = "Item 3", Description = "Description for item 3", Price = 30.0 }
        };

        // GET api/Testfect/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItemById(int id)
        {
            // ค้นหาสินค้าในรายการตาม id
            var item = Items.FirstOrDefault(i => i.Id == id);

            // ถ้าไม่พบรายการสินค้าตาม id
            if (item == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            // ถ้าพบ ก็ส่งกลับข้อมูล
            return Ok(item);
        }
    }
}
namespace YourApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
