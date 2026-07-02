using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class SaleDistrict
{
    public int Id { get; set; }

    public string SaledisCode { get; set; } = null!;

    public string SaledisName { get; set; } = null!;

    public string? SaledisDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
