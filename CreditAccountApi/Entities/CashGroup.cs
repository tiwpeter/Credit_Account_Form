using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class CashGroup
{
    public int Id { get; set; }

    public string CashCode { get; set; } = null!;

    public string CashName { get; set; } = null!;

    public string? CashDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
