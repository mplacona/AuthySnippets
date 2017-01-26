using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthySnippets
{
    public class Program
    {
        // You can get your API Key on
        // https://dashboard.authy.com
        private const string AuthyAPIKey = "AUTHY_API_KEY";
        public static void Main(string[] args)
        {
            //CreateApprovalRequestAsync().Wait();
            //GetApprovalRequestAsync("162a5990-c5ea-0134-2635-0e5d6a065904").Wait();
            //RequestAuthySMSAsync().Wait();
            //RequestAuthyVoiceAsync().Wait();
            //VerifyTokenAsync().Wait();
            //StartPhoneVerificationAsync().Wait();
            //VerifyPhoneAsync().Wait();
        }

#region Push Notification Authentication with OneTouch
        // Authy Create OneTouch Request
        public static async Task CreateApprovalRequestAsync()
        {
            // Create client
            var client = new HttpClient();

            // Add authentication header
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("message", "Requesting War Room Access"),
                new KeyValuePair<string, string>("seconds_to_expire", "300"),
                new KeyValuePair<string, string>("details[Location]", "California, USA"),
                new KeyValuePair<string, string>("details[Room]", "VR Room 1"),
            });

            // http://api.authy.com/onetouch/$AUTHY_API_FORMAT/users/$AUTHY_ID/approval_requests
            HttpResponseMessage response = await client.PostAsync(
                "http://api.authy.com/onetouch/json/users/5661166/approval_requests",
                requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }

        // Authy Check OneTouch Status
        public static async Task GetApprovalRequestAsync(string requestId)
        {
            // Create client
            var client = new HttpClient();

            // Add authentication header
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

            // http://api.authy.com/onetouch/$AUTHY_API_FORMAT/approval_requests/$UUID
            HttpResponseMessage response = await client.GetAsync(
                "http://api.authy.com/onetouch/json/approval_requests/" + requestId);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }
#endregion        

#region OTP via SMS or voice with OneCode
        // Authy Request OneCode via SMS
        public static async Task RequestAuthySMSAsync()
        {
            // Create client
            var client = new HttpClient();

            // Add authentication header
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

            // http://api.authy.com/protected/$AUTHY_API_FORMAT/sms/$AUTHY_ID?force=true
            HttpResponseMessage response = await client.GetAsync(
                "http://api.authy.com/protected/json/sms/5661166?force=true");

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }

        // Authy Request OneCode via Voice
        public static async Task RequestAuthyVoiceAsync()
        {
            // Create client
            var client = new HttpClient();

             // Add authentication header
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

            // http://api.authy.com/protected/$AUTHY_API_FORMAT/call/$AUTHY_ID?force=true
            HttpResponseMessage response = await client.GetAsync(
                "http://api.authy.com/protected/json/call/5661166?force=true");

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }

        // Authy Verify OneCode
        public static async Task VerifyTokenAsync()
        {
            // Create client
            var client = new HttpClient();

            // Add authentication header
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

            // http://api.authy.com/protected/$AUTHY_API_FORMAT/verify/$ONECODE/$AUTHY_ID
            HttpResponseMessage response = await client.GetAsync(
                "http://api.authy.com/protected/json/verify/3812001/5661166");

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }
    
#endregion    

#region Verify possession of phone
       public static async Task StartPhoneVerificationAsync()
       {
            // Create client
            var client = new HttpClient();            

            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("via", "sms"),
                new KeyValuePair<string, string>("phone_number", "5558675309"),
                new KeyValuePair<string, string>("country_code", "1"),
            });
            
            // https://api.authy.com/protected/$AUTHY_API_FORMAT/phones/verification/start?via=$VIA&country_code=$USER_COUNTRY&phone_number=$USER_PHONE
            HttpResponseMessage response = await client.PostAsync(
                "https://api.authy.com/protected/json/phones/verification/start?api_key=" + AuthyAPIKey,
                requestContent);


            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
       } 
       public static async Task VerifyPhoneAsync()
        {
            // Create client
            var client = new HttpClient();

            // Add authentication header
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

            // https://api.authy.com/protected/$AUTHY_API_FORMAT/phones/verification/check?phone_number=$USER_PHONE&country_code=$USER_COUNTRY&verification_code=$VERIFY_CODE
            HttpResponseMessage response = await client.GetAsync(
                "https://api.authy.com/protected/json/phones/verification/check?phone_number=5558675309&country_code=1&verification_code=3043");

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
        }
#endregion
    }
}
