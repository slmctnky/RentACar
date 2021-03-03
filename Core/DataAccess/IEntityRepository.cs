using Core.Entities.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IEntityRepository<TEntity> where TEntity:class,IEntity, new()
    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

        TEntity Get(Expression<Func<TEntity, bool>> filter);

        IResult Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        void DeleteRange(List<TEntity> entities);
    }
}
