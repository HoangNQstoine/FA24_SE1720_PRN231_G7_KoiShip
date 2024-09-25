using System;
using System.Collections.Generic;

namespace KoiShip_DB.Data.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
