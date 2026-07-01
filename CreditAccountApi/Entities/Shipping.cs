using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Shipping
{
    public int ShippingId { get; set; }

    public int? GeneralId { get; set; }

    public string? AddrLine1 { get; set; }

    public string? AddrLine2 { get; set; }

    public string? SubDistrict { get; set; }

    public string? District { get; set; }

    public string? Province { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public virtual General? General { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
