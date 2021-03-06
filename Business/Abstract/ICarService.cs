﻿using Core.Business;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService:IBusinessRepository<Car>
    {

        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailDto>> GetCarsByBrandId(int id);

        IDataResult<List<CarDetailDto>> GetCarsByColorId(int id);
        IDataResult<List<CarImage>> GetCarImages(int carId);
    }
}
