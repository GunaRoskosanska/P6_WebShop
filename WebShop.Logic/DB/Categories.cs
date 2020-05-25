using System;
using System.Collections.Generic;

namespace WebShop.Logic.DB
{
    public partial class Categories
    {
        public Categories()
        {
            InverseParent = new HashSet<Categories>();
            Items = new HashSet<Items>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public Categories Parent { get; set; }
        public ICollection<Categories> InverseParent { get; set; }
        public ICollection<Items> Items { get; set; }
    }
}
