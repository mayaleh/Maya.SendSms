using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Maya.BulkGate.Sms;

namespace SendSms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionalController : ControllerBase
    {
        private readonly IClient smsClient;
        private readonly ILogger<TransactionalController> logger;

        public TransactionalController(IClient smsClient, ILogger<TransactionalController> logger)
        {
            this.smsClient = smsClient;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetToNumber()
        {
            try
            {
                await this.smsClient.Transactional.SendSmsToNumber("420789456123", "Hello, this is test SMS")
                    .ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                this.logger.LogError(e, $"Failled to send message: {e.Message}");
                throw;
            }
        }
    }
}
