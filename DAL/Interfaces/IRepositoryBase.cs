using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryBase<TId, TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(TId id);
        int Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TId id);

    }
}
