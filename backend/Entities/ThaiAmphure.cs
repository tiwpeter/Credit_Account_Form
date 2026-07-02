using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class ThaiAmphure
{
    public int Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string? NameEn { get; set; }

    public int ProvinceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ThaiProvince Province { get; set; } = null!;

    public virtual ICollection<ThaiTambon> ThaiTambons { get; set; } = new List<ThaiTambon>();
}
