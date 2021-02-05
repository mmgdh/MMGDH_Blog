using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.Services
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByCondition(object condition);
        T GetById(int id);
        T Add(T newModel);
        T Update(T newModel);
        int GetMaxID();

    }
}
