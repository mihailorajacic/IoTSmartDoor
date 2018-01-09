using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public class Log
    {
        public int Id { get; set; }
        public ApplicationUser Owner { get; set; }
        public ApplicationUser AffectedUser { get; set; }
        public DateTime Time { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
    }
}
