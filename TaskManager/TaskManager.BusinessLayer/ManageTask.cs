using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DataAccessLayer;
using TaskManager.Model;

namespace TaskManager.BusinessLayer
{
    public class ManageTask : IManageTask
    {
        private readonly ITaskRepository taskRepository;
        public ManageTask(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }
            
        public async Task<int> AddTaskAsync(TaskDetail taskDetail)
        {
            return await taskRepository.InsertAsync(taskDetail);
        }

        public async Task<int> EditTaskAsync(int id, TaskDetail taskDetail)
        {
            return await taskRepository.UpdateAsync(id, taskDetail);
        }

        public async Task<IEnumerable<TaskDetail>> ViewTasksAsync()
        {
           return await taskRepository.GetAllAsync();
        }

       public async Task<TaskDetail> GetTaskAsync(int id)
        {
            return await taskRepository.GetAsync(id);
        }

        public bool IsTaskValidToClose(TaskDetail taskDetail)
        {
            var taskCollection = taskRepository.GetAllAsync().Result;
            return !taskCollection.Any(task => task.ParentId == taskDetail.Id);
        }
    }
}
