using CrudWithPostgreSql.DataAccessLayer;
using CrudWithPostgreSql.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrudWithPostgreSql
{
    class Program
    {
        // install the following from nuget package manager - Microsoft.Extensions.Configuration, Microsoft.Extensions.Configuration.Json
        private static IConfiguration _iconfiguration;
        private static AutoMakesDAL autoMakesDAL;

        static async Task Main(string[] args)
        {
            GetAppSettingsFile();
            await PrintCountries();
        }

        static void GetAppSettingsFile()
        {
            var path = @$"D:{Path.DirectorySeparatorChar}Projects{Path.DirectorySeparatorChar}dotnet{Path.DirectorySeparatorChar}consoleapps{Path.DirectorySeparatorChar}CrudWithPostgreSql";

            var builder = new ConfigurationBuilder()
                                 .SetBasePath(path)
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static async Task PrintCountries()
        {
            autoMakesDAL = new AutoMakesDAL(_iconfiguration);

            //var autoMakesDAL = new AutoMakesDAL(_iconfiguration);

            List<AutoMakeVM> autoMakesData = await autoMakesDAL.GetAutoMakesAsync();

            foreach (var item in autoMakesData)
            {
                Console.WriteLine(item.auto_make_name);
            }

            Console.WriteLine("Press any key to stop.");
            Console.ReadKey();
        }
    }
}
