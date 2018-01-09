using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<ApplicationUser> GetAll(bool allowed)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser UpdateStatus(string userId, string persistedFaceId, bool allowed)
        {
            throw new NotImplementedException();
        }
    }
}
