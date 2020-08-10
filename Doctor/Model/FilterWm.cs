using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// 过滤西医症状和疾病
    /// </summary>
    public class FilterWm
    {
        /// <summary>
        /// 中医编码
        /// </summary>
        public string tcmCode { get; set; }

        /// <summary>
        /// 中医名称
        /// </summary>
        public string tcmName { get; set; }

        /// <summary>
        /// 西医疾病编码（ICD10）
        /// </summary>
        public string wmDiseaseCode { get; set; }

        /// <summary>
        /// 西医症状
        /// </summary>
        public string wmSymptomCode { get; set; }

    }
}
