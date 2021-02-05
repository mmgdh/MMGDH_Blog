using MMGDH_Blog.Data;
using MMGDH_Blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.Services
{
    public class ArticleService : IRepository<Article>
    {
        private readonly DataContext _context;
        public ArticleService(DataContext context)
        {
            _context = context;
        }
        public Article Add(Article newModel)
        {
            _context.Articles.Add(newModel);
            _context.SaveChanges();
            return newModel;
        }

        public IEnumerable<Article> GetAll()
        {
            return _context.Articles.ToList().OrderByDescending(x => x.CreateTime);
        }

        public IEnumerable<Article> GetByCondition(object id)
        {
            throw new NotImplementedException();
        }

        public Article GetById(int id)
        {
            return _context.Articles.Find(id);
        }

        public int GetMaxID()
        {
            throw new NotImplementedException();
            
        }

        public Article Update(Article newModel)
        {
            _context.Articles.Update(newModel);
            _context.SaveChanges();
            return newModel;
        }
    }
}
