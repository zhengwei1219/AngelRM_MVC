using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zhengwei.Print
{
    public abstract class MultiPagePrintHandleBase : IHTMLPrintable
    {
        // Methods
        public MultiPagePrintHandleBase() { }
        protected abstract int GetAmountPerPage(HttpRequest request);
        protected virtual string[] GetDocIDArray(HttpRequest request)
        {
            string str = request.Params["ids"];
            char[] separator = ",|".ToCharArray();
            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        protected virtual string GetDocType(HttpRequest request)
        {
            return request.Params["type"];
        }

        protected abstract List<IOutPutWithTemplate> GetDocuments(HttpRequest request);
        protected string GetStringPara(HttpRequest request, string name, string defaultValue)
        {
            if (!string.IsNullOrEmpty(request.Params[name]))
            {
                return request.Params[name];
            }
            return defaultValue;
        }

        public virtual string Print(HttpRequest request)
        {
            IPrintable printable = new MultDocumentPrint(this.GetDocuments(request), this.GetAmountPerPage(request));
            return printable.Print();
        }

        public virtual string PrintToWord(HttpRequest request)
        {
            IPrintable printable = new MultDocumentPrint(this.GetDocuments(request), this.GetAmountPerPage(request));
            return printable.PrintToWord();
        }

    }

 

}
