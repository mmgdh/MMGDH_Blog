using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MMGDH_Blog.Model;
using MMGDH_Blog.Services;
using MMGDH_Blog.ViewModels;

namespace MMGDH_Blog.Controllers
{

    public class BaseInfoController : Controller
    {
        private readonly IRepository<CodeBase> Codebase_service;
        private readonly IRepository<Project> Project_service;
        private readonly IConfiguration configuration;
        public BaseInfoController(IRepository<CodeBase> codebase,IRepository<Project> project, IConfiguration _configuration)
        {
            Codebase_service = codebase;
            Project_service = project;
            configuration = _configuration;
        }
        public IActionResult AddBaseInfo()
        {
            BaseInfoModel model = new BaseInfoModel
            {
                DisplayTags = Codebase_service.GetByCondition((object)"Tags").ToList()
            };
            if (model.DisplayTags == null)
            {
                model.DisplayTags.Add(new CodeBase
                {
                    Remark = "无数据",
                    Type = ""
                });
            }
            return View(model);
        }

        [HttpPost]
        public string AddProject()
        {
            #region 转换Tag--已注释
            //string Tags = "";
            //foreach(char a in Request.Form["Tags"].ToString())
            //{
            //    if (a != '[')
            //    {
            //        if (a == ']')
            //        {
            //            Tags+=",";
            //        }
            //        else
            //        {
            //            Tags += a;
            //        }

            //    }
            //} 
            #endregion
            try
            {
                var files = Request.Form.Files;
                Project project = new Project
                {
                    //ProjectId = Project_service.GetMaxID(),
                    ProjectName = Request.Form["ProjectName"],
                    Description = Request.Form["Description"],
                    StartDate = DateTime.Now,
                    Tags = Request.Form["Tags"]
                };
                Project NewProject = Project_service.Add(project);
                NewProject.CoverImage = saveImage(files, NewProject.ProjectId.ToString());
                Project_service.Update(NewProject);
            }
            catch(Exception ex)
            {
                return "defeated";
            }

            return "ok";
        }
        public string AddCodeBase()
        {
            string Type = "";
            switch (Request.Form["Type"])
            {
                case "0":
                    Type = "Tags";
                    break;
            }
            if (Type == "")
            {
                return "defeated";
            }
            CodeBase codeBase = new CodeBase
            {
                Remark = Request.Form["Remark"],
                Type = Type
            };
            Codebase_service.Add(codeBase);
            return "ok";
        }

        private string saveImage(IFormFileCollection files, string ProjectId)
        {
            string pathNew = "";
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = formFile.FileName.Substring(formFile.FileName.LastIndexOf(".") + 1, (formFile.FileName.Length - formFile.FileName.LastIndexOf(".") - 1)); //扩展名
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string md5 = GlobalMethod.GenerateMD5(formFile.OpenReadStream());
                    string newFileName = md5 + "." + fileExt; //MD5加密生成文件名保证文件不会重复上传
                    var pathStart = configuration["Location:ArticleImage"] + "/images/Project" + ProjectId + "/";
                    if (System.IO.Directory.Exists(pathStart) == false)//如果不存在新建
                    {
                        System.IO.Directory.CreateDirectory(pathStart);
                    }
                    var filePath = pathStart + newFileName;
                    pathNew = filePath.Replace(configuration["Location:ArticleImage"], "");
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
            }
            return pathNew;
        }
    }
}