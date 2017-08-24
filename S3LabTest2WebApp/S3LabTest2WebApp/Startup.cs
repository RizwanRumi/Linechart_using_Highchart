using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S3LabTest2WebApp.Startup))]
namespace S3LabTest2WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
