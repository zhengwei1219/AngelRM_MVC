using Angel.BLL;
using Angel.Model;
using Angel.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zhengwei.Print;

namespace Angel.Web.Print
{
    public class ZhengweiPrintHandle : OnePagePrintHandleBase
    {
         [Inject]
        public IDataService _queryService=new BLLService();
         
         public ZhengweiPrintHandle()
             : base()
         {
             
         }
        private string templateName;
        protected override IOutPutWithTemplate GetDocument(HttpRequest request)
        {
            IOutPutWithTemplate one = null;
            try
            {
                //根据type确定打印哪个类，根据docID获取打印的类
                string type = GetDocType(request);
                int docID = GetDocID(request);
                type = "user";
                switch (type)
                {
                    case "user":
                        UserPrint user = new UserPrint();
                        user.userName = "zhengwei";
                        one = user;
                        break;
                }
     
                return one;
                 
                
            }
            catch (Exception ep)
            {
                throw new Exception("打印文件出错，原因：" + ep.Message);
            }
        }
    }
}