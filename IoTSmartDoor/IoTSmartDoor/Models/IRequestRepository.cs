using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public interface IRequestRepository
    {
        IEnumerable<Request> GetAll(RequestStatus status);
        IEnumerable<Request> GetAllSent(string senderId);
        Request UpdateStatus(int requestId, RequestStatus newStatus);
        Request Create(Request request);
    }
}
