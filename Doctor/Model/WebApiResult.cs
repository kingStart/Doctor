using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// WebApiResult  WebApi请求结果返回的数据模型
    /// </summary>
    public class WebApiResult
    {
        /// <summary>
        /// 具体的数据
        /// </summary>
        public object data{ get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 结果表示  SUCCESS、FALSE
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long timestamp { get; set; }

        public string requestId { get; set; }
        public object apiCondition { get; set; }

    }
}
