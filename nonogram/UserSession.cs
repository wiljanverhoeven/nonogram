using System;
using System.Windows.Forms;

namespace nonogram
{
    public static class UserSession
    {
        public static User CurrentUser { get; private set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static void Login(User user)
        {
            CurrentUser = user;
            //MessageBox.Show($"DEBUG: UserSession Login - UserId: {user.UserId}, Username: {user.Username}");
        }

        public static void Logout()
        {
            //MessageBox.Show($"DEBUG: UserSession Logout - Previous UserId: {CurrentUser?.UserId}");
            CurrentUser = null;
        }

        public static int? CurrentUserId => CurrentUser?.UserId;
    }
}