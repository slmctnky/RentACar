﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        ICarImageService _carImageService;

        public CarManager(ICarDal carDal,ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }

        
        //[SecuredOperation("car.add,admin")]
        public IResult Add(Car entity)
        {
            ValidationTool.Validate(new CarValidator(),entity);
            //if (entity.Name.Length<2)
            //{
            //    return new ErrorResult("Araba adı en az 2 karakterden oluşmalıdır.");
            //}
            //if (entity.DailyPrice<=0)
            //{
            //    return new ErrorResult("Araba günlük kira ücreti 0 dan büyük olmalıdır.");
            //}
            _carDal.Add(entity);
            return new SuccessResult("Kayıt Eklendi");
        }

        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult();
        }
        [CacheAspect]
        //[LogAspect(typeof(DatabaseLogger))]
        //[LogAspect(typeof(FileLogger))]
        public IDataResult<List<Car>> GetAll()
        {
           
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }


        [CacheAspect]
        public IDataResult<Car> GetById(int entityId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(x => x.Id == entityId));
        }

        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult();
        }

       

        public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorId(int id)
        {
            var result = _carDal.GetAll(c => c.ColorId == id);
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IResult DeleteRange(List<Car> entities)
        {
            _carDal.DeleteRange(entities);
            return new SuccessResult();
            
        }

        public IDataResult<List<CarImage>> GetCarImages(int carId)
        {
            var result = _carImageService.GetByWithCarId(carId).Data;
            if (result.Count==0)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage>() {
                    new CarImage() {
                        CarId=carId,
                        ImagePath="Resources/carimages/default.png"
                    } }) ;
            }
            return new SuccessDataResult<List<CarImage>>(result);

        }
    }
}
