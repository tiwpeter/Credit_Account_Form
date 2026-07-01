using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class Province
{
    public int ProvinceId { get; set; }

    public int CountryId { get; set; }

    public string? ProvinceNameTh { get; set; }

    public string? ProvinceNameEn { get; set; }

    public string? ProvinceCode { get; set; }

    public virtual Country Country { get; set; } = null!;
}
