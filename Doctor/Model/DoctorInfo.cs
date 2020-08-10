using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// DoctorInfo  医生信息
    /// </summary>
    public class DoctorInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string areaCodeCount { get; set; }

        /// <summary>
        /// 医生姓名
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// 医生id
        /// </summary>
        public string doctorid { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sourceId { get; set; }
    }
}
