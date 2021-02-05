using MMGDH_Blog.Data;
using MMGDH_Blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.Services
{
    public class CodeBaseService : IRepository<CodeBase>
    {
        public readonly DataContext _context;
        public CodeBaseService(DataContext context)
        {
            _context = context;
        }
        public CodeBase Add(CodeBase newModel)
        {
            _context.CodeBases.Add(newModel);
            _context.SaveChanges();
            return newModel;
        }

        public IEnumerable<CodeBase> GetAll()
        {
            return _context.CodeBases.ToList();
        }

        public IEnumerable<CodeBase> GetByCondition(object Type)
        {
            return _context.CodeBases.Where(x => x.Type == Type.ToString()).ToList();
        }

        public CodeBase GetById(int id)
        {
            return _context.CodeBases.Find(id);
        }

        public int GetMaxID()
        {
            throw new NotImplementedException();
        }

        public CodeBase Update(CodeBase newModel)
        {
            throw new NotImplementedException();
        }
    }
}
