using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;


        public CustomerManager(ICustomerDal entityDal)
        {
            _customerDal = entityDal;
        }



        public IResult Add(Customer entity)
        {

            _customerDal.Add(entity);
            return new SuccessResult("Kayıt Eklendi");
        }

        public IResult Delete(Customer entity)
        {
            _customerDal.Delete(entity);
            return new SuccessResult();
        }

        public IResult DeleteRange(List<Customer> entities)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }



        public IDataResult<Customer> GetById(int entityId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(x => x.Id == entityId));
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
        }

        public IResult Update(Customer entity)
        {
            _customerDal.Update(entity);
            return new SuccessResult();
        }

    }
}