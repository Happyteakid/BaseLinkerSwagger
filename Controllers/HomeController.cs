using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BaselinkerREST.Model;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using BaselinkerREST.Methods;

namespace BaselinkerREST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// You can set admin_comments, extra_field_1, extra_field_2. Required parameter(OrderID).
        /// </summary>
        /// <param name="baseLinkerRequestBody"></param>
        /// <returns></returns>
        [HttpPost("setOrderFields")]
        public async Task<IActionResult> setOrderFields ([FromBody] BaseLinkerRequest01 baseLinkerRequestBody)
        {
            var client = new RestClient("https://api.baselinker.com/connector.php");
            var retryCount = 0;
            Start:
            while (retryCount < 10)
            {
                try
                {
                    var restResponse = await client.PostAsync(new Methods.Methods().CreateRestRequest("setOrderFields", baseLinkerRequestBody));
                    if (restResponse.StatusCode >= HttpStatusCode.InternalServerError ||
                        restResponse.StatusCode == HttpStatusCode.RequestTimeout)
                    {
                        await Task.Delay(400);
                        retryCount++;
                        _logger.LogInformation($"Failed getting a proper response. Retry count: {retryCount}");
                        goto Start;
                    }
                    _logger.LogInformation("RESPONSE BODY: "+restResponse.Content);
                    return Ok(restResponse.Content);
                }
                catch (HttpRequestException error)
                {
                    _logger.LogError($"Error {error.StatusCode}, {error.Message}, at retry count:{retryCount}");
                    await Task.Delay(200);
                    retryCount++;
                    goto Start;
                }
            }
            _logger.LogError($"Canceling setOrderFields. Parameters: -OrderId {baseLinkerRequestBody.orderId}");
            return StatusCode(408);
        }


        /// <summary>
        /// Set entries for custom_extra_fields with parameters(OrderID) and an Array that may contain text, numbers, binary file up to 2MB.
        /// </summary>
        /// <param name="baseLinkerRequestBody"></param>
        /// <returns></returns>
        [HttpPost("setCustomOrderFields")]
        public async Task<IActionResult> setCustomOrderFields([FromBody] BaseLinkerRequest02 baseLinkerRequestBody)
        {
            var client = new RestClient("https://api.baselinker.com/connector.php");
            var retryCount = 0;
            Start:
            while (retryCount < 10)
            {
                try
                {
                    var restResponse = await client.PostAsync(new Methods.Methods().CreateRestRequest("setOrderFields", baseLinkerRequestBody));
                    if (restResponse.StatusCode >= HttpStatusCode.InternalServerError ||
                        restResponse.StatusCode == HttpStatusCode.RequestTimeout)
                    {
                        await Task.Delay(200);
                        retryCount++;
                        _logger.LogInformation($"{DateTime.Now} Failed getting a proper response. Retry count: {retryCount}");
                        goto Start;
                    }
                    _logger.LogInformation("\nRESPONSE BODY: " + restResponse.Content);
                    return Ok(restResponse.Content);
                }
                catch (HttpRequestException error)
                {
                    await Task.Delay(200);
                    retryCount++;
                    _logger.LogError($"Error {error.StatusCode}, {error.Message}, at retry count:{retryCount}");
                    goto Start;
                }
            }
            _logger.LogError($"Canceling setCustomOrderFields. Parameters: -OrderId {baseLinkerRequestBody.OrderId} -Custom Field ID {baseLinkerRequestBody.Extra_field_ID}");
            return StatusCode(408);
        }

        [HttpPost("addOrderInvoiceFile")]
        public async Task<IActionResult> addFile(string filePath, string external_invoice_number, int invoice_id)
        {
            string file = "data:";
            file = file+(new Methods.Methods().ReplaceChar(System.IO.File.ReadAllText(filePath)));
            
            string? bytes = Encoding.UTF8.GetBytes(file).ToString();

            BaseLinkerRequest03 baseLinkerRequestBody = new BaseLinkerRequest03(invoice_id, bytes, external_invoice_number);

            var client = new RestClient("https://api.baselinker.com/connector.php");
            var retryCount = 0;
            Start:
            while (retryCount < 10)
            {
                try
                {
                    var restResponse = await client.PostAsync(new Methods.Methods().CreateRestRequest("addOrderInvoiceFile", baseLinkerRequestBody));
                    if (restResponse.StatusCode >= HttpStatusCode.InternalServerError ||
                        restResponse.StatusCode == HttpStatusCode.RequestTimeout)
                    {
                        await Task.Delay(200);
                        retryCount++;
                        _logger.LogInformation($"Failed getting a proper response. Retry count: {retryCount}");
                        goto Start;
                    }
                    _logger.LogInformation("RESPONSE BODY: " + restResponse.Content);
                    return Ok(restResponse.Content);
                }
                catch (HttpRequestException error)
                {
                    await Task.Delay(200);
                    retryCount++;
                    _logger.LogError($"Error {error.StatusCode}, {error.Message}, at retry count:{retryCount}");
                    goto Start;
                }
            }
            _logger.LogError($"Canceling addOrderInvoice request. Parameters: FilePath - {filePath} External invoice number {external_invoice_number} Invoice Id - {invoice_id}");
            return StatusCode(408);
        }



    }
}
