using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// StoreTreeAcuherb  症候接口返回的数据模型
    /// </summary>
    public class StoreTreeAcuherb
    {
        /// <summary>
        /// 病种独立 ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 病种名称
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 病种编号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string explain { get; set; }

        /// <summary>
        /// 西医别名
        /// </summary>
        public string wd { get; set; }

        /// <summary>
        /// 是否被收藏，和用户的 微信 OpenID 关联，1 为 收藏，0 为未收藏
        /// </summary>
        public int isstar { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 暂时未知
        /// </summary>
        public string istype { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// 症候列表子集
        /// </summary>
        public List<SubStoreTreeAcuherb> items { get; set; }

        /// <summary>
        /// 上次症候选择值
        /// </summary>
        public string listSymp { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public string trackId { get; set; }
    }

    public class SubStoreTreeAcuherb
    {
        /// <summary>
        /// 症候独立 ID，默认是数 组顺号
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 是否选中标志，1 为选 中，0 为未选中，默认 0
        /// </summary>
        public int value { get; set; }

        /// <summary>
        /// 症候名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 症候解释
        /// </summary>
        public string explain { get; set; }

        /// <summary>
        /// 症候分组，如果相同则 为同一组症候
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 舌诊图片编号子集 舌诊图片地址调用地 址：/lungo/CT/[舌诊编 号].jpg，多个图片的话 用英文逗号分隔，例如： T04,T05
        /// </summary>
        public string ct { get; set; }

        /// <summary>
        /// 一步按钮的文字替换 内容，程序默认下一步 按钮的文字为“无此症 候”，如果这个字段的内 容不为空，则将下一步 按钮的内容替换成这个 节点的文本内容
        /// </summary>
        public string nextbtn { get; set; }
    }
}
