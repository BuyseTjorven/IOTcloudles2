using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MCT.functions.Models;

namespace MCT.Functions
{
    public static class secondcalculator
    {
        [FunctionName("secondcalculator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CalculationRequest calculationRequest = JsonConvert.DeserializeObject<CalculationRequest>(requestBody);
            CalculationResult calculationResult = new CalculationResult();
            calculationResult.Operator = calculationRequest.Operator;
            if (calculationRequest.Operator == "+")
            {
                calculationResult.Result = (calculationRequest.Getal1 + calculationRequest.Getal2).ToString();
            }



            return new OkObjectResult(calculationResult);
        }
    }
}
