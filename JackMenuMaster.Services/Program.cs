namespace JackMenuMaster.Services
{
    public class GlobalData
    {
        public static Logger Log { get; set; } = new Logger(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name ?? "Unknown");
    }
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Log IP 
            //------------------------------------------------
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            // Log IP 
            //------------------------------------------------
            GlobalData.Log.SetHttpContextAccessor(app.Services.GetRequiredService<IHttpContextAccessor>());

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}

