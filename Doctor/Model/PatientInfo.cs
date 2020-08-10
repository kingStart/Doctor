using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// 患者信息
    /// </summary>
    public class PatientInfo
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 诊断id
        /// </summary>
        public string outpatientId { get; set; }

        /// <summary>
        /// -患者唯一id
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// -患者性别- 0：未知的性别 * 1：男性 * 2：女性 * 9：未说明的性别
        /// </summary>
        public string sexCode { get; set; }

        /// <summary>
        /// 症状（主诉分词）
        /// </summary>
        public string symptom { get; set; }

        /// <summary>
        /// 医生UID
        /// </summary>
        public string doctorUid { get; set; }

        /// <summary>
        /// 患者UID
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 患者身份证
        /// </summary>
        public string cardNo { get; set; }
    }
}
