using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// 症状
    /// </summary>
    public class Symptom
    {
        /// <summary>
        /// 症状名称
        /// </summary>
        public string symptom { get; set; }

        /// <summary>
        /// 症状编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 症状描述
        /// </summary>
        public string describe { get; set; }

        /// <summary>
        /// 症状部位
        /// </summary>
        public string parts { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        public string initial { get; set; }

        /// <summary>
        /// 急症标志
        /// </summary>
        public string infectious { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int frequency { get; set; }

        /// <summary>
        /// 男性/女性
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string infantSymptom { get; set; }

        /// <summary>
        /// 中医对应的编码
        /// </summary>
        public string tcmCode { get; set; }

        /// <summary>
        /// 口语化名称
        /// </summary>
        public string colloquialName { get; set; }


    }
}
