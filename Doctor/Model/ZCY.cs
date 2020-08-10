using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// ZCY  中成药详情
    /// </summary>
    public class ZCY
    {
        /// <summary>
        /// 中成药编号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 中成药名称
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 类型，默认：中成药
        /// </summary>
        public string istype { get; set; }

        /// <summary>
        /// 类别，OTC，RX
        /// </summary>
        public string isclass { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string guige { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double price { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 药厂名称
        /// </summary>
        public string vender { get; set; }

        /// <summary>
        /// 成分
        /// </summary>
        public string chengfen { get; set; }

        /// <summary>
        /// 主治
        /// </summary>
        public string zhuzhi { get; set; }

        /// <summary>
        /// 说明书、具体说明
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 中成药图片 URL
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// 是否被收藏，和用 户的微信 OpenID 关联，1 为收藏，0 为未收藏
        /// </summary>
        public string isstar { get; set; }
    }
}
