using System;
using System.Collections.Generic;

namespace WebShop.Logic.DB
{
    public partial class Users
    {
        public Users()
        {
            UserCart = new HashSet<UserCart>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<UserCart> UserCart { get; set; }
    }
}
