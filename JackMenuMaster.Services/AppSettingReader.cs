//===============================================================
#region Manuel
//---------------------------------------------------------------
//
//  Example : สร้าง Class
//  -------------------
//  public class LoggerSetting
//  {
//    public string? Location { get; set; }
//  }
//
//  Example : เรียกใช้
//  ---------------
//  loggerSetting = new AppSettingReader().LoadClass<LoggerSetting>("appsettings.sys.json", "LoggerSetting");
//
// *********************************
// Last Updated : 20231221 by Jakkit
// *********************************
#endregion
//===============================================================

namespace JackMenuMaster.Services
{
    public class AppSettingReader
    {
        public AppSettingReader()
        {
        }

        public T LoadClass<T>(string appsettingsJsonFile, string key) where T : class, new()
        {
            IConfiguration config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile(appsettingsJsonFile).Build();

            var myresp = config.GetSection(key).Get<T>() ?? new T();
            return myresp;
        }
    }
}
