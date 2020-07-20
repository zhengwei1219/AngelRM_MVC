using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhengwei.Print
{
   public interface IGetTemplateMethod
    {
        // Methods
        string GetTemplate(TemplateType type);
    }
}
