using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class PriceList
{
    public int Id { get; set; }

    public string PriceListCode { get; set; } = null!;

    public string PriceListName { get; set; } = null!;

    public string? PriceListDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
