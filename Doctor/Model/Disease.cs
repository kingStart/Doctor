using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// Disease  症状和疾病
    /// </summary>
    public class Disease
    {
        /// <summary>
        /// 伴随症状
        /// </summary>
        public List<Symptom> concomitant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string diagnosis { get; set; }

        /// <summary>
        /// 疾病列表
        /// </summary>
        public List<DiagnosisDisease> diagnosisDisease { get; set; }

        /// <summary>
        /// 系统下疾病统计
        /// </summary>
        public List<SystemDisease> systemDisease { get; set; }
        public int atime { get; set; }

    }


    public class DiagnosisDisease
    {
        public string disease { get; set; }
        public string system { get; set; }
        public string icd10 { get; set; }
        public string degree { get; set; }
        public string infectious { get; set; }
        public string department { get; set; }
        public string wmKnow { get; set; }
        public string tcmCode { get; set; }
        public string diseaseMatching { get; set; }
        public string num { get; set; }
        public string coefficient { get; set; }
        public string typical { get; set; } 
    }

    public class SystemDisease
    {
        public string disease { get; set; }
        public string system { get; set; }
        public string icd10 { get; set; }
        public string degree { get; set; }
        public string infectious { get; set; }
        public string department { get; set; }
        public string wmKnow { get; set; }
        public string tcmCode { get; set; }
        public string diseaseMatching { get; set; }
        public int num { get; set; }
        public string coefficient { get; set; }
        public bool typical { get; set; }

        public List<SystemDisease> diseaseList { get; set; }
    }
}
