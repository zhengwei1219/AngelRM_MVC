using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Xml;

namespace Zhengwei.Print
{
    public class PrintConfigHandle : IConfigurationSectionHandler
    {
        // Methods
        public PrintConfigHandle() { }
        public object Create(object parent, object configContext, XmlNode section)
        {
            PrintHandleConfig config = new PrintHandleConfig();
            foreach (XmlNode node in section.ChildNodes)
            {
                if (!node.Name.Equals("onePageHandles", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!node.Name.Equals("multiPageHandles", StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new Exception("illegal node " + node.Name);
                    }
                    foreach (XmlNode node3 in node.ChildNodes)
                    {
                        if (node3.Name.Equals("handle", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string innerText = node3.InnerText;
                            if (!string.IsNullOrEmpty(innerText))
                            {
                                Type type2 = Type.GetType(innerText);
                                if (type2 == null)
                                {
                                    type2 = BuildManager.GetType(innerText, true);
                                }
                                config.AddMultiPageHandle((IHTMLPrintable)Activator.CreateInstance(type2));
                            }
                        }
                    }
                }
                else
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.Equals("handle", StringComparison.InvariantCultureIgnoreCase))
                        {
                            string str = node2.InnerText;
                            if (!string.IsNullOrEmpty(str))
                            {
                                Type type = Type.GetType(str);
                                if (type == null)
                                {
                                    type = BuildManager.GetType(str, true);
                                }
                                config.AddOnePageHandle((IHTMLPrintable)Activator.CreateInstance(type));
                            }
                        }
                    }
                }
            }
            return config;
        }

 

    }

 

}
