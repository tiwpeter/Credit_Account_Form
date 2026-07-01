using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Country
{
    public int CountryId { get; set; }

    public string? CountryNameTh { get; set; }

    public string? CountryNameEn { get; set; }

    public string? IsoAlpha2 { get; set; }

    public string? IsoAlpha3 { get; set; }

    public string? OfficialName { get; set; }

    public string? Region { get; set; }

    public string? SubRegion { get; set; }

    public string? CapitalCity { get; set; }

    public virtual ICollection<Province> Provinces { get; set; } = new List<Province>();
}
