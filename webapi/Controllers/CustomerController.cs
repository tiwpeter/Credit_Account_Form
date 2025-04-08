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
    new Item
    {
        Id = 1,
        Name = "Item 1",
        Description = "Description for item 1",
        Price = 10.5,
        Province = new Province { NameEn = "Bangkok" }
    },
    new Item
    {
        Id = 2,
        Name = "Item 2",
        Description = "Description for item 2",
        Price = 20.75,
        Province = new Province { NameEn = "Chiang Mai" }
    },
    new Item
    {
        Id = 3,
        Name = "Item 3",
        Description = "Description for item 3",
        Price = 30.0,
        Province = new Province { NameEn = "Phuket" }
    }
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
        // PUT api/Testfect/{id}
        [HttpPut("{id}")]
        public ActionResult<Item> UpdateItem(int id, [FromBody] Item updatedItem)
        {
            // ค้นหาสินค้าในรายการตาม id
            var item = Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound(new { message = "Item not found" });
            }

            // อัปเดตข้อมูลสินค้า
            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            item.Price = updatedItem.Price;
            item.Province = new Province
            {
                NameEn = updatedItem.Province.NameEn
            };

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

        public Province Province { get; set; }

    }

    public class Province
    {
        public string NameEn { get; set; }
    }
}
