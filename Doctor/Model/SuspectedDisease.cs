using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    public class SuspectedDisease
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public PatientInfo patient { get; set; }

        /// <summary>
        /// 诊断信息
        /// </summary>
        public List<SuspectedDiseaseDetail> wmDiseaseDetailSocketParams { get; set; }

    }



    /// <summary>
    /// 疑似病例  诊疗列表
    /// </summary>
    public class SuspectedDiseaseDetail
    {
        /// <summary>
        /// 程度
        /// </summary>
        public string degree { get; set; }

        /// <summary>
        /// 程度
        /// </summary>
        public string degreeJi { get; set; }

        /// <summary>
        /// 程度
        /// </summary>
        public string degreeWei { get; set; }

        /// <summary>
        /// 程度
        /// </summary>
        public string degreeZ { get; set; }

        /// <summary>
        /// 疾病名称
        /// </summary>
        public string disease { get; set; }

        /// <summary>
        /// 疾病匹配度
        /// </summary>
        public string diseaseMatching { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 传染
        /// </summary>
        public string infectious { get; set; }

        public string IsShowIcd { get; set; }
    }
}
