using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheCakeMaker.Startup))]
namespace TheCakeMaker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
