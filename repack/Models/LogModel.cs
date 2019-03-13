using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repack.Entities;

namespace repack.Models
{
    public class LogModel
    {
        private readonly Db _db;

        public LogModel(Db db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackId"></param>
        /// <returns></returns>
        public async Task<List<ReceivedLog>> GetReceivedLog(int stackId, int count = 10)
        {
            return await _db.ReceivedLogs.Where(l => l.StackId == stackId).OrderByDescending(l => l.Created).Take(count)
                .ToListAsync();
        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task<bool> WriteReceivedLog(ReceivedLog log)
        {
            var result = false;
            try
            {
                log.Created = DateTime.Now;
                await _db.ReceivedLogs.AddAsync(log);
                await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception e)
            {

            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<SentLog>> GetSentLog(int taskId, int count = 10)
        {
            return await _db.SentLogs.Where(l => l.TaskId == taskId).OrderByDescending(l => l.Created).Take(count)
                .ToListAsync();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public async Task<bool> WriteSentLog(SentLog log)
        {
            var result = false;
            try
            {
                log.Created = DateTime.Now;
                await _db.SentLogs.AddAsync(log);
                await _db.SaveChangesAsync();
                result = true;
            }
            catch (Exception e)
            {

            }

            return result;
        }
    }
}