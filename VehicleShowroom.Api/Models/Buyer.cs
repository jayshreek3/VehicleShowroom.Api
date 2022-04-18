using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleShowroom.Api.Models
{
    public partial class Buyer
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
    }
}
