using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSmartDoor.Models
{
    public class LogRepository : ILogRepository
    {
        private AppDbContext _context;

        public LogRepository(AppDbContext context)
        {
            _context = context;
        }
        public Log Create(Log log)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> GetAll(string ownerId)
        {
            return _context.Logs.Where(l => l.Owner.Id == ownerId).OrderByDescending(l => l.Time).ToList();
        }
    }
}
