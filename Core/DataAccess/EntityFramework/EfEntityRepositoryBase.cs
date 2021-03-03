using Core.Entities.Abstract;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
         where TContext : DbContext, new()
    {
        public IResult Add(TEntity entity)
        {
            try
            {
                using (TContext context = new TContext())
                {
                    var addedEntity = context.Entry(entity);
                    addedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    context.SaveChanges();
                }
                return new SuccessResult("Başarılı Şekilde Eklendi.");
            }
            catch (Exception ex)
            {

                 return new ErrorResult("Ekleme sırasında hata oluştu."+Environment.NewLine+ex.Message+" "+ex.InnerException);
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public void DeleteRange(List<TEntity> entities)
        {
            using (TContext context = new TContext())
            {
                context.Set<TEntity>().RemoveRange(entities);
               
                context.SaveChanges();
            }
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();

            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
