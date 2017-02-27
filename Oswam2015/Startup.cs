using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Oswam2015.Startup))]
namespace Oswam2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
