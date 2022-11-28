using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class RepositoryBase<TId, TEntity> : IRepositoryBase<TId, TEntity>
    {
        protected readonly IDbConnection _connection;

        public RepositoryBase(IDbConnection connection)
        {
            _connection = connection;
        }

        protected abstract TEntity Mapper(IDataRecord record);
        public abstract int Add(TEntity entity);
        public abstract bool Delete(TId id);
        public abstract IEnumerable<TEntity> GetAll();
        public abstract TEntity? GetById(TId id);
        public abstract bool Update(TEntity entity);
    }
}
