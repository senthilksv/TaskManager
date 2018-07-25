using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Model;

namespace TaskManager.DataAccessLayer
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext taskDbContext;
        public TaskRepository(TaskDbContext taskDbContext)
        {
            this.taskDbContext = taskDbContext;
        }
        public async Task<int> DeleteAsync(TaskDetail entity)
        {
            taskDbContext.Tasks.Remove(entity);

            return await taskDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskDetail>> GetAllAsync()
        {
            return await taskDbContext.Tasks.ToListAsync();
        }

        public async Task<TaskDetail> GetAsync(int id)
        {
            return await taskDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<int> InsertAsync(TaskDetail entity)
        {
            taskDbContext.Tasks.Add(entity);
            return await taskDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(int id,TaskDetail entity)
        {
            //var taskDetail = taskDbContext.Tasks.FirstOrDefault(t => t.Id == id);
            //taskDetail = entity;
            taskDbContext.Update<TaskDetail>(entity);
            return await taskDbContext.SaveChangesAsync();
        }
    }
}
