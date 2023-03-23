using AltFuture.CoinMarketCapAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AltFuture.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoinMarketCapController : Controller
    {
        private readonly ICoinMarketCapAPI _cmcAPI;

        public CoinMarketCapController(ICoinMarketCapAPI cmcAPI)
        {
            _cmcAPI = cmcAPI;
        }

        // GET: CoinMarketCapController
        public async Task<ActionResult> Index()
        {
            var keyInfo = await _cmcAPI.GetKeyInfoAsync();
            return View(keyInfo);
        }


       
    }
}
