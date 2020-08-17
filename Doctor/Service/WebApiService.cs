using Doctor.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Service
{
    public static class WebApiService
    {

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="doctorInfo"></param>
        /// <returns></returns>
        public static DoctorInfo LoginUser(DoctorInfo doctorInfo)
        {
            var json = JsonConvert.SerializeObject(doctorInfo);
            var postContent = HttpHelper.PostJson("http://103.85.170.99:10001/api/assistant/user/login", json);

            //转换结果
            var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);

            var result_doctor = JsonConvert.DeserializeObject<DoctorInfo>(result.data.ToString());

            return result_doctor;
        }


        /// <summary>
        /// 搜索症状
        /// </summary>
        /// <returns></returns>
        public static List<Symptom> QuerySymptomList(string ymptomName,PatientInfo patientInfo)
        {
            var list = new List<Symptom>() { };
            if (string.IsNullOrEmpty(ymptomName))
            {
                return list;
            }

            Dictionary<string, string> dic = new Dictionary<string, string>() { };
            dic.Add("symptomSynonym", ymptomName);
            dic.Add("doctorId", TokenHelper.doctorInfo.doctorId);
            dic.Add("doctorToken", TokenHelper.doctorInfo.doctorToken);
            dic.Add("timestamp", TimeHelper.GetTimeStamp());
            dic.Add("gender", patientInfo.sexCode) ;
            dic.Add("age", patientInfo.age.Replace("岁",""));
            dic.Add("outpatientId", patientInfo.outpatientId);

            var postContent = HttpHelper.Post("http://103.85.170.99:10001/api/assistant/wm/querySymptomList", dic);


            //转换结果
            var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);
            if (result.data != null)
            {
                var result_symptomList = JsonConvert.DeserializeObject<List<Symptom>>(result.data.ToString());

                if (result_symptomList != null && result_symptomList.Any())
                {
                    list = result_symptomList;
                }
            }
           
            return list;
        }


        /// <summary>
        /// 查询患者体征数据
        /// </summary>
        /// <returns></returns>
        public static HealthData QueryHealthData(PatientInfo patientInfo)
        {
            var healthData = new HealthData();
            
            Dictionary<string, string> dic = new Dictionary<string, string>() { };
            if (patientInfo == null)
            {
                dic.Add("doctorToken", TokenHelper.doctorInfo.doctorToken);//令牌
                dic.Add("doctorId", "34182533722695");//医生id
                dic.Add("timestamp", TimeHelper.GetTimeStamp());//时间戳
                dic.Add("outpatientId", "891919391-341825001");//门诊流水号
            }
            else
            {
                dic.Add("doctorToken", TokenHelper.doctorInfo.doctorToken);//令牌
                dic.Add("doctorId", TokenHelper.doctorInfo.doctorId);//医生id
                dic.Add("timestamp", TimeHelper.GetTimeStamp());//时间戳
                dic.Add("outpatientId", patientInfo.outpatientId);//门诊流水号
            }

            var postContent = HttpHelper.Post("http://103.85.170.99:10001/api/assistant/wm/getHealthData", dic);

            //转换结果
            var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);
            if (result.data != null)
            {
                var result_healthData = JsonConvert.DeserializeObject<HealthData>(result.data.ToString());

                if (result_healthData != null )
                {
                    healthData = result_healthData;
                }
                return healthData;
            }
            return null;
        }


        /// <summary>
        /// 搜索症状
        /// </summary>
        /// <returns></returns>
        public static Disease QueryDiseaseBySymptom(string symptom,PatientInfo patientInfo)
        {
            var desease = new Disease();

            var item = new QueryDiseaseBySymptomParams();
            var baseInfo = new Baseinfo() {
                gender = patientInfo.sexCode,
                patientCard = patientInfo.patientId,
                spersonname = patientInfo.patientName,
                //age = patientInfo.age,               
            };
            item.baseinfo = baseInfo;
            item.outpatientId = patientInfo.outpatientId;
            item.trackId = "";
            item.symptom = symptom;
            item.searchtype = 3;
            item.type = 1;


            var json = JsonConvert.SerializeObject(item);


            var postContent = HttpHelper.PostJson("http://103.85.170.99:10001/api/assistant/wm/queryDiseaseBySymptom?doctorToken="+ TokenHelper.doctorInfo.doctorToken + "&timestamp=" + TimeHelper.GetTimeStamp() + "&outpatientId="+ patientInfo.outpatientId + "&doctorId=" + TokenHelper.doctorInfo.doctorId + "", json);


            //转换结果
            var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);
            if (result.data != null)
            {
                var result_desease = JsonConvert.DeserializeObject<Disease>(result.data.ToString());

                if (result_desease != null)
                {
                    desease = result_desease;
                }
            }

            return desease;
        }

        /// <summary>
        /// 添加疾病知识库
        /// </summary>
        public static DiseaseDosageSchedule GetPlanList(string dicID)
        {
            var dSchedule = new DiseaseDosageSchedule();
            
            Dictionary<string, string> dic = new Dictionary<string, string>() { };
            dic.Add("icd10", dicID);//令牌
            dic.Add("doctorToken", "19bdb7cd22354ce0ab8d8e4a8f35e49d");//令牌
            dic.Add("doctorId", "34182533722695");//医生id
            dic.Add("timestamp", TimeHelper.GetTimeStamp());//时间戳
            dic.Add("outpatientId", "891919391-341825001");//门诊流水号

            var postContent = HttpHelper.Post("http://103.85.170.99:10001/api/assistant/wm/getDiseaseDosageSchedule ", dic);

            //转换结果
            var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);
            if (result.data != null)
            {
                var result_healthData = JsonConvert.DeserializeObject<DiseaseDosageSchedule>(result.data.ToString());

                if (result_healthData != null)
                {
                    dSchedule = result_healthData;
                }
                return dSchedule;
            }
            return null;
        }

    }
}
