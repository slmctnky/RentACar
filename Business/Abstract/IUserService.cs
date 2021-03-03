using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService:IBusinessRepository<User>
    {
        IDataResult<List<OperationClaim>> GetClaims(User user);
       
        IDataResult<User> GetByMail(string Email);
    }
}
