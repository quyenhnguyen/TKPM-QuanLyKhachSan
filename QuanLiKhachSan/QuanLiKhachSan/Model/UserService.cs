using QuanLiKhachSan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiKhachSan.Model
{
    public class UserService
    {
        private static NHANVIEN _CurrentUser = null;
        public static NHANVIEN GetCurrentUser
        {
            get//để to
            {
                return _CurrentUser;
            }
        }

        public static void LoadUser(NHANVIEN user)
        {
            _CurrentUser = user;
        }
        // GO
    }
}
