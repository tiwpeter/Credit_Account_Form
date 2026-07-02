using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class SalePerson
{
    public int Id { get; set; }

    public string SalePersonCode { get; set; } = null!;

    public string SalePersonName { get; set; } = null!;

    public string? SalePersonDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
