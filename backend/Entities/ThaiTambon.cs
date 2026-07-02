using System;
using System.Collections.Generic;

namespace CreditAccountApi.Entities;

public partial class ThaiTambon
{
    public int Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string? NameEn { get; set; }

    public string? ZipCode { get; set; }

    public int AmphureId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ThaiAmphure Amphure { get; set; } = null!;
}
