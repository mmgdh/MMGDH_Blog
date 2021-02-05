using Microsoft.EntityFrameworkCore;
using MMGDH_Blog.Model;

namespace MMGDH_Blog.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
             
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<CodeBase> CodeBases { get; set; }
    }
}
