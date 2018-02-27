#r "Newtonsoft.Json"
#r "TermDates.Library.dll"
#r "System.Data.SqlClient"
#r "Microsoft.Extensions.Configuration"
#r "Microsoft.Extensions.Configuration.FileExtensions"
#r "Microsoft.Extensions.Configuration.Abstractions"
#r "Microsoft.Extensions.Configuration.Json"
#r "Microsoft.Extensions.Configuration.EnvironmentVariables"

using System;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using Microsoft.Extensions.Configuration;

public static IActionResult Run(HttpRequest req, TraceWriter log, ExecutionContext context)
{
    var config = new ConfigurationBuilder()
        .SetBasePath(context.FunctionAppDirectory)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

    var results = new List<Teacher>();

    log.Info(config.GetConnectionString("Teacher"));

    using (SqlConnection conn = new SqlConnection(config.GetConnectionString("Teacher")))
    {
        conn.Open();
        var sql = "select * from TestTable";

        using (SqlCommand command = new SqlCommand(sql, conn))
        {
            SqlDataReader reader = command.ExecuteReader();
            
            try
            {
                while (reader.Read())
                {
                    results.Add(new Teacher(){Id = reader.GetInt32(0), Name = reader[1].ToString()});
                }
            }
            finally
            {
                reader.Close();
            }
        }
    }
    
    return new OkObjectResult(JsonConvert.SerializeObject(results));
}
