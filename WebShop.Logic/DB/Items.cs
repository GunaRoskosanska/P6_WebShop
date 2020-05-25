using System;
using System.Collections.Generic;

namespace WebShop.Logic.DB
{
    public partial class Items
    {
        public Items()
        {
            UserCart = new HashSet<UserCart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public Categories Category { get; set; }
        public ICollection<UserCart> UserCart { get; set; }
    }
}
