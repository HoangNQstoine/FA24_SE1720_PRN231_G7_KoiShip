using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class ShipMent
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Vehicle { get; set; }

    public DateTime? EstimatedArrivalDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string HealthCheck { get; set; }

    public string Description { get; set; }

    public int? Weight { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ShippingOrder> ShippingOrders { get; set; } = new List<ShippingOrder>();

    public virtual User User { get; set; }
}
