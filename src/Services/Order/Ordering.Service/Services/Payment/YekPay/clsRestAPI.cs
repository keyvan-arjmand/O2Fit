using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Service.Services.Payment.YekPay
{
    public class clsRestAPI
    {
        public string LoadWebSite(string Address, string PostData)
        {
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Address);
                request.UserAgent = "VB.NET";
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(PostData);
                request.ContentLength = byteArray.Length;
                System.IO.Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                System.Net.WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                String Result = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                return Result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
