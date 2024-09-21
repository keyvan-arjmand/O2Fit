using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service.Services.Sms
{
    public class SmsService : ISmsService
    {
        public async Task<bool> SendSmsNewVersionSmsIr(string phone, string code)
        {
            var apiKey = "VwdOrxksRmuiyCjCdoGui635cHxYNAEQ26k7RmKw3QN8zKEVFeeqUNjsAbY0Vz0x";
            
            parameter[] parameters = new parameter[1];
            parameters[0] = new parameter
            {
                name = "VerificationCode",
                value = code
            };

            RequestBody requestBody = new RequestBody
            {
                mobile = phone,
                templateId = 514504,
                parameters = parameters
            };

            var serializeObject = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");


            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            var response = await client.PostAsync(new Uri("https://api.sms.ir/v1/send/verify"), content);

            if (response.StatusCode == HttpStatusCode.OK) 
                return true;
            
            return false;
        }

        public async Task<bool> SendSmsRegisterNutritionist(string phone)
        {
            var apiKey = "VwdOrxksRmuiyCjCdoGui635cHxYNAEQ26k7RmKw3QN8zKEVFeeqUNjsAbY0Vz0x";

            parameter[] parameters = new parameter[1];
            parameters[0] = new parameter
            {
                name = "Name",
                value = "متخصص"
            };

            RequestBody requestBody = new RequestBody
            {
                mobile = phone,
                templateId = 644583,
                parameters = parameters
            };

            var serializeObject = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");


            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
            var response = await client.PostAsync(new Uri("https://api.sms.ir/v1/send/verify"), content);

            if (response.StatusCode == HttpStatusCode.OK)
                return true;

            return false;
        }

        public int Balance()
        {
            //TODO IMPLEMENT MAGFA SERVICE SMS
            // string username = "oksizhen_97500021";
            // string password = "T9Qw5Bj&";
            // string domain = "magfa";
            //
            // // Client
            // var client = new RestClient("https://sms.magfa.com/api/http/sms/v2/balance");
            //
            // // Auth
            // client.Authenticator = new HttpBasicAuthenticator(username + "/" + domain, password);
            //
            // // Request
            // var request = new RestRequest();
            // request.AddHeader("cache-control", "no-cache");
            // request.AddHeader("accept", "application/json");
            //
            // // Call
            // var response = client.Execute(request);
            //
            // if (response.StatusCode == HttpStatusCode.OK)
            // {
            //     return response;
            // }
            return 25;
        }

    }

    class RequestBody
    {
        public string mobile { get; set; }
        public int templateId { get; set; }
        public parameter[] parameters { get; set; }
    }

    class parameter
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
