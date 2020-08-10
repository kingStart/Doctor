using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// 用户详情
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// uid
        /// </summary>
        public string doctorUid { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sourceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string areaCodeCount { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 医生性别
        /// </summary>
        public double doctorSex { get; set; }

        /// <summary>
        /// 医生token
        /// </summary>
        public string doctorToken { get; set; }
    }
}
