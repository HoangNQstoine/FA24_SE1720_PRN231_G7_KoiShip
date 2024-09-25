using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class ShippingOrderDetail
{
    public int Id { get; set; }

    public int ShippingOrdersId { get; set; }

    public int KoiFishId { get; set; }

    public int Quantity { get; set; }

    public bool Status { get; set; }

    public virtual KoiFish KoiFish { get; set; } = null!;

    public virtual ShippingOrder ShippingOrders { get; set; } = null!;
}
