using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiWithB2C.Configuration
{
    public class MyConfigManager
    {
        public IConfigurationRoot Configuration { get; private set; }

        public static MyConfigManager Instance { get; private set; }

        static MyConfigManager()
        {
            Instance = new MyConfigManager();
        }

        public void LoadConfiguration(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }
    }
}
