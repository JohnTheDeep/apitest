using apitest.Interfaces;
using apitest.Middleware;
using apitest.Models;
using Microsoft.AspNetCore.Mvc;

namespace apitest.Controllers
{
    public class TradeMarkController : Controller
    {
        private readonly IDatabaseManager _databaseManager;
        public TradeMarkController(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }
        [Authorize]
        [HttpGet("/TradeMarks")]
        public async Task<IActionResult> GetTradeMarks(int from = 0, int to = 0)
        {
            return Json(await _databaseManager.GetTradeMarks(from, to));
        }
        [Authorize]
        [HttpPost("/CreateTradeMark")]
        public async Task<IActionResult> CreateTradeMark(string Name)
        {
            if (await _databaseManager.CreateTradeMark(new TradeMark(Name)))
                return Json("Added new trade mark !");
            else
                return Json("Something wrong !");
        }
        [Authorize]
        [HttpDelete("/RemoveTradeMark")]
        public async Task<IActionResult> RemoveTradeMark(int Id)
        {
            if (await _databaseManager.RemoveTradeMark(Id))
                return Json("Removed trade mark !");
            else
                return Json("Something wrong !");
        }
        [Authorize]
        [HttpPut("/UpdateTradeMark")]
        public async Task<IActionResult> UpdateTradeMark(int Id, TradeMark tradeMark)
        {
            if (await _databaseManager.UpdateTradeMark(Id, tradeMark))
                return Json("Updated trade mark !");
            else
                return Json("Something wrong !");
        }
    }
}
