using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhengwei.Print
{
    public class DocumentPrint : IPrintable
    {


        // Fields
        private IOutPutWithTemplate document;
        private bool isPrintDetail;
        private string template;


       public DocumentPrint(IOutPutWithTemplate document)
           : this(document, null)
       {

       }
        public DocumentPrint(IOutPutWithTemplate document, string template)
       {
           this.document = document;
           this.Template = template;
       }
        string IPrintable.Print()
        {
            return this.document.OutPut(this.Template);
        }
        public string PrintToWord()
        {
            string template = TemplateHelper.CreateMethodForDocument(this.document).GetTemplate(TemplateType.Doc);
            return this.document.OutPut(template);
        }

        // Properties
        public bool IsPrintDetail
        {
            get
            {
                return this.isPrintDetail;
            }
            set
            {
                this.isPrintDetail = value;
            }
        }
        public string Template
        {
            get
            {
                if (this.template == null)
                {
                    this.template = TemplateHelper.CreateMethodForDocument(this.document).GetTemplate(TemplateType.Html);
                }
                return this.template;
            }
            set
            {
                this.template = value;
            }
        }
    }
}
