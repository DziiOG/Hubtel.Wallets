using Microsoft.AspNetCore.Mvc;

namespace Hubtel.Wallets.Api.Controllers
{
    public class WalletController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
