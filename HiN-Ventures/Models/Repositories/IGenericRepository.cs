using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiN_Ventures.Models.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        TEntity GetById(object id);

    }
}
