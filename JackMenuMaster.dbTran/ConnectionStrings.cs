using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


//===============================================================
#region Manuel
//---------------------------------------------------------------
//
//  Update ApplicationDbContext ทุกครั้งที่ Update Database
//  -------------------
//  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//  {
//    if (!optionsBuilder.IsConfigured)
//    {
//       var connectionStrings = new TrainingCenter.Services.AppSettingReader().LoadClass<ConnectionStrings>("appsettings.db.json", "ConnectionStrings");
//      optionsBuilder.UseOracle(connectionStrings?.Configuration ?? "");
//    }
//  }
//
//  Example : Powershell Command
//  ------------------------------------------
//


//dotnet ef dbcontext scaffold "User Id=voteadmin;Password=voteadmin;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.9.206.148)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=t1it148)));" Oracle.EntityFrameworkCore -o Models -c ApplicationDbContext --project TrainingCenter.DB.csproj

// *********************************
// Last Updated : 20231221 by Jakkit
// *********************************
#endregion
//===============================================================

namespace JackMenuMaster.dbTran
{
    public class ConnectionStrings
    {
        public string? Configuration { get; set; }
    }

}
