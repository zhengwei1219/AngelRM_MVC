using Angel.BLL;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zhengwei.Print;

namespace Angel.Web.Controllers
{
    public class DocumentPrintController : Controller
    {
        //
        // GET: /DocumentPrint/
        [Inject]
        public IDataService QueryService { get; set; }
        public ActionResult Index()
        {

            string result = string.Empty;
            PrintHandleConfig config = (PrintHandleConfig)System.Configuration.ConfigurationManager.GetSection("printConfig");
            for (int i = 0; i < config.OnePagehandles.Count; i++)
            {
                result = config.OnePagehandles[i].Print(System.Web.HttpContext.Current.Request);
                var asdsd = System.Web.HttpContext.Current.Request.Params["id"];
                if (!string.IsNullOrEmpty(result))
                    break;
            }
            ViewData["pPrintContent"] = result;
            return View();
        }

        public ActionResult PrintHelp()
        {
            return View();
        }

    }
}
