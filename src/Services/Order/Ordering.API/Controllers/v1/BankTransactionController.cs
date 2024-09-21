using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Controllers.v1
{
    public class BankTransactionController : Controller
    {
        private static string token = String.Empty;
        private static string language = "fa";

        // GET: Banks
        //public async Task<IActionResult> Index(BankTransactionRequestModel model)
        //{
        //    string token = model.Token;
        //    string language = model.Language;

        //    int rand = 0;
        //    (string, bool) retValue = ("در حال انتقال به درگاه بانک", false);

        //    //O2FittModels.BaseInfo.BaseInfo.CheckBaseInfo(ref token, ref language);

        //    if (String.IsNullOrEmpty(model.BankId))
        //    {
        //        rand = new Random().Next(1, 3);
        //    }

        //    try
        //    {
        //        string bankId = model.BankId.ToLower();
        //        if (rand == 1 || bankId == "41862cf9-5386-419c-915e-2ca2d6d27831")
        //        {
        //            if (model.Amount < 1000)
        //            {
        //                model.BankId = "1C3BD1BA-4C02-45A7-9108-66F53378C54C";
        //                model.Amount *= 10;
        //                (string, bool) ret = await O2Gateway.SendData(model);
        //                ViewBag.BankResponse = ret.Item1;
        //                ViewBag.BankResponseStatus = ret.Item2;
        //                return View("BankResponse");
        //            }
        //            else
        //            {
        //                model.BankId = "41862CF9-5386-419C-915E-2CA2D6D27831";
        //                model.Amount *= 10;
        //                retValue = await MellatGateway.SendData(model);
        //            }
        //        }
        //        else if (rand == 2 || bankId == "a7080806-9446-4b5e-acce-fc566e08ee76")
        //        {
        //            if (model.Amount < 1000)
        //            {
        //                model.BankId = "1C3BD1BA-4C02-45A7-9108-66F53378C54C";
        //                model.Amount *= 10;
        //                (string, bool) ret = await O2Gateway.SendData(model);
        //                ViewBag.BankResponse = ret.Item1;
        //                ViewBag.BankResponseStatus = ret.Item2;
        //                return View("BankResponse");
        //            }
        //            else
        //            {
        //                model.BankId = "A7080806-9446-4B5E-ACCE-FC566E08EE76";
        //                model.Amount *= 10;
        //                retValue = await SamanGateway.SendData(model);
        //            }
        //        }
        //        else if (bankId == "f1b9fe15-6123-4d04-9983-4980aaead0f9")
        //        {
        //            if (model.Amount == 0)
        //            {
        //                model.BankId = "1C3BD1BA-4C02-45A7-9108-66F53378C54C";
        //                model.Amount *= 10;
        //                (string, bool) ret = await O2Gateway.SendData(model);
        //                ViewBag.BankResponse = ret.Item1;
        //                ViewBag.BankResponseStatus = ret.Item2;
        //                return View("BankResponse");
        //            }
        //            else
        //            {
        //                Random random = new Random();
        //                model.resNum = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(1000, 9999).ToString();
        //                model.BankId = "F1B9FE15-6123-4D04-9983-4980AAEAD0F9";
        //            }
        //            var rtVal = await YekPayGateway.SendData(model);
        //            string Payment_URL = "https://gate.yekpay.com/api/payment/start/" + rtVal.Item3;
        //            Response.Redirect(Payment_URL);
        //        }
        //        else
        //        {
        //            retValue = ("درگاه بانک یافت نشد", false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retValue = (ex.Message, false);
        //    }
        //    return Content(retValue.Item1);
        //}
        //public async Task<ActionResult> SamanResponse(SamanResponseModel model)
        //{
        //    if (!String.IsNullOrEmpty(Request.QueryString["params"]))
        //    {
        //        string[] paramArr = Request.QueryString["params"].Split('_');
        //        model.Token = paramArr[0];
        //        model.Language = paramArr[1];

        //        (string, bool, string) ret = await SamanGateway.ResponseAsync(model);
        //        ViewBag.BankResponse = ret.Item1;
        //        ViewBag.BankResponseStatus = ret.Item2;
        //        ViewBag.TraceNo = ret.Item3;
        //    }
        //    return View("BankResponse");
        //}
        //public async Task<ActionResult> MellatResponse(MellatResponseModel model)
        //{
        //    if (!String.IsNullOrEmpty(Request.QueryString["params"]))
        //    {
        //        string[] paramArr = Request.QueryString["params"].Split('_');
        //        model.Token = paramArr[0];
        //        model.Language = paramArr[1];
        //        (string, bool, string) ret = await MellatGateway.MellatReturn(model);
        //        ViewBag.BankResponse = ret.Item1;
        //        ViewBag.BankResponseStatus = ret.Item2;
        //        ViewBag.TraceNo = ret.Item3;
        //    }
        //    return View("BankResponse");
        //}
        //public async Task<ActionResult> YekPayResponse()
        //{
        //    if (!String.IsNullOrEmpty(Request.QueryString["params"]))
        //    {
        //        string[] paramArr = Request.QueryString["params"].Split('_');
        //        string resNum = String.Empty;
        //        (bool, string) ret = await YekPayGateway.YekPayResponse(paramArr[0], paramArr[1], Request.QueryString["success"], Request.QueryString["authority"], paramArr[2]);
        //        ViewBag.BankResponse = ret.Item2;
        //        ViewBag.BankResponseStatus = ret.Item1;
        //        ViewBag.TraceNo = "";
        //    }
        //    return View("BankResponse");
        //}
    }
}
