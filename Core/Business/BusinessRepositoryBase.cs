using Core.DataAccess;
using Core.Entities.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Business
{
    public abstract class BusinessRepositoryBase<TEntity,IBusinessRepository> : IEntityRepository<TEntity>  where TEntity : class, IEntity, new() 
                                                                   where IBusinessRepository : class,IEntityRepository<TEntity> 
    {
        IBusinessRepository _IEntityDal;
       

        public BusinessRepositoryBase(IBusinessRepository entityDal)
        {
            _IEntityDal = entityDal;
        }

        public virtual void Add(TEntity entity)
        {


            _IEntityDal.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _IEntityDal.Delete(entity);
        }

        public void DeleteRange(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll()
        {
            return _IEntityDal.GetAll();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity car)
        {
            _IEntityDal.Update(car);
        }

        IResult IEntityRepository<TEntity>.Add(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
