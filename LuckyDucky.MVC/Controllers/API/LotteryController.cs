
using Microsoft.AspNetCore.Mvc;


namespace LuckyDucky.MVC.Controllers.API
{
    [ApiController]
    public class LotteryController : ControllerBase
    {
        
        [Route("CheckLottery/{email?}")]
        [Route("api/[controller]/{email?}")]
        [HttpGet]
        public IActionResult Check(string Email)
        {
            return Ok(new { email = Email, isSubscribed = false, isLottery = false });
        }
    }
}