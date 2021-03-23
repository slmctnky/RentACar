using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;

using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarDBContext>, ICarDal
    {
      

        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (CarDBContext context=new CarDBContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             select new CarDetailDto
                             {
                                 CarName = c.Name,
                                 ColorName = col.Name,
                                 BrandName = b.Name,
                                 DailyPrice = c.DailyPrice,
                                 ColorId=c.ColorId,
                                 BrandId=b.Id,
                                 Id=c.Id
                             };
                
                return filter == null ? result.ToList(): result.Where(filter).ToList();

            }
        }

        
    }
}
