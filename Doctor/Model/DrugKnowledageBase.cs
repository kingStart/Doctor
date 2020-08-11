using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    /// <summary>
    /// DrugKnowledageBase  药品知识库
    /// </summary>
    public class DrugKnowledageBase
    {
        /// <summary>
        /// 药品ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string drugName { get; set; }

        /// <summary>
        /// 药品编码
        /// </summary>
        public string drugCode { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string drugFormulation { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string drugSpecification { get; set; }

        /// <summary>
        /// 类型1
        /// </summary>
        public double drugType1 { get; set; }

        /// <summary>
        /// 类型2
        /// </summary>
        public string drugType2 { get; set; }

        /// <summary>
        /// 类型3
        /// </summary>
        public string drugType3 { get; set; }

        /// <summary>
        /// 类型4
        /// </summary>
        public string drugType4 { get; set; }

        /// <summary>
        /// 详细说明
        /// </summary>
        public string drugUrl { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string drugAlias { get; set; }

        /// <summary>
        /// 适应症
        /// </summary>
        public string drugAdaptation { get; set; }

        /// <summary>
        /// 不良反应
        /// </summary>
        public string drugUntowardEffect { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string drugAttention { get; set; }

        /// <summary>
        /// 使用说明，用法用量
        /// </summary>
        public string drugInstructions { get; set; }

        /// <summary>
        /// 性状
        /// </summary>
        public string drugShape { get; set; }

        /// <summary>
        /// 禁忌症
        /// </summary>
        public string drugTaboo { get; set; }

        /// <summary>
        /// 基本信息
        /// </summary>
        public DrugBase basicInformation { get; set; }
    }

    public class DrugBase
    {

        /// <summary>
        /// 剂型
        /// </summary>
        public string drugFormulation { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string drugSpecification { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string drugAlias { get; set; }

        /// <summary>
        /// 性状
        /// </summary>
        public string drugShape { get; set; }
    }
}
