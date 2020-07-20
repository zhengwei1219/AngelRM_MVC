using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhengwei.Print
{
    public class GetTemplateFromType : IGetTemplateMethod
    {
        // Fields
        private Type classType;

        // Methods
        public GetTemplateFromType(Type type)
        {
            this.classType = type;
        }
        public string GetTemplate(TemplateType type)
        {
            string name = this.classType.Name;
            return TemplateHelper.GetTemplateFromFile(string.Format("{0}.{1}", name, type.ToString()));
        }
    }
}
