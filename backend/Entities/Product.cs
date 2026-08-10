using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Product
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public float? Price { get; set; }

    public int? CategoryId { get; set; }

    public float? SalePercent { get; set; }

    public string? DiscountEndTime { get; set; }

    public float? Rating { get; set; }

    public int? Sold { get; set; }

    public int? Stock { get; set; }

    public DateTime? CreatedAt { get; set; }
}
