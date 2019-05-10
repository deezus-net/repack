using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using repack.Entities;
using repack.Models;
using repack.ViewModels;

namespace repack.Controllers
{
    [Authorize]
    public class LogController : Controller
    {
        private readonly SystemLogModel _systemLogModel;

        public LogController(Db db)
        {
            _systemLogModel = new SystemLogModel(db);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(LogViewModel vModel)
        {
            vModel.SystemLogs = await _systemLogModel.Search(vModel.Type, vModel.From, vModel.To);
            return View(vModel);
        }
        
        
    }
}