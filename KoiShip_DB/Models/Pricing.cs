﻿using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class Pricing
{
    public int Id { get; set; }

    public int? Quantity { get; set; }

    public string WeightRange { get; set; }

    public string ShippingMethod { get; set; }

    public string Description { get; set; }

    public double? Price { get; set; }

    public string Currency { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ShippingOrder> ShippingOrders { get; set; } = new List<ShippingOrder>();
}
