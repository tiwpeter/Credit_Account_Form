using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class CustGroupCountry
{
    public int Id { get; set; }

    public string CustgroCountryCode { get; set; } = null!;

    public string CustgroCountryName { get; set; } = null!;

    public string? CustgroCountryDes { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
