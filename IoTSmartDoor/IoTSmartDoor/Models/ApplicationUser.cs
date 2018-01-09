using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }
        public string PersistedFaceId { get; set; }
        public bool Allowed { get; set; }
    }
}
