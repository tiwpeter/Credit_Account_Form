using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class CreditInfo
{
    public int CreditinfoId { get; set; }

    public decimal? EstimatedPurchase { get; set; }

    public string? TimeRequired { get; set; }

    public decimal? CreditLimit { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
