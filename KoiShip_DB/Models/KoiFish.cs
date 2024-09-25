using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class KoiFish
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public int Age { get; set; }

    public string ColorPattern { get; set; } = null!;

    public double? Price { get; set; }

    public string? Description { get; set; }

    public string? UrlImg { get; set; }

    public bool Status { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ShippingOrderDetail? ShippingOrderDetail { get; set; }

    public virtual User User { get; set; } = null!;
}
