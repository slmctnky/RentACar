using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarDBContext>, IRentalDal
    {
        public List<RentalDetailDto> GetAllWithDetail()
        {
            using (CarDBContext context=new CarDBContext())
            {
                var result = from r in context.Rentals
                             join c in context.Customers on r.CustomerId equals c.Id
                             join u in context.Users on c.UserId equals u.Id
                             join car in context.Cars on r.CarId equals car.Id
                             join b in context.Brands on car.BrandId equals b.Id
                             select new RentalDetailDto
                             {
                                 Id=r.Id,
                                 CarName = b.Name,
                                 CustomerName = u.FirstName + " " + u.LastName,
                                 RentDate=r.RentDate,
                                 ReturnDate=r.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}