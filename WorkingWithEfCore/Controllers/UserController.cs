using Microsoft.AspNetCore.Mvc;
using WorkingWithEfCore.Database;
using WorkingWithEfCore.Entities;
using WorkingWithEfCore.Models;

namespace WorkingWithEfCore.Controllers
{
    public class UserController : Controller
    {
        public IActionResult AddCompanyRelatedData()
        {
            using var context = new MyDbContext();
            var companies = new List<Company>()
            {
                new Company
                {
                    Name = "Microsoft",
                    Users = new List<User>()
                    {
                        new User
                        {
                            FirstName = "Dima",
                            LastName = "Danon",
                            Age = 20,
                            Contact = new Contact
                            {
                                Email = "tEST@TESST.COM",
                                PhoneNumber = "12345"
                            },

                            Addresses = new List<Address>
                            {
                                new Address
                                {
                                    City ="Misk",
                                    Country = "Belarus",
                                    Street = "Test Street"
                                }
                            }
                        }
                    }
                }
            };

            context.AddRange(companies);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateYearsUntilRetirement()
        {
            using var context = new MyDbContext();

            var users = context.Users.ToArray();
            foreach (var user in users)
            {
                var yearsDiff = 60 - user.Age;
                user.YearsUntilRetirement = yearsDiff < 0 ? 0 : yearsDiff;

            }
            context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult DeleteUserOnRetirement()
        {
            using var context = new MyDbContext();
            var usersOnRetirement = context.Users
                .Where(q => q.YearsUntilRetirement == 0)
                .ToArray();

            context.RemoveRange(usersOnRetirement);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetUsersInfo()
        {
            using var context = new MyDbContext();
            var users = context.Users.Select(q => new UserInfoModel
            {
                Age = q.Age,
                CompanyName = q.CompanyId.HasValue ? q.Company.Name,
                FullContact = $"{q.Contact.Email} {q.Contact.PhoneNumber}",
                FullName = $"{q.FirstName} {q.LastName}",
                FirstFullAddress = $"{q.Addresses.First().Country} {q.Addresses.First().City} {q.Addresses.First()}"
            })
                .ToArray();

            context.RemoveRange(users);
            context.SaveChanges();
            return View(users);
            //return RedirectToAction("Index", "Home");
        }
    }
}
