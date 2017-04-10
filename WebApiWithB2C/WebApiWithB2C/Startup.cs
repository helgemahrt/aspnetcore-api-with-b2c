using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApiWithB2C.Configuration;

namespace WebApiWithB2C
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            MyConfigManager.Instance.LoadConfiguration(env);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(MyConfigManager.Instance.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            string ClientId = MyConfigManager.Instance.Configuration["AzureAD:ClientId"];
            string AadInstance = MyConfigManager.Instance.Configuration["AzureAD:AadInstance"];
            string Tenant = MyConfigManager.Instance.Configuration["AzureAD:Tenant"];
            string SignInPolicyId = MyConfigManager.Instance.Configuration["AzureAD:SignInPolicyId"];

            app.UseJwtBearerAuthentication(new JwtBearerOptions()
            {
                MetadataAddress = string.Format(AadInstance, Tenant, SignInPolicyId),
                Audience = ClientId,
            });

            app.UseMvc();
        }
    }
}
