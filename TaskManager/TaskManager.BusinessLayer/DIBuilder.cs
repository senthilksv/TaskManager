using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataAccessLayer;

namespace TaskManager.BusinessLayer
{
    public static class DIBuilder
    {
        public static void Build(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddEntityFrameworkSqlServer().
                AddDbContext<TaskDbContext>(option => option.UseSqlServer("Server = DOTNET; Database = TaskDb; Trusted_Connection = True;"));

        }
    }
}
