using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repack.Entities;
using Task = System.Threading.Tasks.Task;

namespace repack.Models
{
    public class SystemLogModel
    {
        private readonly Db _db;

        public SystemLogModel(Db db)
        {
            _db = db;
        }

        public async Task Write(string type, string message)
        {
            _db.SystemLogs.Add(new SystemLog {Type = type, Message = message, Created = DateTime.Now});
            await _db.SaveChangesAsync();
        }

        public async Task<List<SystemLog>> Search(string type, DateTime? from, DateTime? to)
        {
            var q = _db.SystemLogs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(type))
            {
                q = q.Where(l => l.Type == type);
            }

            if (from != null)
            {
                q = q.Where(l => l.Created >= from);
            }

            if (to != null)
            {
                q = q.Where(l => l.Created < to);
            }

            return await q.OrderByDescending(l => l.Created).ToListAsync();
        }
    }
}