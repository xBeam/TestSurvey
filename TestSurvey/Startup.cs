using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestSurvey.Startup))]
namespace TestSurvey
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
