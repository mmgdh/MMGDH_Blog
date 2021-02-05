using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMGDH_Blog.Model;
using MMGDH_Blog.Services;
using MMGDH_Blog.ViewModels;

namespace MMGDH_Blog.Controllers
{
    public class IndexController : Controller
    {
        private readonly IRepository<Article> Article_Repository;

        public IndexController(IRepository<Article> repository)
        {
            this.Article_Repository = repository;
        }
        public IActionResult Index()
        {
            List<ArticleViewModel> articleView = new List<ArticleViewModel>();
            try
            {
                IEnumerable<Article> articles = Article_Repository.GetAll();
                foreach (Article article in articles)
                {
                    ArticleViewModel articleViewModel = new ArticleViewModel();
                    articleViewModel.id = article.id;
                    articleViewModel.Title = article.Title;
                    articleViewModel.Author = article.Author;
                    articleViewModel.GetHtml = article.GetHtml;
                    articleViewModel.Synopsis = article.Synopsis;
                    articleViewModel.MarkDown = article.MarkDown;
                    articleViewModel.CreateTime = article.CreateTime;
                    articleViewModel.BelongProject = article.ProjectId.ToString();
                    articleViewModel.KnowledgePoints = article.KnowledgePoints;
                    articleViewModel.CoverImagePath = article.CoverImagePath;
                    articleViewModel.DisplayTags2 = GlobalMethod.GetTags(article.KnowledgePoints);
                    articleView.Add(articleViewModel);
                }
            }
            catch(Exception ex)
            {
                ArticleViewModel articleViewModel2 = new ArticleViewModel();
                articleViewModel2.Title = ex.Message;
                articleView.Add(articleViewModel2);
            }

            return View("BlogIndex",articleView);
        }
        [Authorize]
        public IActionResult EditArticle()
        {
            return View("../Article/EditArticle");
        }

        public IActionResult TestDemo()
        {
            return View();
        }
    }
}