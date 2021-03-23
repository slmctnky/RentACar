using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{

    public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarDBContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (CarDBContext context=new CarDBContext())
            {
                var result = (from cus in context.Customers
                              join us in context.Users on cus.UserId equals us.Id
                              select new CustomerDetailDto
                              {
                                  CompanyName = cus.CompanyName,
                                  FirstName = us.FirstName,
                                  LastName = us.LastName
                              });
                return result.ToList();
            }
        }
    }
}
