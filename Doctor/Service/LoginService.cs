using Doctor.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor.Service
{
    public static class LoginService
    {
        public static DoctorInfo LoginUser(DoctorInfo doctorInfo)
        {
            var json = JsonConvert.SerializeObject(doctorInfo);
            var result = HttpHelper.PostJson("http://103.85.170.99:10001/api/assistant/user/login", json);
            return result;

        }
    }
}
