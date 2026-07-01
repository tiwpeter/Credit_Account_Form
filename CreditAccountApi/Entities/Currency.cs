using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Currency
{
    public int Id { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string CurrencyName { get; set; } = null!;

    public string? CurrencyDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
