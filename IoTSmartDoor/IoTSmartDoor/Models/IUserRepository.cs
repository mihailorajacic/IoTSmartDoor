using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetAll(bool allowed);
        ApplicationUser UpdateStatus(string userId, string persistedFaceId, bool allowed);
    }
}
