using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhengwei.Print
{
   public class GetTemplateByInstance: IGetTemplateMethod
    {
        private IOutPutWithTemplate instance;

        // Methods
        public GetTemplateByInstance(IOutPutWithTemplate instance){
            this.instance = instance;
        }
        public string GetTemplate(TemplateType type)
        {
             string templateName = this.instance.GetTemplateName();
    return TemplateHelper.GetTemplateFromFile(string.Format("{0}.{1}", templateName, type.ToString()));
}


        }
    }

