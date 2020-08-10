using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// WebSocketResult  WebSocketResult数据模型
    /// </summary>
    public class WebSocketResult
    {
        /// <summary>
        /// 具体的数据
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public int dateTime { get; set; }

        /// <summary>
        /// 跳转的页面标识 
        /// 诊断页面 disease_list 
        /// 错误诊断页面 undisease_list 
        /// 用户信息页 patient_info 
        /// 医生信息页 doctor_info 
        /// 错误token页 un_token
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 传染
        /// </summary>
        public string infectious { get; set; }
    }
}
