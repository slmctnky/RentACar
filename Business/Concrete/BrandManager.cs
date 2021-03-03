using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;


        public BrandManager(IBrandDal entityDal)
        {
            _brandDal = entityDal;
        }



        public IResult Add(Brand entity)
        {

            _brandDal.Add(entity);
            return new SuccessResult("Kayıt Eklendi");
        }

        public IResult Delete(Brand entity)
        {
            _brandDal.Delete(entity);
            return new SuccessResult();
        }

        public IResult DeleteRange(List<Brand> entities)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }



        public IDataResult<Brand> GetById(int entityId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(x => x.Id == entityId));
        }

        public IResult Update(Brand entity)
        {
            _brandDal.Update(entity);
            return new SuccessResult();
        }

    }
}