using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;
using Microsoft.AspNetCore.Http;

namespace ToDoAPI
{
    public class Startup
    {
		/// <summary>
		/// interface configuration of a configuration. This configures the configuration in order to configure with an interface.
		/// </summary>
		/// <param name="configuration">interface configuration to be methodically configured through configuration reassignment</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

		/// <summary>
		/// this pairs with the above, like red wine with steak
		/// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

			services.AddMvc();
			services.AddDbContext<ToDoContext>(options => options
			.UseSqlServer(Configuration.GetConnectionString("ProductionConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

				app.UseMvcWithDefaultRoute();
			
			//default
			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("To do list API. Use with a front-end application, Postman or Curl. Endpoints: /api, api/todo, api/todolist, api/todo/{id}, api/todolist{id}");
			});
		}
    }
}
