using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhengwei.Print
{
    public class MultDocumentPrint : IPrintable
    {
        // Fields
        private int amountPerPage;
        private List<IOutPutWithTemplate> documents;
        private bool isBreakAtFirstPage;
        private const string pageBreakHtml = "<div class='noprinting pageinterval' ></div><div class='pagebreak'><b style='height:1px;font-size:1px'>&nbsp;</b></div>";
        private string template;

        // Methods
        public MultDocumentPrint(List<IOutPutWithTemplate> doucments): this(doucments, (string) null){

    }
        public MultDocumentPrint(List<IOutPutWithTemplate> doucments, int amountPerPage)
            : this(doucments, (string)null)
        {
            this.amountPerPage = amountPerPage;
        }
        public MultDocumentPrint(List<IOutPutWithTemplate> documents, string template)
        {
            this.amountPerPage = 1;
            this.documents = documents;
            this.template = template;
        }

        public MultDocumentPrint(List<IOutPutWithTemplate> documents, string template, int amountPerPage)
        {
            this.amountPerPage = amountPerPage;
        }
        public virtual string Print()
        {
            StringBuilder builder = new StringBuilder(0x2800);
            for (int i = 0; i < this.documents.Count; i++)
            {
                if ((i == 0) && this.IsBreakAtFirstPage)
                {
                    builder.Append("<div class='noprinting pageinterval' ></div><div class='pagebreak'><b style='height:1px;font-size:1px'>&nbsp;</b></div>");
                }
                else if ((i != 0) && ((i % this.AmountPerPage) == 0))
                {
                    builder.Append("<div class='noprinting pageinterval' ></div><div class='pagebreak'><b style='height:1px;font-size:1px'>&nbsp;</b></div>");
                }
                builder.Append(this.PrintOneDocument(this.documents[i], TemplateType.Html));
            }
            return builder.ToString();
        }
        private string PrintOneDocument(IOutPutWithTemplate one, TemplateType type)
        {
            string template;
            if (this.template != null)
            {
                template = this.template;
            }
            else
            {
                template = TemplateHelper.CreateMethodForDocument(one).GetTemplate(type);
            }
            return one.OutPut(template);
        }

        public virtual string PrintToWord()
        {
            StringBuilder builder = new StringBuilder(0x2800);
            for (int i = 0; i < this.documents.Count; i++)
            {
                if ((i == 0) && this.IsBreakAtFirstPage)
                {
                    builder.Append("<div class='noprinting pageinterval' ></div><div class='pagebreak'><b style='height:1px;font-size:1px'>&nbsp;</b></div>");
                }
                else if ((i != 0) && ((i % this.AmountPerPage) == 0))
                {
                    builder.Append("<div class='noprinting pageinterval' ></div><div class='pagebreak'><b style='height:1px;font-size:1px'>&nbsp;</b></div>");
                }
                builder.Append(this.PrintOneDocument(this.documents[i], TemplateType.Doc));
            }
            return builder.ToString();
        }


        // Properties
        public int AmountPerPage { get; set; }
        public List<IOutPutWithTemplate> Documents { get; set; }
        public bool IsBreakAtFirstPage { get; set; }
        public string Template { get; set; }
    }
}
