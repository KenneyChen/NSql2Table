using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NSql2Table
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var regex = new Regex(@"order\s+by\s+([\s|\S]+)(desc|asc)",RegexOptions.IgnoreCase);
            //var test= regex.Match("select * from order  by  id  desc  ");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
