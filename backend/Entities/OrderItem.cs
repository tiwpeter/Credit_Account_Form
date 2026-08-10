using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class OrderItem
{
    public int? Id { get; set; }

    public int? OrderId { get; set; }

    public int? ProductId { get; set; }

    public string? Name { get; set; }

    public float? Price { get; set; }

    public int? Quantity { get; set; }
}
