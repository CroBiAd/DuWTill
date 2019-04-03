//using Microsoft.Owin;
//using Owin;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Web.ApplicationServices;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using Microsoft.Extensions.PlatformAbstractions;

//[assembly: OwinStartupAttribute(typeof(WebTilling.Startup))]
namespace WebTilling {
    public partial class Startup {
        //public void Configuration(IAppBuilder app) {
        //    ConfigureAuth(app);
        //}
    }
}
//public class Startup {
//    private IApplicationEnvironment _appEnv;

//    public Startup(IApplicationEnvironment appEnv) {
//        _appEnv = appEnv;
//    }
//    public void ConfigureServices(IServiceCollection services) {
//        services.AddEntityFramework()
//            .AddSqlite()
//            .AddDbContext<MyContext>(
//                options => { options.UseSqlite($"Data Source={_appEnv.ApplicationBasePath}/data.db"); });
//    }
//}