using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace NalogCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            long FN = 8710000100430665;
            long FD = 21839;
            long FPD = 2652550138;

            // string post = $"fiscalSign={FPD}&sendToEmail=no";

            // byte[] byt = Encoding.UTF8.GetBytes(post);

            // var webReq = WebRequest.CreateHttp("http://proverkacheka.nalog.ru:8888" + $"/v1/inns/*/kkts/*/fss/{FN}/tickets/{FD}?{post}");

            // webReq.Method = "GET";
            // webReq.Timeout = 12000;
            // webReq.Headers.Add("Device-Id", Guid.NewGuid().ToString());
            // webReq.Headers.Add("Device-OS", "Android 5.0");
            // webReq.Headers.Add("Version", "2");
            // webReq.Headers.Add("ClientVersion", "1.4.1.3");
            // webReq.UserAgent = "okhttp/3.0.1";

            // webReq.Credentials = new NetworkCredential("+79117702827", "694445");
            // webReq.PreAuthenticate = true;
            // //webReq.ContentType = "application/x-www-form-urlencoded";
            //// webReq.ContentLength = byt.Length;

            // //using (var stream = webReq.GetRequestStream())
            // //{
            // //    stream.Write(byt, 0, byt.Length);
            // //}

            // var resp = webReq.GetResponse();

            // using (StreamReader stream = new StreamReader(resp.GetResponseStream()))
            // {
            //     string result = stream.ReadToEnd();

            //     Console.WriteLine(result);

            //     var json = JObject.Parse(result);

            //     string json_result = JsonConvert.SerializeObject(json, Formatting.Indented);

            //     System.IO.File.WriteAllText($"res{DateTime.Now.ToString("HH_mm_ss")}.txt", json_result);
            // }


            var kkt = new API_KKT_Online.KKT("+79117702827", "694445");

            //var check = kkt.GetCheck(FN, FD, FPD);
            foreach (var check in kkt.GetCheckReport(DateTime.Now.AddDays(-2), DateTime.Now))
                Console.WriteLine($"Shop {check.ShopInfo.Name} Total {check.TotalSum}");
            

            //Console.WriteLine($"SHOP NAME: {check.ShopInfo.Name}");
            Console.Read();

        }
    }
}
