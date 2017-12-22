using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Ranking.Services.Users;
using Ranking.DataProviders.Users;
using Ranking.DataProviders.Scores;

namespace Ranking.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var _connection = Configuration.GetSection("ConnectionString").Value;
            services.AddMvc();
            services.AddScoped<IUsersDataProvider, UsersDataProvider>(s => new UsersDataProvider(_connection));
            services.AddTransient<IScoreDataProvider, ScoreDataProvider>(s => new ScoreDataProvider(_connection));
            services.AddTransient<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            //   app.UseSwagger();
        }
    }
}
