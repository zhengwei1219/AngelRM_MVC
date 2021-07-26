using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angel.Web.ViewModel
{
   
        /// <summary>
        /// Ztree节点 类 注意节点名称是有大小写敏感
        /// </summary>
        public class ZTreeNode
        {
            public ZTreeNode()
            {
                this.open = true;
                this.children = new List<ZTreeNode>();
            }
            public ZTreeNode(int Id, string Name, int ParentId, string Type)
            {
                this.open = true;
                this.children = new List<ZTreeNode>();
                this.Id = Id;
                this.name = Name;
                this.ParentId = ParentId;
                this.type = Type;
                Checked = false;
            }
            /// <summary>
            /// 记录标识符
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// （节点显示）名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 项目类型代码
            /// </summary>
            public string ProjectCode { get; set; }
            /// <summary>
            /// 节点类型(项目/数据/作业/...)
            /// </summary>
            public string type { get; set; }
            /// <summary>
            /// 父节点ID
            /// </summary>
            public int ParentId { get; set; }
            /// <summary>
            /// 路径
            /// </summary>
            public string Url { get; set; }
            /// <summary>
            /// 节点是否展开
            /// </summary>
            public bool open { get; set; }
            /// <summary>
            /// 是否有添加权限
            /// </summary> 
            public bool CanAdd = true;
            /// <summary>
            /// 是否待新增（及没有记录）
            /// </summary>
            public bool IsNew
            {
                get
                {
                    return this.Id == -1;
                }
                set { }
            }
            /// <summary>
            /// 是否选中
            /// </summary>
            public bool Checked { get; set; }
            /// <summary>
            /// 子节点
            /// </summary>
            public List<ZTreeNode> children { get; set; }

            /// <summary>
            /// 节点名称
            /// </summary>
            public string NodeName { get; set; }
            /// <summary>
            /// 待提交 
            /// </summary>
            public int UnsubmitCount { get; set; }
            /// <summary>
            /// 待审核
            /// </summary>
            public int NeedappCount { get; set; }
            /// <summary>
            /// 审核失败
            /// </summary>
            public int appfailedCount { get; set; }
            /// <summary>
            /// 自定义节点名称
            /// </summary>
            public string FullName { get; set; }
            /// <summary>
            /// 预览时总数据条数
            /// </summary>
            public int ViewDataCount { get; set; }
            /// <summary>
            /// 辅助字段，用于排序测井项目组合（施工开始时间）
            /// </summary>
            public DateTime? OrderRule { get; set; }
        }
    }
