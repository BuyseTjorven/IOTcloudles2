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

namespace MCT.functions
{
    public static class trigger
    {
        [FunctionName("trigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/{getal1}/{op}/{getal2}")] HttpRequest req,
            int getal1, string op, int getal2,
            ILogger log)
        {
            try
            {
                string result = "";
                if (op == "+")
                {
                    result = (getal1 + getal2).ToString();
                }
                if (op == "-")
                {
                    result = (getal1 - getal2).ToString();
                }
                if (op == "*")
                {
                    result = (getal1 * getal2).ToString();
                }
                if (string.IsNullOrEmpty(result))
                {
                    return new BadRequestObjectResult("ongeldige operator");
                }
                CalculationResult calculationResult = new CalculationResult
                {
                    Operator = op,
                    Result = result
                };
                return new OkObjectResult(calculationResult);
            }
            catch (System.Exception ex)
            {
                log.LogError(ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}
