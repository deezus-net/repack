using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using repack.Entities;
using repack.Models;
using repack.ViewModels;

namespace repack.Controllers
{
    public class StackController : Controller
    {
        private readonly StackModel _stackModel;

        public StackController(Entities.Db db)
        {
            _stackModel = new StackModel(db);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var vModel = new StackViewModel {Stacks = await _stackModel.GetList()};
            return View(vModel);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var vModel = new StackViewModel {Stack = (await _stackModel.Get(id)) ?? new Stack()};
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
            if (!ModelState.IsValid) return View(vModel);
            var result = false;
            if (vModel.Delete)
            {
                result = await _stackModel.Delete(id);
            }
            else
            {
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