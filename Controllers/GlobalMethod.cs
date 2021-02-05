
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MMGDH_Blog.Controllers
{
    public class GlobalMethod
    {
        private  readonly IConfiguration configuration;
        public  GlobalMethod(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public static string GenerateMD5(Stream inputStream)
        {
            using (MD5 mi = MD5.Create())
            {
                //开始加密
                byte[] newBuffer = mi.ComputeHash(inputStream);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public static List<String> GetTags(string Tags)
        {
            if (Tags == null)
            {
                return null;
            }
            List<string> lstTags = new List<string>();
            string Tag = "";
            foreach(char a in Tags)
            {
                if (a != '[')
                {
                    if (a == ']')
                    {
                        lstTags.Add(Tag);
                        Tag = "";
                        continue;
                    }
                    Tag += a;
                }
            }
            return lstTags;
        }

        /// <summary>
        /// 复制文件夹及文件
        /// </summary>
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        /// <returns></returns>
        public int CopyFolder2(string sourceFolder, string destFolder)
        {
            try
            {
                string folderName = System.IO.Path.GetFileName(sourceFolder);
                string destfolderdir = System.IO.Path.Combine(destFolder, folderName);
                string[] filenames = System.IO.Directory.GetFileSystemEntries(sourceFolder);
                foreach (string file in filenames)// 遍历所有的文件和目录
                {
                    if (System.IO.Directory.Exists(file))
                    {
                        string currentdir = System.IO.Path.Combine(destfolderdir, System.IO.Path.GetFileName(file));
                        if (!System.IO.Directory.Exists(currentdir))
                        {
                            System.IO.Directory.CreateDirectory(currentdir);
                        }
                        CopyFolder2(file, destfolderdir);
                    }
                    else
                    {
                        string srcfileName = System.IO.Path.Combine(destfolderdir, System.IO.Path.GetFileName(file));
                        if (!System.IO.Directory.Exists(destfolderdir))
                        {
                            System.IO.Directory.CreateDirectory(destfolderdir);
                        }
                        System.IO.File.Copy(file, srcfileName);
                    }
                }

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }


    }
}
