using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_KKT_Online
{
    public class KKT
    {
        private readonly string phone;
        private readonly string password;

        public KKT(string phone, string password)
        {
            this.phone = phone;
            this.password = password;
        }

        public IEnumerable<Check> GetCheckReport(DateTime start, DateTime end)
        {
            var json = new HttpWorker().RequestReport(new System.Net.NetworkCredential(phone, password), start, end);

            foreach(var check in json)
            {
                yield return ParseCheck(check);
            }
        }

        public Check GetCheck(long _FiscalDriveNumber, long _FiscalDocumentNumber, long _FiscalSign)
        {
            var json = new HttpWorker().RequestCheck(new System.Net.NetworkCredential(phone, password), new Check()
            {
                CashBox = new CashRegister()
                {
                    FiscalDriveNumber = _FiscalDriveNumber,
                    FiscalDocumentNumber = _FiscalDocumentNumber,
                    FiscalSign = _FiscalSign
                }
            });

            return ParseCheck(json);
        }

        private Check ParseCheck(JToken json_check)
        {
            JToken checkInfo = json_check["document"]["receipt"];

            var check = new Check();

            check.CashBox = new CashRegister()
            {
                FiscalDocumentNumber = checkInfo["fiscalDocumentNumber"].Value<long>(),
                FiscalDriveNumber = checkInfo["fiscalDriveNumber"].Value<long>(),
                FiscalSign = checkInfo["fiscalSign"].Value<long>(),
                KktRegId = checkInfo["kktRegId"].Value<long>(),
                Operator = checkInfo["operator"].Value<string>()
            };
            check.CashTotalSum = checkInfo["cashTotalSum"].Value<int>().ToDecimal();
            check.dateTime = checkInfo["dateTime"].Value<DateTime>();
            check.ECashTotalSum = checkInfo["ecashTotalSum"].Value<int>().ToDecimal();
            check.Nds10 = checkInfo["nds10"] == null ? 0  : checkInfo["nds10"].Value<int>().ToDecimal();
            check.Nds18 = checkInfo["nds18"] == null ? 0 : checkInfo["nds18"].Value<int>().ToDecimal();
            check.TotalSum = checkInfo["totalSum"].Value<int>().ToDecimal();
            check.ShopInfo = new Shop()
            {
                Inn = checkInfo["userInn"].Value<long>(),
                Name = checkInfo["user"].Value<string>().Trim(' ', '\t', '\r', '\n')
            };

            foreach (var item in checkInfo["items"])
            {
                var product = new CheckItem()
                {
                    Name = item["name"].Value<string>(),
                    Price = item["price"].Value<int>().ToDecimal(),
                    Quantity = item["quantity"].Value<int>(),
                    Sum = item["sum"].Value<int>().ToDecimal()
                };

                if (item["nds10"] != null)
                    product.Nds10 = item["nds10"].Value<int>().ToDecimal();
                else
                    product.Nds18 = item["nds18"].Value<int>().ToDecimal();

                check.Items.Add(product);
            }

            check.json = JsonConvert.SerializeObject(json_check, Formatting.Indented);

            return check;
        }
    }
}
