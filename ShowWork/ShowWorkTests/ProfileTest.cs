using ShowWork.DAL_MSSQL.Models;
using ShowWorkTests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ShowWorkTests
{
    public class ProfileTest : BaseTest
    {
        
        [Test]
        public async Task AddTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                UserModel user = new UserModel
                {
                    FirstName = "Иван",
                    SecondName = "Иванов",
                    Email = "asdasd@yandex.ru",
                    Login = "Тест"
                };
                await profile.AddOrUpdate(user);
                    
                var results = await profile.Get((int)user.UserId);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("Иван"));
                Assert.That(result.SecondName, Is.EqualTo("Иванов"));
                Assert.That(result.Login, Is.EqualTo("Тест"));
                Assert.That(result.UserId, Is.EqualTo((int)user.UserId));
                Assert.That(result.Email, Is.EqualTo("asdasd@yandex.ru"));
            }
        }

        [Test]
        public async Task UpdateTest()
        {
            using (TransactionScope scope = Helper.CreateTransactionsScope())
            {
                UserModel user = new UserModel
                {
                    FirstName = "Иван",
                    SecondName = "Иванов",
                    Email = "asdasd@yandex.ru",
                    Login = "Тест"
                };
                await profile.AddOrUpdate(user);

                user.FirstName = "Иван1";

                await profile.AddOrUpdate(user);

                var results = await profile.Get((int)user.UserId);
                Assert.That(results.Count(), Is.EqualTo(1));

                var result = results.First();
                Assert.That(result.FirstName, Is.EqualTo("Иван1"));
                Assert.That(result.UserId, Is.EqualTo(user.UserId));
            }
        }
    }
}
