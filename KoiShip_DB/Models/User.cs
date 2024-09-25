using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int RoleId { get; set; }

    public string Gender { get; set; } = null!;

    public string ImgUrl { get; set; } = null!;

    public int Status { get; set; }

    public virtual ICollection<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<ShipMent> ShipMents { get; set; } = new List<ShipMent>();

    public virtual ICollection<ShippingOrder> ShippingOrders { get; set; } = new List<ShippingOrder>();
}
