using ASPDotNetApiProxy;
using ASPDotNetApiProxy.DataAccess;
using ASPDotNetApiProxy.Proxies;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ASPDotNetWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //-- 認証トークンの有効期限設定
            AuthSettings.SetAccessTokenValidDuration(AppSettings.AccessTokenValidDuration);
            AuthSettings.SetRequestTokenValidDuration(AppSettings.RequestTokenValidDuration);
            //AuthSettings.SetAccessTokenValidDuration(TimeSpan.Parse(ConfigurationManager.AppSettings["AccessTokenValidDuration"]));
            //AuthSettings.SetRequestTokenValidDuration(TimeSpan.Parse(ConfigurationManager.AppSettings["RequestTokenValidDuration"]));
            
            //-- APIのルートURL設定
            //ApiClient.SetRootUrl(new Uri(ConfigurationManager.AppSettings["RootUri"]));

            //-- DB接続設定
            DbConnector.SetConnectionString(ConfigurationManager.ConnectionStrings["ASPDotNetClient.Properties.Settings.api_dbConnectionString"].ConnectionString);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
