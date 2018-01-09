using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public class Request
    {
        public int Id { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
        public DateTime Time { get; set; }
        public RequestStatus Status { get; set; }
    }
}
