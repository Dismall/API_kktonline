using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace API_KKT_Online
{
    class HttpWorker
    {
        public HttpWorker()
        {

        }
        public JObject RequestCheck(NetworkCredential credentials, Check check)
        {
            var webRequest = WebRequest.CreateHttp($@"http://proverkacheka.nalog.ru:8888/v1/inns/*/kkts/*/fss/{check.CashBox.FiscalDriveNumber}/tickets/{check.CashBox.FiscalDocumentNumber}?fiscalSign={check.CashBox.FiscalSign}&sendToEmail=no");

            setWebParams(ref webRequest, credentials);

            var response = webRequest.GetResponse();

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                string result = stream.ReadToEnd();

                if (!string.IsNullOrEmpty(result))
                    return JObject.Parse(result);            
            }

            return null;
        }

        public JArray RequestReport(NetworkCredential credentials, DateTime d1, DateTime d2)
        {
            var webRequest = WebRequest.CreateHttp($@"http://proverkacheka.nalog.ru:8888/v1/extract?dateFrom={d1.ToString("yyyy-MM-ddTHH:mm:ss")}&dateTo={d2.ToString("yyyy-MM-ddTHH:mm:ss")}&sendToEmail=0&fileType=json");
            setWebParams(ref webRequest, credentials);
            string file = getFileUrl(webRequest);

            if (file != null)
            {
                webRequest = WebRequest.CreateHttp($@"http://proverkacheka.nalog.ru:8888{file}");
                setWebParams(ref webRequest, credentials);

                var response = webRequest.GetResponse();

                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string result = stream.ReadToEnd();

                    if (!string.IsNullOrEmpty(result))
                    {
                        return JArray.Parse(result);
                    }
                }
            }

            return null;
        }

        private string getFileUrl(HttpWebRequest webRequest)
        {
            var response = webRequest.GetResponse();

            string file_url;

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                string result = stream.ReadToEnd();

                if (string.IsNullOrEmpty(result))
                    return null;

                var json = JObject.Parse(result);

                file_url = json["url"].Value<string>();
            }

            return file_url;
        }

        private void setWebParams(ref HttpWebRequest webRequest, NetworkCredential credentials)
        {
            webRequest.Method = "GET";
            webRequest.Timeout = 10000;
            webRequest.Headers.Add("Device-Id", Guid.NewGuid().ToString());
            webRequest.Headers.Add("Device-OS", "Android 5.0");
            webRequest.Headers.Add("Version", "2");
            webRequest.Headers.Add("ClientVersion", "1.4.1.3");
            webRequest.UserAgent = "okhttp/3.0.1";
            webRequest.Credentials = credentials;
            webRequest.PreAuthenticate = true;
        }
    }
}
