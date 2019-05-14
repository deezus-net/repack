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

        public async Task<SearchResult<SystemLog>> Search(string type, DateTime? from, DateTime? to, int page)
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

            var result = new SearchResult<SystemLog>
            {
                DataCount = await q.CountAsync(),
                Page = page,
            };
            result.Data = await q.OrderByDescending(l => l.Created)
                .Skip((page - 1) * result.DataPerPage).Take(result.DataPerPage).ToListAsync();


            return result;
        }
    }
}