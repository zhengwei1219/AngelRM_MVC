using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zhengwei.Print
{
    public abstract class OnePagePrintHandleBase : IHTMLPrintable
    {
        // Methods
        public OnePagePrintHandleBase() { }
        protected virtual int GetDocID(HttpRequest request)
        {
            int num;
            if (int.TryParse(request.Params["id"], out num))
            {
                return num;
            }
            return 0;
        }

        protected virtual string GetDocType(HttpRequest request)
        {
            return request.Params["type"];
        }

        protected abstract IOutPutWithTemplate GetDocument(HttpRequest request);
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
            IOutPutWithTemplate document = this.GetDocument(request);
            if (document == null)
            {
                return null;
            }
            IPrintable printable = new DocumentPrint(document);
            return printable.Print();
        }

        public virtual string PrintToWord(HttpRequest request)
        {
            IOutPutWithTemplate document = this.GetDocument(request);
            if (document == null)
            {
                return null;
            }
            IPrintable printable = new DocumentPrint(document);
            return printable.PrintToWord();
        }

    }

 

}
