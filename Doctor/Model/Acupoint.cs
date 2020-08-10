using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// 穴位
    /// </summary>
    public class Acupoint
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// <summary>
        /// 中成药图片 URL
        /// </summary>
        public string img { get; set; }


        /// <summary>
        /// 否被收藏，和用户 的微信 OpenID 关联， 1 为收藏，0 为未收藏
        /// </summary>
        public string isstar { get; set; }

        /// <summary>
        /// 总页数，类型 16 时返 回
        /// </summary>
        public int pagecount { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public List<SubAcupoint> items { get; set; }


    }

    public class SubAcupoint
    {
        public string desc { get; set; }
    }
}
