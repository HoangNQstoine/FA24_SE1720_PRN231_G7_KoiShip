using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShip_DB.Data.DTO.Request
{

    public class KoiFishEdit
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int? CategoryId { get; set; }

        public string? Name { get; set; }

        public double? Weight { get; set; }

        public int? Age { get; set; }

        public string? ColorPattern { get; set; }

        public double? Price { get; set; }

        public string? Description { get; set; }

        public string? UrlImg { get; set; }

        public bool? Status { get; set; }
    }
}
