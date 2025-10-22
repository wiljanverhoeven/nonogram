using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nonogram
{
    public static class UserService
    {
        private static List<User> _users = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Email = "admin@nonogram.com" }
        };

        public static bool Register(string username, string password, string email)
        {
            if (_users.Any(u => u.Username == username))
                return false;

            _users.Add(new User { Username = username, Password = password, Email = email });
            return true;
        }

        public static bool Login(string username, string password)
        {
            return _users.Any(u => u.Username == username && u.Password == password);
        }
    }
}