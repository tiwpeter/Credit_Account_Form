using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class SaleGroup
{
    public int Id { get; set; }

    public string SaleGroCode { get; set; } = null!;

    public string SaleGroName { get; set; } = null!;

    public string? SaleGroDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
