using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zhengwei.Print
{
    public interface IHTMLPrintable
    {
        string Print(HttpRequest request);
        string PrintToWord(HttpRequest request);
    }
}
