using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using repack.Classes;
using repack.Entities;
using repack.Models;
using repack.ViewModels;

namespace repack.Controllers
{
    [Authorize(Roles = Define.AccountType.Administrator)]
    public class UserController : Controller
    {
        private readonly UserModel _userModel;

        public UserController(Entities.Db db, AppSetting appSetting)
        {
            _userModel = new UserModel(db, appSetting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var vModel = new UserViewModel {Users = await _userModel.GetList()};
            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var vModel = new UserViewModel {User = (await _userModel.Get(id)) ?? new User()};
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
            var result = false;
            if (vModel.Delete)
            {
                result = await _userModel.Delete(id);
            }
            else
            {
                if (vModel.User.Id == 0)
                {
                    if (string.IsNullOrWhiteSpace(vModel.User.Password))
                    {
                        ModelState.AddModelError("User.Password", "RequiredUserPassword");
                        result = false;
                    }

                    if (!(await _userModel.CheckUserName(vModel.User)))
                    {
                        ModelState.AddModelError("User.Name", "DuplicateLoginID");
                        result = false;
                    }
                }


                if (!ModelState.IsValid)
                {
                    vModel.ErrorMessages = ModelState.Where(s => s.Value.Errors.Count > 0)
                        .ToDictionary(s => s.Key, s => s.Value.Errors.Select(e => e.ErrorMessage).ToList());
                    return View(vModel);
                }

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