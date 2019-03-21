using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using repack.Entities;
using Task = repack.Entities.Task;

namespace repack.Models
{
    public class StackModel
    {
        private readonly Db _db;
        public StackModel(Db db)
        {
            _db = db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Stack>> GetList()
        {
            return await _db.Stacks.Include(s => s.Tasks).ToListAsync();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Stack> Get(int id)
        {
            return await _db.Stacks.FindAsync(id);
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Stack> GetByToken(string token)
        {
            return await _db.Stacks.Include(s => s.Tasks).FirstOrDefaultAsync(s => s.Token == token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public async Task<bool> Update(Stack stack)
        {
            var result = false;
            try
            {
                var currentStack = await _db.Stacks.FindAsync(stack.Id);
                if (currentStack == null)
                {
                    stack.Modified = DateTime.Now;
                    stack.Created = DateTime.Now;
                    stack.Token = Guid.NewGuid().ToString("N");
                    await _db.Stacks.AddAsync(stack);
                }
                else
                {
                    currentStack.Name = stack.Name;
                    if (!string.IsNullOrWhiteSpace(stack.Token))
                    {
                        currentStack.Token = stack.Token;
                    }
                    currentStack.Modified = DateTime.Now;
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
                _db.Stacks.Remove(await _db.Stacks.FindAsync(id));
                _db.Tasks.RemoveRange(_db.Tasks.Where(t => t.StackId == id));
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