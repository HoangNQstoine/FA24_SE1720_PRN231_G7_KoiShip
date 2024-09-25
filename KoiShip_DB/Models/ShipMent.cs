using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class ShipMent
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Vehicle { get; set; } = null!;

    public DateTime EstimatedArrivalDate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string HealthCheck { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<ShippingOrder> ShippingOrders { get; set; } = new List<ShippingOrder>();

    public virtual User User { get; set; } = null!;
}
