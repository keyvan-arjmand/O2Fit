using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ordering.Web.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Web.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bank(int orderid, int bankid)
        {
            return View();
        }

        public async Task<IActionResult> SamanPayment(int orderid)
        {
            HttpClient httpClient = new HttpClient();

            ViewBag.ErrorBank = null;

            var result = await httpClient.GetAsync($"https://order.o2fitt.com/api/v1/Order/GetOrder?OrderId={orderid}&BankId=1");

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                SamanPayment samanPayment = JsonConvert.DeserializeObject<SamanPayment>(response);



                if (samanPayment.data != null)
                {
                    ViewBag.BankToken = samanPayment.data.token;
                    ViewBag.RedirectURL = $"{samanPayment.data.redirectUrl}";
                    ViewBag.BankUrl = samanPayment.data.postUrl;
                }
                else
                {
                    ViewBag.ErrorBank = "خطا در اتصال به بانک";
                }
            }

            return View();
        }

        public async Task<IActionResult> SamanBack(string State, string StateCode, string ResNum,
            string MID, string RefNum, string CID, string TRACENO, string SecurePan, string orderid)
        {
            string errorMsg = "";

            ViewBag.Message = errorMsg;

            ViewBag.State = State;
            ViewBag.StateCode = StateCode;
            ViewBag.MID = MID;
            ViewBag.RefNum = RefNum;
            ViewBag.CID = CID;
            ViewBag.TRACENO = TRACENO;
            ViewBag.SecurePan = SecurePan;

            //OrderId Back
            ViewBag.ResNum = ResNum;

            SamanResult samanResult = new SamanResult()
            {
                CID = CID,
                MID = MID,
                RefNum = RefNum,
                ResNum = ResNum,
                SecurePan = SecurePan,
                State = State,
                StateCode = StateCode,
                TRACENO = TRACENO,
            };

            try
            {
                if (State != null && ResNum != null && RefNum != null)
                {
                    HttpClient httpClient = new HttpClient();
                    var apiSerlize = JsonConvert.SerializeObject(samanResult);
                    var content = new StringContent(apiSerlize, Encoding.UTF8, "application/json");
                    var result = await httpClient.PostAsync("https://order.o2fitt.com/api/v1/Order/SamanResponse", content);

                    if (result.IsSuccessStatusCode)
                    {
                        var _value = await result.Content.ReadAsStringAsync();
                        SamanResultResponse samanMessage = JsonConvert.DeserializeObject<SamanResultResponse>(_value);

                        if (samanMessage.data.isError)
                        {
                            ViewBag.Message = samanMessage.data.errorMsg;
                        }
                        else
                        {
                            ViewBag.Message = samanMessage.data.succeedMsg;
                        }

                    }
                }
                else
                {
                    ViewBag.Message = "خطا در پرداخت مجدد سعی نمایید";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "خطا در پرداخت";
            }

            return View();
        }

        public async Task<IActionResult> MelatPayment(int orderid)
        {
            HttpClient httpClient = new HttpClient();

            ViewBag.ErrorBank = null;

            var result = await httpClient.GetAsync($"https://order.o2fitt.com/api/v1/Order/GetOrder?OrderId={orderid}&BankId=0");

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                MelatPayment samanPayment = JsonConvert.DeserializeObject<MelatPayment>(response);

                if (samanPayment.data.isError == false)
                {
                    ViewBag.RefId = samanPayment.data.RefId;
                    ViewBag.RedirectURL = $"{samanPayment.data.RedirectUrl}";
                    ViewBag.BankUrl = samanPayment.data.PostUrl;
                }
                else
                {
                    ViewBag.ErrorBank = samanPayment.data.ErrorMessage;
                }
            }
            else
            {
                ViewBag.ErrorBank = "خطا در ارتباط با درگاه بانک ملت";
            }

            return View();
        }

        public async Task<IActionResult> MelatBack(string RefId, string ResCode, string SaleOrderId, string SaleReferenceId, string orderid)
        {
            ViewBag.Message = null;

            ViewBag.RefId = RefId;
            ViewBag.ResCode = ResCode;
            ViewBag.SaleReferenceId = SaleReferenceId;

            //Order Id Back
            ViewBag.SaleOrderId = SaleOrderId;

            try
            {
                if (ResCode == "0")
                {
                    MelatResult melatResult = new MelatResult()
                    {
                        RefId = RefId,
                        ResCode = ResCode,
                        SaleOrderId = SaleOrderId,
                        SaleReferenceId = SaleReferenceId
                    };

                    HttpClient httpClient = new HttpClient();
                    var apiSerlize = JsonConvert.SerializeObject(melatResult);
                    var content = new StringContent(apiSerlize, Encoding.UTF8, "application/json");
                    var result = await httpClient.PostAsync("https://order.o2fitt.com/api/v1/Order/MelatResponse", content);

                    if (result.IsSuccessStatusCode)
                    {
                        var _value = await result.Content.ReadAsStringAsync();
                        MelatResultResponse melatMessage = JsonConvert.DeserializeObject<MelatResultResponse>(_value);

                        if (melatMessage.data.isError)
                        {
                            ViewBag.Message = melatMessage.data.errorMsg;
                        }
                        else
                        {
                            ViewBag.Message = melatMessage.data.succeedMsg;
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "پرداخت آنلاین با خطا مواجه شده است.";
                }
            }
            catch
            {
                ViewBag.Message = "خطا در برقراری ارتباط با درگاه بانک ملت";
            }

            return View();
        }

        public async Task<IActionResult> YekPayBack(string success, string authority)
        {

            ViewBag.Message = null;

            //Order Id Back
            ViewBag.OrderId = null;

            ViewBag.success = success;
            ViewBag.authority = authority;

            YekPayResult yekPayResult = new YekPayResult
            {
                Authority = authority,
                Success = success
            };

            try
            {
                HttpClient httpClient = new HttpClient();
                var apiSerlize = JsonConvert.SerializeObject(yekPayResult);
                var content = new StringContent(apiSerlize, Encoding.UTF8, "application/json");
                var result = await httpClient.PostAsync("https://order.o2fitt.com/api/v1/Order/YekPayResponse", content);

                if (result.IsSuccessStatusCode)
                {
                    var _value = await result.Content.ReadAsStringAsync();
                    YekPayMessage yekpay = JsonConvert.DeserializeObject<YekPayMessage>(_value);

                    ViewBag.Message = yekpay.data.succeedMsg == null ? yekpay.data.errorMsg : yekpay.data.succeedMsg;
                    ViewBag.OrderId = yekpay.data.OrderId;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error : {ex.Message}";
            }

            return View();
        }

        public IActionResult BackApp([FromQuery] string orderId)
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
