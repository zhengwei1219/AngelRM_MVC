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
                string type = GetDocType(request);
                templateName = "user";

                int userid = GetDocID(request);
                string id = request["id"];
                _queryService.GetData(new JObject(), "");
                        UserPrint user = new UserPrint();
                        user.userName = "zhengwei";
                return user;
                 
                
            }
            catch (Exception ep)
            {
                //one = new NoRightPrintError();
            }
            return one;
        }
    }
}