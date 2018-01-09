using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public class RequestRepository : IRequestRepository
    {
        private AppDbContext _context;

        public RequestRepository(AppDbContext context)
        {
            _context = context;
        }
        public Request Create(Request request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetAll(RequestStatus status)
        {
            return _context.Requests.Where(r => r.Status == status).OrderByDescending(r => r.Time).ToList();
        }

        public IEnumerable<Request> GetAllSent(string senderId)
        {
            return _context.Requests.Where(r => r.Sender.Id == senderId).OrderByDescending(r => r.Time).ToList();
        }

        public Request UpdateStatus(int requestId, RequestStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
