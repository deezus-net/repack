using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repack.Entities;
using Task = repack.Entities.Task;

namespace repack.Models
{
    public class TaskModel
    {
        private readonly Db _db;
        public TaskModel(Db db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Task>> GetList(int stackId)
        {
            return await _db.Tasks.Where(t => t.StackId == stackId).OrderBy(t => t.OrderNo).ToListAsync();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Task> Get(int id)
        {
            return await _db.Tasks.FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Update(Task task)
        {
            var result = false;
            try
            {
                var currentTask = await _db.Tasks.FindAsync(task.Id);
                if (currentTask == null)
                {
                    task.Modified = DateTime.Now;
                    task.Created = DateTime.Now;
                    task.OrderNo = await _db.Tasks.Where(t => t.StackId == task.StackId).Select(t => t.OrderNo).DefaultIfEmpty(0).MaxAsync() + 1;
                    await _db.Tasks.AddAsync(task);
                }
                else
                {
                    currentTask.Name = task.Name;
                    currentTask.Modified = DateTime.Now;
                }

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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            var result = false;
            try
            {
                _db.Tasks.Remove(await _db.Tasks.FindAsync(id));
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