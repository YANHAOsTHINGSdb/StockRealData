using LooWooTech.AssetsTrade.Models;
using LooWooTech.AssetsTrade.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Managers
{
    public class UserManager : ManagerBase
    {
        public User GetUser(string username, string password = null)
        {
            if(string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }
            using (var db = GetDbContext())
            {
                var entity = db.Users.FirstOrDefault(e => e.Username.ToLower() == username.ToLower());
                if(entity == null)
                {
                    throw new ArgumentException("用户不存在");
                }
                if(!string.IsNullOrEmpty(password) && entity.Password != password.MD5())
                {
                    throw new ArgumentException("密码不正确");
                }
                return entity;
            }
        }

        public void Logout(string currentUserId)
        {
            
        }

        public void Save(User user)
        {
            using (var db = GetDbContext())
            {
                var entity = db.Users.FirstOrDefault(e => e.Username.ToLower() == user.Username.ToLower());
                if(entity != null)
                {
                    entity.Password = user.Password.MD5();
                    db.SaveChanges();
                }
            }
        }
    }
}
