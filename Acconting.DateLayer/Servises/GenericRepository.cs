using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;


namespace Acconting.DateLayer.Servises
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private AccontingEntities _db;
        private DbSet<TEntity> _dbset;
        public GenericRepository(AccontingEntities Context)
        {
            _db = Context;
            _dbset = _db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity,bool>>where = null)
        {
            IQueryable<TEntity> query = _dbset;
            if(where != null)
            {
                query = query.Where(where);
            }
            return query.ToList();
        }

        public virtual TEntity GetByID(object ID)
        {
            return _dbset.Find(ID);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }


        public virtual void Update(TEntity entity)
        {
            
            //if (_db.Entry(entity).State == EntityState.Detached)
            //{
            //    _dbset.Attach(entity);
                
            //}
            _db.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            if(_db.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
                
            }
            _dbset.Remove(entity);
        }

        public virtual void Delete(object ID)
        {
            var resualt = GetByID(ID);
            Delete(resualt);
        }
    }
}
