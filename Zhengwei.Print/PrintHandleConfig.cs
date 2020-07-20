using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhengwei.Print
{
   public class PrintHandleConfig
    {

        // Methods
        public PrintHandleConfig()
        {
            this.OnePagehandles = new List<IHTMLPrintable>();
            this.MultiPageHandles = new List<IHTMLPrintable>();
        }

        public void AddMultiPageHandle(IHTMLPrintable handle)
        {
            this.MultiPageHandles.Add(handle);
        }

        public void AddOnePageHandle(IHTMLPrintable handle)
        {
            this.OnePagehandles.Add(handle);
        }


        // Properties
        public List<IHTMLPrintable> MultiPageHandles { get; set; }
        public List<IHTMLPrintable> OnePagehandles { get; set; }

    }
}
