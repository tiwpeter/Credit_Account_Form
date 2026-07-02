using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class General
{
    public int GeneralId { get; set; }

    public string GeneralName1 { get; set; } = null!;

    public string? GeneralName2 { get; set; }

    public string? GeneralTel { get; set; }

    public string? GeneralFax { get; set; }

    public string? GeneralEmail { get; set; }

    public string? GeneralLine { get; set; }

    public string? GeneralTax { get; set; }

    public string? GeneralBranch { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();

    public virtual ICollection<Shipping> Shippings { get; set; } = new List<Shipping>();
}
