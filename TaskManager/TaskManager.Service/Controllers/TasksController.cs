using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.BusinessLayer;
using TaskManager.Model;

namespace TaskManager.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly IManageTask manageTask;
        public TasksController(IManageTask manageTask)
        {
            this.manageTask = manageTask;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IEnumerable<TaskDetail>> GetAllAsync()
        {
            return await manageTask.ViewTasksAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<TaskDetail> GetAsync(int id)
        {
            return await manageTask.GetTaskAsync(id);
        }
        
        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]TaskDetail taskDetail)
        {
            if (taskDetail == null)
                return BadRequest();

            await manageTask.AddTaskAsync(taskDetail);

            return Ok("Task Added Successfully and the new task id is " + taskDetail.Id);
        }
        
        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody]TaskDetail taskDetail)
        {
            if (taskDetail == null || id != taskDetail.Id)
                return BadRequest();
            if (taskDetail.EndTask && !manageTask.IsTaskValidToClose(taskDetail))
                return BadRequest("You can not close this task as the task have child tasks");

            await manageTask.EditTaskAsync(id, taskDetail);

            return Ok("Task Updated Successfully for the task name " + taskDetail.Name);

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return BadRequest("Deleting Task is not accessible");
        }
    }
}
