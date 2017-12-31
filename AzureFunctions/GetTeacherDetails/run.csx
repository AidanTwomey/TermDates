#r "Newtonsoft.Json"
#r "TermDates.Library.dll"
#r "System.Data.SqlClient"

using System;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using aidantwomey.src.Azure.Functions.TermDates.Library;

public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    var results = new List<Teacher>();

    var connString = "";
    using (SqlConnection conn = new SqlConnection(connString))
    {
        conn.Open();
        var text = "select * from TestTable";

        using (SqlCommand command = new SqlCommand(text, conn))
        {
            SqlDataReader reader = command.ExecuteReader();
            
            try
            {
                while (reader.Read())
                {
                    results.Add(new Teacher(){Id = reader.GetInt32(0)});
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
