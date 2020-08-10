using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// 患者体征数据
    /// </summary>
    public class HealthData
    {
        /// <summary>
        /// 门诊流水号
        /// </summary>
        public string outpatientId { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 血糖
        /// </summary>
        public string bloodGlucose { get; set; }

        /// <summary>
        /// 血氧
        /// </summary>
        public string bloodOxygen { get; set; }

        /// <summary>
        /// 舒张压(mmHg)
        /// </summary>
        public string diastolicPressure { get; set; }

        /// <summary>
        /// 心率（次/分）
        /// </summary>
        public double heartRate { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public string height { get; set; }

        /// <summary>
        /// 平均压
        /// </summary>
        public string meanPressure { get; set; }

        /// <summary>
        /// 脉搏(次/min)
        /// </summary>
        public string pulse { get; set; }

        /// <summary>
        /// 呼吸频率(次/min)
        /// </summary>
        public string respiratoryRate { get; set; }

        /// <summary>
        /// 收缩压(mmHg)
        /// </summary>
        public string systolicPressure { get; set; }

        /// <summary>
        /// 体温
        /// </summary>
        public string temperature { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string weight { get; set; }
    }
}
