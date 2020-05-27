using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using FunctionApp1.Model;
using System.Net;
using System.Text;

namespace FunctionApp1
{

    /// <summary>
    /// This function app used to perform the addtion operation by using two integers
    /// </summary>
    public static class Addition
    {
        [FunctionName("Addition")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Addition")] HttpRequestMessage req)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");
            bool success = true;
            try
            {
                var request = req.RequestUri;
                var modelValues = await req.Content.ReadAsStringAsync();
                var inputElements = JsonConvert.DeserializeObject<Elements>(modelValues);

                Elements elements = new Elements();
                elements.number1 = inputElements.number1;
                elements.number2 = inputElements.number2;
                elements.result = inputElements.number1 + inputElements.number2;

                var response = JsonConvert.SerializeObject(elements);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(response, Encoding.UTF8, "application/json")
                };
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success
                ? req.CreateResponse(HttpStatusCode.InternalServerError, "Unable to process your request!")
                : req.CreateResponse(HttpStatusCode.BadRequest, "You have passed invalid integer");
        }
    }
}
