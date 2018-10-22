using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeSearch.Startup))]
namespace CodeSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
