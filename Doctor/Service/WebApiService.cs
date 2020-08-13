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
        public static DoctorInfo LoginUser(DoctorInfo doctorInfo)
        {
            var json = JsonConvert.SerializeObject(doctorInfo);
            var postContent = HttpHelper.PostJson("http://103.85.170.99:10001/api/assistant/user/login", json);

            //转换结果
            var result = JsonConvert.DeserializeObject<WebApiResult>(postContent);

            var result_doctor = JsonConvert.DeserializeObject<DoctorInfo>(result.data.ToString());

            return result_doctor;
        }

        public static List<Symptom> QuerySymptomList()
        {
            var list = new List<Symptom>() { };




            return list;


        }

    }
}
