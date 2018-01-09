using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public interface ILogRepository
    {
        IEnumerable<Log> GetAll(string ownerId);
        Log Create(Log log);
    }
}
