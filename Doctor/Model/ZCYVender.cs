using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// ZCYVender  中成药（药厂）详情
    /// </summary>
    public class ZCYVender
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
        /// 药厂名称
        /// </summary>
        public string vender { get; set; }

        /// <summary>
        /// 中成药图片 URL
        /// </summary>
        public string img { get; set; }
    }
}
