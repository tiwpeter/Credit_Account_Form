using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class BusinessType
{
    public int BusitypeId { get; set; }

    public string BusiTypeCode { get; set; } = null!;

    public string BusiTypeName { get; set; } = null!;

    public string? BusiTypeDes { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public decimal? RegisteredCapital { get; set; }

    public virtual ICollection<RegisterFrom> RegisterFroms { get; set; } = new List<RegisterFrom>();
}
