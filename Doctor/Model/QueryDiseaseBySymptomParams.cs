using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Model
{
    

    public class Baseinfo
    {
        /// <summary>
        /// 男
        /// </summary>
        public string gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patientCard { get; set; }
        /// <summary>
        /// 张三
        /// </summary>
        public string spersonname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int age { get; set; }
    }

    public class QueryDiseaseBySymptomParams
    {
        /// <summary>
        /// 
        /// </summary>
        public Baseinfo baseinfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string outpatientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trackId { get; set; }
        /// <summary>
        /// 发热（体温≥37.3℃）
        /// </summary>
        public string symptom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int searchtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
    }

}
