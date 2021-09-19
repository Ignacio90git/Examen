using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Examen.Startup))]
namespace Examen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
