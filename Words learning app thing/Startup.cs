using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Words_learning_app_thing.Startup))]
namespace Words_learning_app_thing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
