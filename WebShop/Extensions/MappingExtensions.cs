using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Logic.DB;
using WebShop.Models;

namespace WebShop
{
    public static class MappingExtensions
    {
        public static CategoryModel ToModel(this Categories category)
        {
            return new CategoryModel()
            {
                Id = category.Id,
                Name = category.Name,
                ParentId = category.ParentId,
            };
        }

        public static UserModel ToModel(this Users user)
        {
            if(user == null)
            {
                return null;
            }

            return new UserModel()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IsAdmin = user.IsAdmin
                //Password = user.Password,
            };
        }

        public static ItemModel ToModel(this Items item)
        {
            if(item == null)
            {
                return null;
            }

            return new ItemModel()
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                Description = item.Description,
                Image = item.Image,
                Name = item.Name,
                Price = item.Price,
            };
        }
    }
}
