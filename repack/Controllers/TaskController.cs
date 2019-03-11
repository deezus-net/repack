using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using repack.Entities;
using repack.Models;
using repack.ViewModels;
using Task = repack.Entities.Task;

namespace repack.Controllers
{
    [Authorize]
    [Route("task")]
    public class TaskController : Controller
    {
        private readonly TaskModel _taskModel;
        private readonly LogModel _logModel;

        public TaskController(Entities.Db db)
        {
            _taskModel = new TaskModel(db);
            _logModel = new LogModel(db);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("{stackId}")]
        public async Task<IActionResult> Index(int stackId)
        {
            var vModel = new TaskViewModel { StackId = stackId, Tasks = await _taskModel.GetList(stackId)};
            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("log/{stackId}/{id}")]
        public async Task<IActionResult> Log(int stackId, int id)
        {
            var vModel = new TaskViewModel { StackId = stackId, Logs = await _logModel.GetSentLog(id) };
            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("edit/{stackId}/{id?}")]
        public async Task<IActionResult> Edit(int stackId, int id)
        {
            var vModel = new TaskViewModel {StackId = stackId, Id = id, Task = (await _taskModel.Get(id)) ?? new Task() { }};
            vModel.TaskContent = JsonConvert.DeserializeObject<TaskContent>(vModel.Task.Content ?? "");
            return View(vModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vModel"></param>
        /// <returns></returns>
        [HttpPost("edit/{stackId}/{id?}")]
        public async Task<IActionResult> Edit(TaskViewModel vModel)
        {
            vModel.Task.Id = vModel.Id;
            vModel.Task.StackId = vModel.StackId;
            if (!ModelState.IsValid) return View(vModel);
            var result = false;
            if (vModel.Delete)
            {
                result = await _taskModel.Delete(vModel.Id);
            }
            else
            {
                vModel.Task.Content = JsonConvert.SerializeObject(vModel.TaskContent);
                result = await _taskModel.Update(vModel.Task);
            }

            if (result)
            {
                return Redirect($"~/task/{vModel.StackId}");
            }
            return View(vModel);
        }
    }
}