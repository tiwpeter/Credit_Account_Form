using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class SaleOrg
{
    public int Id { get; set; }

    public string SaleOrgCode { get; set; } = null!;

    public string SaleOrgName { get; set; } = null!;

    public string? SaleOrgDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
