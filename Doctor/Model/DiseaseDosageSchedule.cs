using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// 疾病方案
    /// </summary>
    public class DiseaseDosageSchedule
    {
        /// <summary>
        /// 临床特点
        /// </summary>
        public string clinicalManifestation { get; set; }

        /// <summary>
        /// 并发症
        /// </summary>
        public string complication { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string department { get; set; }

        /// <summary>
        /// 鉴别诊断
        /// </summary>
        public string differentialDiagnosis { get; set; }

        /// <summary>
        /// 疾病诊断
        /// </summary>
        public string diseaseDiagnosis { get; set; }

        /// <summary>
        /// 疾病概述
        /// </summary>
        public string diseasesummary { get; set; }

        /// <summary>
        /// 病因
        /// </summary>
        public string etiology { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 是否急症（1是，0否）
        /// </summary>
        public int isemergency { get; set; }

        /// <summary>
        /// 是否感染病（1是，0否）
        /// </summary>
        public int isinfection { get; set; }

        /// <summary>
        /// 辅助检查
        /// </summary>
        public string laboratoryExamination { get; set; }

        /// <summary>
        /// 疾病名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 预防和预后
        /// </summary>
        public string prophylacticPrognosis { get; set; }

        /// <summary>
        /// 治疗方案
        /// </summary>
        public string treatmentPlan { get; set; }

        /// <summary>
        /// 治疗原则
        /// </summary>
        public string treatmentPrinciple { get; set; }

        /// <summary>
        /// 症状集合，比如1:急2:危3:重0:否
        /// </summary>
        public string symptom { get; set; }

        /// <summary>
        /// 流行病学
        /// </summary>
        public string epidemiology { get; set; }

        /// <summary>
        /// 疾病出处
        /// </summary>
        public string diseaseProvenance { get; set; }

        /// <summary>
        /// 常见症状
        /// </summary>
        public string commonSymptoms { get; set; }

        /// <summary>
        /// 用药方案，具体类型也不详
        /// </summary>
        public List<object> drugProgram { get; set; }

        /// <summary>
        /// 全部治疗方案
        /// </summary>
        public List<ProgramItem> drugProgramAll { get; set; }

        /// <summary>
        /// 检查方案
        /// </summary>
        public List<CheckItem> diseaseCheckItem { get; set; }

        /// <summary>
        /// 常见症状列表
        /// </summary>
        public List<Symptom> commonSymptomsList { get; set; }

        /// <summary>
        /// 是否是危症 1:是 ，0:否
        /// </summary>
        public int danger { get; set; }

        /// <summary>
        /// 是否重症 1:是 ，0:否
        /// </summary>
        public int grave { get; set; }

        /// <summary>
        /// 症状集合
        /// </summary>
        public string symptoms { get; set; }
    }



    /// <summary>
    /// 检查方案
    /// </summary>
    public class CheckItem
    {
        /// <summary>
        /// 疾病名称
        /// </summary>
        public string diseaseName { get; set; }

        /// <summary>
        /// 检查项
        /// </summary>
        public string itemName { get; set; }

        /// <summary>
        /// 检查项类型
        /// </summary>
        public string itemType { get; set; }

        /// <summary>
        /// 资料来源
        /// </summary>
        public string source { get; set; }
    }


    public class ProgramItem
    {
        public string smptomnames { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string icdcode { get; set; }
        public string icd10 { get; set; }

        /// <summary>
        /// 治疗方案集合
        /// </summary>
        public List<ProgramItem> childItem { get; set; }
        public bool emergencyFlag { get; set; }
        public int sort { get; set; }
        public bool existSuspectDisease { get; set; }
        public bool existChildItem { get; set; }
        public string judgingCondition { get; set; }
        public int nineDrugType { get; set; }
        public string drugid { get; set; }
        public string drugInstructionsId { get; set; }
        public string drugInstructions { get; set; }
        public string source { get; set; }
        public string drugType { get; set; }
        public bool @checked { get; set; }


        public string itemChidNames { get; set; }


    }

}
