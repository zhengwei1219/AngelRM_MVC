using Angel.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Angel.Web.Controllers
{
    public class TreeController : Controller
    {
        //
        // GET: /Tree/

        public ActionResult JqueryZtree()
        {
            return View();
        }
        public bool TraversingCatalog(ZTreeNode tn, string path)
        {
            if (Directory.Exists(path) == false) { return false; }
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            int allNum = dirInfo.GetDirectories().Length + dirInfo.GetFiles("*.*").Length;
            if (allNum == 0) //没有任何文件夹和文件就建立"（空白）"节点并返回false
            {
                ZTreeNode empty = new ZTreeNode();
                empty.name = "（空白）"; //得到文件名
               
                tn.children.Add(empty); //添加新节点
                return false;
            }

            //循环文件夹(避免混乱,先循环文件夹)
            int folderIndex = -1; //文件夹索引
            foreach (DirectoryInfo folder in dirInfo.GetDirectories())
            {
                folderIndex++;
                ZTreeNode folderNode = new ZTreeNode();
                folderNode.name = folder.Name; //得到文件夹名
               
                tn.children.Add(folderNode); //添加新节点
                TraversingCatalog(tn.children[folderIndex], path + "/" + folder.Name); //递归遍历其它文件夹
            }

            //循环文件
            foreach (FileInfo file in dirInfo.GetFiles("*.*")) //循环扩展名为*.*的文件
            {
                ZTreeNode fileNode = new ZTreeNode();
                fileNode.name = file.Name; //得到文件名

                tn.children.Add(fileNode); //添加新节点
            }
            return true;
        }

        public ActionResult TreeNodes(int WellboreId)
        {
            string path = @"E:\MyProject\AngelRM_MVC\Angel.Web";
            ZTreeNode Wellnode = new ZTreeNode();
            string[] pathinfo = Path.GetFullPath(path.Trim()).Split(char.Parse("\\")); //得到文件路径数组
            Wellnode.name = pathinfo[pathinfo.Length - 1]; 
            TraversingCatalog(Wellnode, path); //调用函数

            return Json(Wellnode, JsonRequestBehavior.AllowGet);
        }
    }
}
