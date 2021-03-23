using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;


        public RentalManager(IRentalDal entityDal)
        {
            _rentalDal = entityDal;
        }



        public IResult Add(Rental entity)
        {
            Rental rental = _rentalDal.Get(x => x.CarId == entity.CarId && x.ReturnDate == null);
            if (rental!=null)
            {
                return new ErrorResult(Messages.CarIsNotAppropriate);
            }
            var result = _rentalDal.Add(entity);
            
            return result;
           
            
        }

        public IResult Delete(Rental entity)
        {
            _rentalDal.Delete(entity);
            return new SuccessResult();
        }

        public IResult DeleteRange(List<Rental> entities)
        {
            _rentalDal.DeleteRange(entities);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<List<RentalDetailDto>> GetAllWithDetail()
        {
            var result = _rentalDal.GetAllWithDetail();
            return new SuccessDataResult<List<RentalDetailDto>>(result);
        }

        public IDataResult<Rental> GetById(int entityId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(x => x.Id == entityId));
        }

        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult();
        }

       
    }
}