using BKBNIDVerify.DataModel;
using BKBNIDVerify.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace BKBNIDVerify.Models
{
    public class TokenManagerModels
    {

        BKBNIDDBEntities db = new BKBNIDDBEntities();
        Covid19LoanDBEntities db1 = new Covid19LoanDBEntities();
        ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string Token = null;
        private string AuthToken = null;
        private string error = null;
        private DateTime? lastTokenGenerated = null;

        public string GetToken()
        {
            AccessToken resultToken;
            if (AuthToken == null || IsTokenExpired())
            {
                AuthToken = GenerateToken();

            }
            // outErr = error;
            return AuthToken;
        }

        private string GenerateToken()
        {
            TokenResponse tokenRes = new TokenResponse();
            FailureResponse failureRes = new FailureResponse();

            string date_ = System.DateTime.Now.ToString("yyyy-MM-dd");
            SearchLog sl = new SearchLog();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://prportal.nidw.gov.bd/partner-service/rest/auth/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    //password = "bkb123456",
                    //username = "0918"
                    password = "@Bkrishibank#321",
                    username = "bkrishibank"
                });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                string errorMessage = null;
                if (result.Contains("SUCCESS"))
                {
                    tokenRes = JsonConvert.DeserializeObject<TokenResponse>(result);
                    return tokenRes.success.data.access_token;
                }
                else
                {
                    FailureResponse errResponse = JsonConvert.DeserializeObject<FailureResponse>(result);
                    tokenRes.status = "Status:" + errResponse.status + " Status Code:" + errResponse.statusCode;
                    //TempData["error"] = "Status:" + errResponse.status + " Status Code:" + errResponse.statusCode;
                    return tokenRes.status;
                }
            }
    }

        private bool IsTokenExpired()
        {
            System.DateTime dtDateTime = Convert.ToDateTime(lastTokenGenerated).AddHours(24);
            //dtDateTime = dtDateTime.AddSeconds(AuthToken.expires_in).ToLocalTime();
            return dtDateTime <= DateTime.Now;
        }
    }



}