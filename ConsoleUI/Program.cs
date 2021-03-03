using Autofac;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolver.AutoFac;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarTest();
            //BrandTest();
            //CarListing();
            //AddUser();
     

            var service =AutofacBusinessModule.LoadContainerBuilder<IRentalService>();
           
            Rental rental = new Rental();
            rental.CarId = 2003;
            rental.CustomerId = 2;
            rental.RentDate = DateTime.Now;
            Console.WriteLine(service.Add(rental).Message);
            
        }
        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EfRentalDal>()
                   .As<IRentalDal>()
                   .InstancePerDependency();
            builder.RegisterType<RentalManager>()
                   .As<IRentalService>()
                   .InstancePerDependency();
            return builder.Build();
        }
        private static void AddUser()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            List<User> newUsers = new List<User>();
            //newUsers.Add(new User() { FirstName = "Salim", LastName = "Çetinkaya", Email = "blabla", Password = "123" });
            //newUsers.Add(new User() { FirstName = "Vesile", LastName = "Çetinkaya", Email = "blabla", Password = "123" });
            //newUsers.Add(new User() { FirstName = "Ali", LastName = "Çetinkaya", Email = "blabla", Password = "123" });
            foreach (var user in newUsers)
            {
                IResult result = userManager.Add(user);
                Console.WriteLine(result.Message + " " + user.FirstName);
            }
        }

        private static void CarListing()
        {
            //CarManager carManager = new CarManager(new EfCarDal());

            //var result = carManager.GetCarDetails();
            //foreach (var car in result.Data)
            //{
            //    Console.WriteLine(car.CarName);
            //    Console.WriteLine(car.BrandName);
            //    Console.WriteLine(car.ColorName);
            //    Console.WriteLine(car.DailyPrice);
            //    Console.WriteLine("-------------------");
            //}
        }

        private static void BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(new Brand { Id = 1, Name = "Fiat" });
            brandManager.Add(new Brand { Id = 2, Name = "Honda" });
        }

        private static void CarTest()
        {
            Car car1 = new Car
            {
                Name = "bsdg",
                BrandId = 2,
                ColorId = 3,
                DailyPrice = 12,
                ModelYear = 2020,
                Description = "Makyajlı Kasadır 2."

            };

            //CarManager carManager = new CarManager(new EfCarDal());
            //try
            //{
            //    carManager.Add(car1);
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //}
            //var result = carManager.GetAll();
            //foreach (var car in result.Data )
            //{
            //    Console.WriteLine(car.Name);
            //}
        }
    }
}
