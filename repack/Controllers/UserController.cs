using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using repack.Entities;
using repack.Models;
using repack.ViewModels;

namespace repack.Controllers
{
    public class UserController : Controller
    {
        private readonly UserModel _userModel;

        public UserController(Entities.Db db, AppSetting appSetting)
        {
            _userModel = new UserModel(db, appSetting.Salt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var vModel = new UserViewModel { Users = await _userModel.GetList()};
            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var vModel = new UserViewModel { User = (await _userModel.Get(id)) ?? new User()};
            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserViewModel vModel)
        {
            vModel.User.Id = id;
            if (!ModelState.IsValid) return View(vModel);
            var result = false;
            if (vModel.Delete)
            {
                result = await _userModel.Delete(id);
            }
            else
            {
                result = await _userModel.Update(vModel.User);
            }

            if (result)
            {
                return Redirect("~/user/");
            }
            return View(vModel);
        }
    }
}