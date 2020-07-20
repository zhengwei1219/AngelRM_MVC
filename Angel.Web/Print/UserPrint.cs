using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Zhengwei.Print;

namespace Angel.Web.Print
{
    public class UserPrint : IOutPutWithTemplate
    {
        public string userName { get; set; }

        public string GetTemplateName()
        {
            return "user";
        }

        public string OutPut(string template)
        {
            StringBuilder builder = new StringBuilder(template);
            builder.Replace("{用户名}", this.userName);
            return builder.ToString();
        }
    }
}