using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? UrlImg { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();
}
