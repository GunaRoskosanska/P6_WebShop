using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebShop.Logic.DB;

namespace WebShop.Logic
{
    public class CategoryManager
    {
        public static List<Categories> GetAll()
        {
            using (var db = new DbContext())
            {
                return db.Categories.OrderBy(c => c.Name)
                    .ToList();
            }
        }
        public static Categories Get(int id)
        {
            using (var db = new DbContext())
            {
                return db.Categories.FirstOrDefault(c => c.Id == id);
            }
        }

        public static void Create(string name, int? parentId)
        {
            using (var db = new DbContext())
            {
                db.Categories.Add(new Categories()
                {
                    Name = name,
                    ParentId = parentId,
                });
                db.SaveChanges();
            }
        }
        public static void Delete(int id)
        {
            using (var db = new DbContext())
            {
                db.Categories.Remove(db.Categories.Find(id));
                db.SaveChanges();
            }
        }

        public static int GetItemCount(int id)
        {
            using (var db = new DbContext())
            {
                return db.Items.Count(i => i.CategoryId == id);

                // 2.variants
                //return db.Items.Where(i => i.CategoryId == id).Count();
            }
        }
    }
}


