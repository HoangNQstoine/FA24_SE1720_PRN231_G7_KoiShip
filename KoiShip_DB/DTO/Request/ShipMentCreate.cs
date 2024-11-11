﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShip_DB.Data.DTO.Request
{
    public class ShipMentCreate
    {

        public int? UserId { get; set; }

        public string? Vehicle { get; set; }

        public DateTime? EstimatedArrivalDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? HealthCheck { get; set; }

        public string? Description { get; set; }

        public int? Weight { get; set; }

        public bool? Status { get; set; }
    }
}