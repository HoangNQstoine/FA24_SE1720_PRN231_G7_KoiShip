using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class ShippingOrder
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PricingId { get; set; }

    public int? ShipMentId { get; set; }

    public string AdressTo { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int TotalPrice { get; set; }

    public string Description { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public DateTime? ShippingDate { get; set; }

    public DateTime? EstimatedDeliveryDate { get; set; }

    public int Status { get; set; }

    public virtual Pricing Pricing { get; set; } = null!;

    public virtual ShipMent? ShipMent { get; set; }

    public virtual ICollection<ShippingOrderDetail> ShippingOrderDetails { get; set; } = new List<ShippingOrderDetail>();

    public virtual User User { get; set; } = null!;
}
