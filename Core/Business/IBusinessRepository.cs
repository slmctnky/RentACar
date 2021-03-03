using Core.Entities.Abstract;
using Core.Utilities.Results;
using System.Collections.Generic;

namespace Core.Business
{
    public interface IBusinessRepository<TEntity> where TEntity:class,IEntity,new()
    {
        IDataResult< List<TEntity>> GetAll();
        IDataResult<TEntity> GetById(int entityId);
      
        IResult Add(TEntity entity);
        IResult Update(TEntity entity);
        IResult Delete(TEntity entity);

        IResult DeleteRange(List<TEntity> entities);
    }
}
