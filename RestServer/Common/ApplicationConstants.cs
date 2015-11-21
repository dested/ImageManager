using System.Configuration;

namespace RestServer.Common
{
    public static class ApplicationConstants
    {
        public static class API
        {
            public static string Version { get { return "v1"; } }
            public static string BaseUrl { get { return ConfigurationManager.AppSettings["api:BaseURL"]; } }
        }
         
        public static class AWS
        {
            public static string AccessKey { get { return ConfigurationManager.AppSettings["aws:accesskey"]; } }
            public static string SecretKey { get { return ConfigurationManager.AppSettings["aws:secretkey"]; } }
       
        } 
        public static class Application
        {
            public static string ServerMode { get { return ConfigurationManager.AppSettings["ServerMode"]; } }
            public static bool IsProduction { get { return ServerMode == "Production"; } }
            public static string DefaultInbound { get { return ConfigurationManager.AppSettings["DefaultInbound"]; } }
            public static string DefaultRedirect { get { return ConfigurationManager.AppSettings["DefaultRedirect"]; } }
            public static string BaseUrl { get { return ConfigurationManager.AppSettings["BaseURL"]; } }
        } 
    }
}