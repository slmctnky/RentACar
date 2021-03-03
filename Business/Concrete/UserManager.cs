using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _dal;


        public UserManager(IUserDal entityDal)
        {
            _dal = entityDal;
        }


        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User entity)
        {
            
            _dal.Add(entity);
            return new SuccessResult("Kayıt Eklendi");
        }

        private bool CheckEmailIsUsed(string email)
        {
            var users = _dal.GetAll(x => x.Email == email);
            if (users.Count != 0)
            {
                return true;
            }
            else
            {
               return false;
            }
        }


        public IDataResult<User> GetByMail(string email)
        {
            return new SuccessDataResult<User>(_dal.Get(u => u.Email == email));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>( _dal.GetClaims(user));
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_dal.GetAll());
        }

        public IDataResult<User> GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public IResult Update(User entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IResult DeleteRange(List<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}