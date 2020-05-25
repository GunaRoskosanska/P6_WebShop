using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using WebShop.Logic.DB;

namespace WebShop.Logic
{
    public class UserCartManager
    {
        public static void Create(int userId, int itemId)
        {
            using (var db = new DbContext())
            {
                db.UserCart.Add(new UserCart()
                {
                    UserId = userId,
                    ItemId = itemId,
                });
                db.SaveChanges();
            }
        }

        public static List<UserCart> GetByUser(int userId)
        {
            using (var db = new DbContext())
            {
            // atlasa lietotāja groza ierakstus
            // var userCart = db.UserCart.Where(c => c.UserId == userId).ToList();

            // katram groza ierakstam atlasa atbilstošā 'Item' datus
            // TODO: izmantot SQL join
            // foreach (var item in userCart)
            // {
            //     item.Item = db.Items.Find(item.ItemId);
            // }
            var userCart = db.UserCart.Where(c => c.UserId == userId)
                    .Join(db.Items, c => c.ItemId, i => i.Id, (c, i) => new UserCart()
                    {
                        Item = i
                    }).ToList();

                return userCart;
            }
        }
        public static void DeleteFromCart(int id)
        {
            using (var db = new DbContext())
            {
                db.UserCart.Remove(db.UserCart.FirstOrDefault(i => i.ItemId == id));
                db.SaveChanges();
            }
        }

        public static int GetEqualItemCountInCart(int id)
        {
            using (var db = new DbContext())
            {
                return db.UserCart.Count(i => i.ItemId == id);
            }
        }

        public static void ConfirmOrderAndDeleteItemsFromCart(int userId)
        {
            using (var db = new DbContext())
            {
                db.UserCart.RemoveRange(db.UserCart.Where(i => i.UserId == userId));
                db.SaveChanges();
            }
        }
    }
}