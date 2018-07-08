using HangFire;
using HangFire.SqlServer;
using HangFire.Web;

[assembly: WebActivatorEx.PostApplicationStartMethod(
    typeof(MyDiary.API.HangFireConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(
    typeof(MyDiary.API.HangFireConfig), "Stop")]

namespace MyDiary.API
{
    public class HangFireConfig
    {
        private static AspNetBackgroundJobServer _server;

        public static void Start()
        {
            // Please, visit http://hangfire.io/ for details.

            // HangFire uses persistent data storage to store information
            // about jobs, queues, statistics, etc. 
            // Default implementation uses SQL Server as a storage. You only
            // need to provide connection string to start using HangFire -
            // all database objects will be installed automatically.

            JobStorage.Current = new SqlServerStorage(
                @"Server=.\sqlexpress; Database=MyDatabase; Trusted_Connection=True;");

            // If your project infrastructure contains Redis server, you may
            // choose Redis job storage implementation (it is much faster).
            // To do this, type in your Package Manager Console window:
            //
            // Install-Package HangFire.Redis
            //
            // Then, uncomment the line below and set up your connection.
            // JobStorage.Current = new RedisStorage("localhost:6379", 3);
            
            // HangFire Server processes jobs while your application is being
            // performed. They are performed in a reliable way and your ASP.NET 
            // application will never loose them.

            _server = new HangFire.Web.AspNetBackgroundJobServer();
            _server.Start();
        }

        public static void Stop()
        {
            _server.Stop();
        }
    }
}
