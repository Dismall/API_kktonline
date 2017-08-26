# API_kktonline
Библиотека для выгрузки чеков с kkt-online

Пример выгрузки чека:
var kkt = new API_KKT_Online.KKT("<номер телефона,+7хххххххххх>", "<пароль>");
var check = kkt.GetCheck(<ФН>,<ФД>,<ФПД>);

Console.WriteLine(check.ShopInfo.Name);

foreach (var item in check.Items)
    Console.WriteLine($"{item.Name}\t{item.Price}\t{item.Quantity}\t{item.Sum}\t{item.Nds18}");
    
    
Пример выгрузки отчета:
var kkt = new API_KKT_Online.KKT("<номер телефона,+7хххххххххх>", "<пароль>");
var dateStart = DateTime.Now.AddDays(-5);
var dateEnd = DateTime.Now;

foreach (var check in kkt.GetCheckReport(dateStart, dateEnd))
{
    Console.WriteLine(check.ShopInfo.Name);

    foreach(var item in check.Items)
        Console.WriteLine($"{item.Name}\t{item.Price}\t{item.Quantity}\t{item.Sum}\t{item.Nds18}");
}
