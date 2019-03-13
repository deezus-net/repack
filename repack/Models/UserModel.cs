using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using repack.Entities;
using Task = repack.Entities.Task;

namespace repack.Models
{
    public class UserModel
    {
        private readonly Db _db;
        private readonly AppSetting _appSetting;
        public UserModel(Db db, AppSetting appSetting)
        {
            _db = db;
            _appSetting = appSetting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetList()
        {
            return await _db.Users.OrderBy(t => t.Id).ToListAsync();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> Get(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User> Login(string id, string password)
        {
            // check admin
            if (_appSetting.AdminId == id && _appSetting.AdminPassword == password)
            {
                return new User() {Name = id, IsAdmin = true};
            }
            else
            {
                password = ToPassword(password);
                return await _db.Users.FirstOrDefaultAsync(u => u.Name == id && u.Password == password);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserName(User user)
        {
            if (_appSetting.AdminId == user.Name)
            {
                return false;
            }

            if (user.Id > 0)
            {
                return true;
            }

            var otherUser = await _db.Users.FirstOrDefaultAsync(u => u.Id != user.Id && u.Name == user.Name);
            return otherUser == null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> Update(User user)
        {
            var result = false;
            try
            {
                var currentUser = await _db.Users.FindAsync(user.Id);
                if (currentUser == null)
                {
                    user.Password = ToPassword(user.Password);
                    user.Modified = DateTime.Now;
                    user.Created = DateTime.Now;
                    await _db.Users.AddAsync(user);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(user.Password))
                    {
                        currentUser.Password = ToPassword(user.Password);
                    }

                    currentUser.Modified = DateTime.Now;
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
                _db.Users.Remove(await _db.Users.FindAsync(id));
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
        /// <param name="src"></param>
        /// <returns></returns>
        private string ToPassword(string src)
        {
            var result = src;
            for (var i = 0; i < 10000; i++)
            {
                result = Sha256(result + _appSetting.Salt);
            }

            return result;
        }

        private static string Sha256(string src)
        {
            var result = "";
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(src));
                result = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }

            return result;
        }
    }
}