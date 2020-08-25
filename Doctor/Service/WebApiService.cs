using Doctor.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;

namespace Doctor.Service
{
    public static class WebApiService
    {

        private static string url = ConfigurationManager.AppSettings["ApiUrl"];
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="doctorInfo"></param>
        /// <returns></returns>
        public static DoctorInfo LoginUser(DoctorInfo doctorInfo)
        {
            var json = JsonConvert.SerializeObject(doctorInfo);

            try
            {
                var postContent = HttpHelper.PostJson(url + "/api/assistant/user/login", json);



                //转换结果
                var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);

                var result_doctor = JsonConvert.DeserializeObject<DoctorInfo>(result.data.ToString());

                return result_doctor;
            }
            catch(Exception ex)
            {
                MessageBox.Show("登陆失败", "错误");
                return null;
            }

           
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
            dic.Add("doctorId", App.doctor.doctorId);
            dic.Add("doctorToken", App.doctor.doctorToken);
            dic.Add("timestamp", TimeHelper.GetTimeStamp());
            dic.Add("gender", patientInfo.sexCode) ;
            dic.Add("age", patientInfo.age.Replace("岁",""));
            dic.Add("outpatientId", patientInfo.outpatientId);

            try
            {
                var postContent = HttpHelper.Post(url + "/api/assistant/wm/querySymptomList", dic);
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

            }
            catch(Exception ex)
            {
                MessageBox.Show("搜索症状失败", "错误");
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
                dic.Add("doctorToken", App.doctor.doctorToken);//令牌
                dic.Add("doctorId", "34182533722695");//医生id
                dic.Add("timestamp", TimeHelper.GetTimeStamp());//时间戳
                dic.Add("outpatientId", "891919391-341825001");//门诊流水号
            }
            else
            {
                dic.Add("doctorToken", App.doctor.doctorToken);//令牌
                dic.Add("doctorId", App.doctor.doctorId);//医生id
                dic.Add("timestamp", TimeHelper.GetTimeStamp());//时间戳
                dic.Add("outpatientId", patientInfo.outpatientId);//门诊流水号
            }

            try {

                var postContent = HttpHelper.Post(url + "/api/assistant/wm/getHealthData", dic);

                //转换结果
                var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);
                if (result.data != null)
                {
                    var result_healthData = JsonConvert.DeserializeObject<HealthData>(result.data.ToString());

                    if (result_healthData != null)
                    {
                        healthData = result_healthData;
                    }
                    return healthData;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询患者体征数据失败", "错误");
                return null;
            }

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

            try {
                var postContent = HttpHelper.PostJson(url + "/api/assistant/wm/queryDiseaseBySymptom?doctorToken=" + App.doctor.doctorToken + "&timestamp=" + TimeHelper.GetTimeStamp() + "&outpatientId=" + patientInfo.outpatientId + "&doctorId=" + App.doctor.doctorId + "", json);


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
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询症状相关数据失败", "错误");
                return null;
            }

            return desease;
        }

        /// <summary>
        /// 添加疾病
        /// </summary>
        /// <returns></returns>
        public static bool AddDisease(string diseaseName, string symptoms,string outpatientId)
        {
            try
            {
                var postContent = HttpHelper.PostJson(url + "/api/assistant/wm/addDisease?diseaseName=" + diseaseName + "&symptoms=" + symptoms + "&doctorId=" + App.doctor.doctorId + "&doctorToken=" + App.doctor.doctorToken + "&timestamp=" + TimeHelper.GetTimeStamp() + "&outpatientId=" + outpatientId, "");


                //转换结果
                var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);
                if (result.msg != null && (result.msg == "SUCCESS" || result.msg == "Success" || result.msg == "success"))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加疾病失败", "错误");
                return false;
            }

        }


        /// <summary>
        /// 获取疾病知识库
        /// </summary>
        public static DiseaseDosageSchedule GetPlanList(string dicID,string outpatientId)
        {
            var dSchedule = new DiseaseDosageSchedule();
            
            Dictionary<string, string> dic = new Dictionary<string, string>() { };
            dic.Add("icd10", dicID);//令牌
            dic.Add("doctorToken", App.doctor.doctorToken);//令牌
            dic.Add("doctorId", App.doctor.doctorId);//医生id
            dic.Add("timestamp", TimeHelper.GetTimeStamp());//时间戳
            dic.Add("outpatientId", outpatientId);//诊断号
            try
            {
                var postContent = HttpHelper.Post(url + "/api/assistant/wm/getDiseaseDosageSchedule ", dic);

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
            catch(Exception ex)
            {
                LogHelper.Info("获取疾病知识库失败");
                LogHelper.Info("/api/assistant/wm/getDiseaseDosageSchedule-->");
                LogHelper.Info(JsonConvert.SerializeObject(dic));
                MessageBox.Show("获取疾病知识库失败", "错误");
                return null;
            }

            
            
        }


        /// <summary>
        /// 获取药品详情
        /// </summary>
        public static DrugKnowledageBase GetDrug(string drugid, string outpatientId)
        {
            var dSchedule = new DrugKnowledageBase();

            Dictionary<string, string> dic = new Dictionary<string, string>() { };
            dic.Add("drugid", drugid);//令牌
            dic.Add("doctorToken", App.doctor.doctorToken);//令牌
            dic.Add("doctorId", App.doctor.doctorId);//医生id
            dic.Add("timestamp", TimeHelper.GetTimeStamp());//时间戳
            dic.Add("outpatientId", outpatientId);//诊断号

            try
            {
                var postContent = HttpHelper.Post(url + "/api/assistant/wm/getDrugKnowledageBase", dic);

                //转换结果
                var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);
                if (result.data != null)
                {
                    var result_healthData = JsonConvert.DeserializeObject<DrugKnowledageBase>(result.data.ToString());

                    if (result_healthData != null)
                    {
                        dSchedule = result_healthData;
                    }
                    return dSchedule;
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取药品详情失败", "错误");
                return null;
            }

        }

    }
}
