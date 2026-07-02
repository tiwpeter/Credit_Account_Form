using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class SaleMaster
{
    public int Id { get; set; }

    public string SaleGroupCode { get; set; } = null!;

    public string SaleGroupName { get; set; } = null!;

    public string? SaleGroupDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
