using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaveAlertBeta.Models
{
    public class CurrentUser
    {
        private static CurrentUser instance;
        public User user;
        private CurrentUser(User user) {
            this.user = user;
        }
        public static CurrentUser getInstance(User user) {
            if (instance == null) {
                instance = new CurrentUser(user);

            }
            return instance;
        }
    }
}
