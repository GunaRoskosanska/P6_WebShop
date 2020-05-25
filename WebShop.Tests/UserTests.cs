using System;
using System.Net.Mail;
using WebShop.Logic;
using Xunit;

namespace WebShop.Tests
{
    public class UserTests
    {
        [Fact]
        public void TestCreateUser()
        {
            string mail = GetRandomText();
            UserManager.Create(mail, "testname", "testpass");
            var user = UserManager.GetByEmail(mail);
            // ja asertacija ir pareiza -> tests veismigs
            Assert.NotNull(user);
            Assert.Equal(user.Email, mail);
        }

       [Fact]
        public void TestGetUser()
        {
            string mail = GetRandomText();
            string password = "testpass";
            UserManager.Create(mail, "testname", password);

            var user = UserManager.GetByEmailAndPassword(mail, password);
            Assert.NotNull(user);
            Assert.Equal(user.Email, mail);
            Assert.Equal(user.Password, password);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
        }
        
        public static string GetRandomText()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8);
        }

       // [Fact]
        //public void TestCreateItem()
       // {
            //string mail = GetRandomText();
           // ItemManager.Create(5, "testname", "testdescription", 1050);

            //var item = ItemManager
                //.GetByEmail(mail);

           // Assert.NotNull(item);
           // Assert.Equal(user.Email, mail);
        //}
 