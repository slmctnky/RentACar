using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _dal;


        public CarImageManager(ICarImageDal entityDal)
        {
            _dal = entityDal;
        }



        public IResult Add(CarImage entity)
        {

            //_dal.Add(entity);
            //return new SuccessResult("Kayıt Eklendi");
            return new ErrorResult("") ;
        }

        public IResult Add(IFormFile file,int carId)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carId));
            if (result != null)
            {
                return result;
            }
            CarImage carImage = new CarImage();
            carImage.CarId = carId;
            carImage.ImagePath = FileHelper.Save(file, Path.Combine("resources", "carimages"));
            carImage.Date = DateTime.Now;
            var isAdded=_dal.Add(carImage);
            if (isAdded.Success)
            {
                return new SuccessResult(file.FileName+":eklendi.");
            }
            FileHelper.Delete(carImage.ImagePath);
            return new ErrorResult(isAdded.Message);
        }

        private IResult CheckImageLimitExceeded(int carid)
        {
            var carImagecount = _dal.GetAll(p => p.CarId == carid).Count;
            if (carImagecount >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitExceeded);
            }

            return new SuccessResult();
        }
        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _dal.GetAll(c => c.CarId == carId);
           // IfCarImageOfCarNotExistsAddDefault(ref result);

            return new SuccessDataResult<List<CarImage>>(result);
        }
        public IResult Delete(CarImage entity)
        {
            string dbPath = entity.ImagePath;
            FileHelper.Delete(dbPath);
            _dal.Delete(entity);
            return new SuccessResult();
        }

        public IResult DeleteRange(List<CarImage> entities)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_dal.GetAll());
        }



        public IDataResult<CarImage> GetById(int entityId)
        {
            return new SuccessDataResult<CarImage>(_dal.Get(x => x.Id == entityId));
        }

        public IDataResult<List<CarImage>> GetByWithCarId(int carId)
        {
            return new SuccessDataResult< List<CarImage >> (_dal.GetAll(x => x.CarId == carId));
        }

        public IResult Update(CarImage entity)
        {
            _dal.Update(entity);
            return new SuccessResult();
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {

            FileHelper.Delete(carImage.ImagePath);
            carImage.ImagePath = FileHelper.Save(file, Path.Combine("resources", "carimages"));
            carImage.Date = DateTime.Now;
            _dal.Update(carImage);
            return new SuccessResult();
        }
    }
}