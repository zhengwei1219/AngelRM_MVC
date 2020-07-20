using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zhengwei.Print
{
   public class TemplateHelper
    {
       public TemplateHelper() { }
       public static IGetTemplateMethod CreateMethodForDocument(IOutPutWithTemplate document)
       {
           if (!string.IsNullOrEmpty(document.GetTemplateName()))
           {
               return new GetTemplateByInstance(document);
           }
           return new GetTemplateFromType(document.GetType());
       }
       public static string GetTemplateFromFile(string fileName)
       {
           return ReadTemplateFromFile(HttpContext.Current.Server.MapPath("../Template/" + fileName));
       }
        private static string ReadTemplateFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("模板文件" + fileName + "不存在");
            }
            StringBuilder builder = new StringBuilder();
            using (StreamReader reader = File.OpenText(fileName))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    builder.AppendLine(str);
                }
                reader.Close();
            }
            return builder.ToString();
        }
    }
}
