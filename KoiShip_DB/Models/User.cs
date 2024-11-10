using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public DateOnly? Dob { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public int? RoleId { get; set; }

    public string Gender { get; set; }

    public string ImgUrl { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();

    public virtual Role Role { get; set; }

    public virtual ICollection<ShipMent> ShipMents { get; set; } = new List<ShipMent>();

    public virtual ICollection<ShippingOrder> ShippingOrders { get; set; } = new List<ShippingOrder>();
}
