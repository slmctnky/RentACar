using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;


        public ColorManager(IColorDal entityDal)
        {
            _colorDal = entityDal;
        }



        public IResult Add(Color entity)
        {

            _colorDal.Add(entity);
            return new SuccessResult("Kayıt Eklendi");
        }

        public IResult Delete(Color entity)
        {
            _colorDal.Delete(entity);
            return new SuccessResult();
        }

        public IResult DeleteRange(List<Color> entities)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());
        }



        public IDataResult<Color> GetById(int entityId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(x => x.Id == entityId));
        }

        public IResult Update(Color entity)
        {
            _colorDal.Update(entity);
            return new SuccessResult();
        }

    }
}