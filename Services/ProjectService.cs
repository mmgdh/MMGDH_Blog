using MMGDH_Blog.Data;
using MMGDH_Blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.Services
{
    public class ProjectService : IRepository<Project>
    {
        private readonly DataContext _context;
        public ProjectService(DataContext context)
        {
            _context = context;
        }
        public Project Add(Project newModel)
        {
            _context.Projects.Add(newModel);
            _context.SaveChanges();
            return(newModel) ;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public IEnumerable<Project> GetByCondition(object condition)
        {
            throw new NotImplementedException();
        }

        public Project GetById(int id)
        {
            return _context.Projects.Find(id);
        }

        public int GetMaxID()
        {
            try
            {
                return _context.Projects.Max(x => x.ProjectId) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public Project Update(Project newModel)
        {
            _context.Update(newModel);
            _context.SaveChanges();
            return newModel;
        }
    }
}
