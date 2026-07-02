using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class ShopType
{
    public int Id { get; set; }

    public string ShopCode { get; set; } = null!;

    public string ShopName { get; set; } = null!;

    public string? ShopDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
