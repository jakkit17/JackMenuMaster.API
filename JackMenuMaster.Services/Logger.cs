//===============================================================
#region Manuel
//---------------------------------------------------------------
// Initial in Global in program.cs 
//
// Example
// -------
// public class GlobalData
// {
//    public static Logger Log { get; set; } = new Logger(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name ?? "Unknown");
// }   
// 
// Add to start if you want to log IP Address
// Example
// -------
// builder.Services.AddHttpContextAccessor();
// var app = builder.Build();
// GlobalData.Log.SetHttpContextAccessor(app.Services.GetRequiredService<IHttpContextAccessor>());
//
// *********************************
// Last Updated : 20231221 by Jakkit
// *********************************
#endregion
//===============================================================

namespace JackMenuMaster.Services
{
    // Class for appsettings.Services.json
    public class LoggerSetting
    {
        public string? Location { get; set; }
    }

    public class Logger
    {
        private readonly string _appFolder = "c:\\Logs\\"; // Default 
        private IHttpContextAccessor? _httpContextAccessor;

        public Logger(string appFolder)
        {
            var loggerSetting = new AppSettingReader().LoadClass<LoggerSetting>("appsettings.Services.json", "LoggerSetting");

            if (loggerSetting.Location != null)
                _appFolder = loggerSetting.Location + appFolder;
            else
                _appFolder += appFolder;

            // Ensure the specified application folder and its parent folders exist
            if (!Directory.Exists(_appFolder))
            {
                Directory.CreateDirectory(_appFolder);
            }
        }

        public void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        // ===================================================
        // Log
        // ---------------------------------------------------

        public void Write(string message)
        {
            string? clientIpAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();


            string subFolder = DateTime.Now.ToString("yyMM");
            string fileName = DateTime.Now.ToString("yyMMdd");

            string logFolderPath = Path.Combine(_appFolder, subFolder);

            // Ensure the subfolder and its parent folders exist
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            string logFilePath = Path.Combine(logFolderPath, $"{fileName}.log");

            // Append the message to the log file or create a new file if it doesn't exist

            File.AppendAllText(logFilePath, $"{DateTime.Now}({clientIpAddress ?? "No IP"}) - {message}{Environment.NewLine}");

        }
        // ===================================================

    }

}
