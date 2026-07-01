using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class ThaiProvince
{
    public int Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string? NameEn { get; set; }

    public int GeographyId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ThaiGeography Geography { get; set; } = null!;

    public virtual ICollection<ThaiAmphure> ThaiAmphures { get; set; } = new List<ThaiAmphure>();
}
