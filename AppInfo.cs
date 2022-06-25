using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop
{
    static class AppInfo
    {
        private static int currentUserId { get; set; }
        public static int GetCurrentUser() => currentUserId;
        public static void SetEmployee(int employee) => currentUserId = employee;
    }
}
