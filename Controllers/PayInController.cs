using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebService_RealIT.Services;

namespace TestWebService_RealIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayInController : ControllerBase
    {
        private readonly PayInService _payInService;

        public PayInController(PayInService payInService)
        {           
            _payInService = payInService ?? throw new ArgumentNullException(nameof(payInService));
        }

        [HttpPost]
        public async Task<IActionResult> SendPayIn()
        {
            await _payInService.SendPayInRequest();
            return Ok(new { Message = "request sent successfully" });
        }
    }
}
