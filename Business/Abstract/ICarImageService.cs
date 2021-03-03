using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarImageService : IBusinessRepository<CarImage>
    {
        IDataResult <List<CarImage>> GetByWithCarId(int carId);
        IResult Add(IFormFile file, int carId);

        IResult Update(IFormFile file, CarImage carImage);
    }
}