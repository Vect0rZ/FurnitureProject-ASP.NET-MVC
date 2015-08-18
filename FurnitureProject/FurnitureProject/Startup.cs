using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FurnitureProject.Startup))]
namespace FurnitureProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
