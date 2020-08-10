using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// Diagnosis  辨证施治
    /// </summary>
    public class Diagnosis
    {

        /// <summary>
        /// 是否被收藏，和用 户的微信 OpenID 关联，1 为收藏，0 为未收藏
        /// </summary>
        public string isstar { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 基础资料类型
        /// </summary>
        public int istype { get; set; }

        /// <summary>
        /// 辨证结果子集
        /// </summary>
        public List<DiagnosisDetail> items { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public int backid { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public int checkcode { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public List<string> fztGoodsList { get; set; }


    }


    /// <summary>
    /// 辨证施治明细
    /// </summary>
    public class DiagnosisDetail
    {
        /// <summary>
        /// 病症
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 症候，用全角空格 分隔
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 治法
        /// </summary>
        public string zf { get; set; }

        /// <summary>
        /// 辨证
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 辨证编号
        /// </summary>
        public string bzcode { get; set; }

        /// <summary>
        /// 针灸指压子集
        /// </summary>
        public string cf { get; set; }

        /// <summary>
        /// 针灸指压手法
        /// </summary>
        public List<ZCY> zjf { get; set; }

        /// <summary>
        /// 传统方剂名称
        /// </summary>
        public string cm1 { get; set; }

        /// <summary>
        /// 传统方剂饮片文 本，用全角空格分 隔
        /// </summary>
        public string cm2 { get; set; }

        /// <summary>
        /// 现代中成药子集
        /// </summary>
        public List<ZCY> zcy { get; set; }

        /// <summary>
        /// 其他疗法
        /// </summary>
        public string qt { get; set; }

        /// <summary>
        /// 食疗药膳
        /// </summary>
        public string sl { get; set; }

        /// <summary>
        /// 临证参考
        /// </summary>
        public string lzck { get; set; }

        /// <summary>
        /// 保健品子集
        /// </summary>
        public string bjp { get; set; }

        /// <summary>
        /// 医疗器械子集
        /// </summary>
        public string ylqx { get; set; }

        /// <summary>
        /// 合作伙伴诊断结果
        /// </summary>
        public List<string> partnerinfo { get; set; }

        /// <summary>
        /// 疾病编码
        /// </summary>
        public string diseaseCode { get; set; }

        /// <summary>
        /// 西医症状名称
        /// </summary>
        public string symptomName { get; set; }

        /// <summary>
        /// 处方回传ID,用于 处方回传接口 复诊接口
        /// </summary>
        public string backid { get; set; }

        /// <summary>
        /// 处方回传校验码
        /// </summary>
        public string checkcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string tcmDrugsRespParams { get; set; }

        /// <summary>
        /// 用法（服用方法）
        /// </summary>
        public string tcmDrugsAnnotation { get; set; }


    }
}
