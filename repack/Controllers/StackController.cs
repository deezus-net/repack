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
    public class StackController : Controller
    {
        private readonly StackModel _stackModel;
        private readonly LogModel _logModel;

        public StackController(Entities.Db db)
        {
            _stackModel = new StackModel(db);
            _logModel = new LogModel(db);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var vModel = new StackViewModel
            {
                Stacks = await _stackModel.GetList(),
                BaseURL = $"{Request.Scheme}://{Request.Host.ToUriComponent()}{Request.PathBase.ToUriComponent()}"
            };
            return View(vModel);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Log(int id)
        {
            var vModel = new StackViewModel { Logs = await _logModel.GetReceivedLog(id, 10) };
            return View(vModel);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var vModel = new StackViewModel
            {
                BaseURL = $"{Request.Scheme}://{Request.Host.ToUriComponent()}{Request.PathBase.ToUriComponent()}",
                Stack = (await _stackModel.Get(id)) ?? new Stack()
            };
            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, StackViewModel vModel)
        {
            vModel.Stack.Id = id;
            var result = false;
            if (vModel.Delete)
            {
                result = await _stackModel.Delete(id);
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    vModel.ErrorMessages = ModelState.Where(s => s.Value.Errors.Count > 0)
                        .ToDictionary(s => s.Key, s => s.Value.Errors.Select(e => e.ErrorMessage).ToList());
                    return View(vModel);
                }
                result = await _stackModel.Update(vModel.Stack);
            }

            if (result)
            {
                return Redirect("~/stack/");
            }

            

            return View(vModel);
        }
    }
}