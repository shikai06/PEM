using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonalExpense.Startup))]
namespace PersonalExpense
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
