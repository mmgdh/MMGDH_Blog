using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MMGDH_Blog.Model;
using MMGDH_Blog.Services;
using MMGDH_Blog.ViewModels;

namespace MMGDH_Blog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IRepository<Article> Article_Repository;
        private readonly IRepository<CodeBase> Codebase_service;
        private readonly IRepository<Project> Project_service;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;

        public ArticleController(IRepository<CodeBase> codebase, IRepository<Project> project, IRepository<Article> repository, IConfiguration _configuration,UserManager<IdentityUser> userManager)
        {
            this.Article_Repository = repository;
            this.configuration = _configuration;
            this.Project_service = project;
            this.Codebase_service = codebase;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View("BlogIndex");
        }
        [Authorize]
        public IActionResult EditArticle()
        {
            ArticleViewModel viewModel = new ArticleViewModel();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<Project> projects = Project_service.GetAll();
            foreach (Project model in projects)
            {
                selectListItems.Add(new SelectListItem { Value = model.ProjectId.ToString(), Text = model.ProjectName });
            }
            viewModel.projects = selectListItems;
            viewModel.DisplayTags = Codebase_service.GetByCondition((object)"Tags").ToList();
            return View(viewModel);
        }

        //public string AddArticle()
        //{
        //    Article article = new Article();
        //    article.Title = Request.Form["Title"];
        //    article.Author = Request.Form["Author"];
        //    article.GetHtml = Request.Form["getHTML"];
        //    article.MarkDown = Request.Form["MarkDown"];
        //    article.Synopsis = Request.Form["Synopsis"];
        //    article.KnowledgePoints = Request.Form["Type"];
        //    article.ProjectId = int.Parse(Request.Form["BelongProject"]);
        //    article.CreateTime = DateTime.Now;

        //    Article_Repository.Add(article);
        //    return "ok";
        //}

        public IActionResult ArticleDetail(int id)
        {
            var Article = Article_Repository.GetById(id);
            if (Article == null)
            {
                return RedirectToAction("Index");
            }
            ArticleViewModel articleViewModel = new ArticleViewModel();
            articleViewModel.id = Article.id;
            articleViewModel.Title = Article.Title;
            articleViewModel.Author = Article.Author;
            articleViewModel.GetHtml = Article.GetHtml;
            articleViewModel.MarkDown = Article.MarkDown;
            articleViewModel.CreateTime = Article.CreateTime;
            articleViewModel.BelongProject = Article.ProjectId.ToString();
            articleViewModel.KnowledgePoints = Article.KnowledgePoints;
            return View(articleViewModel);
        }

        public JsonResult UpImage(long? id)//id传过来，如需保存可以备用
        {
            int success = 0;
            string msg = "";
            string pathNew = "";
            try
            {
                var date = Request;
                var files = Request.Form.Files;
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        string fileExt = formFile.FileName.Substring(formFile.FileName.LastIndexOf(".") + 1, (formFile.FileName.Length - formFile.FileName.LastIndexOf(".") - 1)); //扩展名
                        long fileSize = formFile.Length; //获得文件大小，以字节为单位
                        string md5 = GlobalMethod.GenerateMD5(formFile.OpenReadStream());
                        string newFileName = md5 + "." + fileExt; //MD5加密生成文件名保证文件不会重复上传
                        var pathStart = configuration["Location:ArticleImage"] + "/images/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                        if (System.IO.Directory.Exists(pathStart) == false)//如果不存在新建
                        {
                            System.IO.Directory.CreateDirectory(pathStart);
                        }
                        var filePath = pathStart + newFileName;
                        pathNew = filePath.Replace(configuration["Location:ArticleImage"], "");
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {

                            formFile.CopyTo(stream);
                            success = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = 0;
                msg = ex.ToString();
            }
            var obj = new { success = success, url = pathNew, message = msg };
            return Json(obj);
        }


        public string AddArticle()
        {
            var date = Request;
            string outMessage = "";
            string pathNew = "";
            
            try
            {
                var files = Request.Form.Files;
                Article article = new Article();
                article.Title = Request.Form["Title"];
                article.Author = "MMGDH";
                article.GetHtml = Request.Form["getHTML"];
                article.MarkDown = Request.Form["MarkDown"];
                article.Synopsis = Request.Form["Synopsis"];
                article.KnowledgePoints = Request.Form["KnowledgePoints"];
                article.ProjectId = int.Parse(Request.Form["BelongProject"]);
                article.CreateTime = DateTime.Now;
                Article NewArticle = Article_Repository.Add(article);
                pathNew = article.CoverImagePath = saveImage(files, Request.Form["BelongProject"], NewArticle.id.ToString(),out outMessage);
                article.CoverImagePath = pathNew;
                Article_Repository.Update(article);
            }
            catch(Exception ex)
            {
                return outMessage+ex.ToString();
            }
            return outMessage;
        }
        private string saveImage(IFormFileCollection files, string ProjectId,string AriticleId, out string message)
        {
            string pathNew = "";
            message = "";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = formFile.FileName.Substring(formFile.FileName.LastIndexOf(".") + 1, (formFile.FileName.Length - formFile.FileName.LastIndexOf(".") - 1)); //扩展名
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string md5 = GlobalMethod.GenerateMD5(formFile.OpenReadStream());
                    string newFileName = md5 + "." + fileExt; //MD5加密生成文件名保证文件不会重复上传
                    var pathStart = configuration["Location:ArticleImage"] + "/images/Project" + ProjectId + "/Article"+ AriticleId+"/" ;
                    if (!Directory.Exists(pathStart))//如果不存在新建
                    {
                        message += "新建pathStart：" + pathStart;
                        Directory.CreateDirectory(pathStart);
                        message += "未跳出";
                    }
                    else
                    {
                        message += "未新建pathStart："+ pathStart;
                    }
                    var filePath = pathStart + newFileName;
                    pathNew = filePath.Replace(configuration["Location:ArticleImage"], "");
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        message += "进入copy";
                        formFile.CopyTo(stream);
                    }
                    message += "未进入copy";
                }
            }
            message += "Over";
            return pathNew;
        }

    }
}