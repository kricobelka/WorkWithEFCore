using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WorkingWithEfCore.Database;
using WorkingWithEfCore.Entities;
using WorkingWithEfCore.Models;

namespace WorkingWithEfCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using var context = new MyDbContext();

            //когда мы выполним след часть, не пошлется запрос в EF:
            //context.Users.Where(q => q.Age >= 18);
            //нужно итерировать выборку - и в этот момент пошлется запрос (материализация данных происходит, когда
            //идет итерирование по нашей выборке, после этого сформируется запрос : select * from Users where Age>=18
            //i.e. после этого нужно написать foreach

        // запрос который генерируется в Sql Server Profiler:
        //            SELECT[u].[UserId], [u].[Age], [u].[Email], [u].[FirstName], [u].[LastName]
        //FROM[Users] AS[u]
        //WHERE([u].[UserId] = 1) AND([u].[Age] > 18)
            //можно вместо foreach юзать методы ToList()/ToArray(), которые итурируют по умолчанию:
            //var users = await context.Users.Where(q => q.Age >= 18)
            //    .Select(q => new
            //    {
            //        q.FirstName,
            //        q.LastName
            //    })
            //    .ToListAsync();

            //лучше создать UserModel (это будет не анонимный тип, и мы сможем работать с ним в теч всего приложения), for example:
            //q => new UserModel
            //{

            //      Email = q.Email,
            //    FirstName = q.FirstName,
            //    LastName = q.LastName
            //})
            //    .ToListAsync();


            //var user = context.Users.Where(q => q.UserId == 1)
            //    .SingleOrDefault();

            ////changed age of the user
            //user.Age = 30;
            ////to save changes upon update:
            //context.SaveChanges();

            // чтобы изменить параметр у всех юзеров:
            //var users = context.Users.Where(q => q.Age > 30)
            //    .ToList();

            //foreach (var user in users)
            //{
            //    user.Age = 30;
            //}
            //context.SaveChanges();

            var newUser = new User
            {
                FirstName = "Lilia",
                LastName = "Gagarina",
                Age = 38
            };

            //add user to our database:
            context.Users.Add(newUser);
            context.SaveChanges();

            //removal of user:
            //var removedUser = context.Users
            //    .Where(q => q.UserId == 1)
            //    .SingleOrDefault();

            //context.Users.Remove(removedUser);
            //context.SaveChanges();

            var lilias = context.Users
                .AsNoTracking()
                //когда нужно просто получить информацию,он нужен
                //когда добавляем,  обноляем, тогда не нужен, метод
                //значительно усккоряет работу приложения и не тратит много памяти

                .Include(q => q.Addresses)
                .Where(q => q.FirstName == "Lilia")
                .ToList();
                //с помощь. include подтягиваем зависимые проперти
                //.ThenInclude(q => q) - еслиу  адреса есть зависимая сущность - можем юзать ThenInclude()
                //с помощью include можем получить юзеров с его адресами

            context.SaveChanges();

        foreach (var lilia in lilias)
            {
                //проверка на null^(null ли данная проперти), если да, делает операцию справа
                lilia.Addresses ??= new List<Address>();
                lilia.Addresses.Add(new Address
                {
                    Country = "Belarus",
                    City = "Minsk",
                    Street = "Timiryazeva"
                });
            }

            context.SaveChanges();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}