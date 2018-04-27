#r "Newtonsoft.Json"
#r "TermDates.Library.dll"
#r "Microsoft.Extensions.Configuration"
#r "Microsoft.Extensions.Configuration.FileExtensions"
#r "Microsoft.Extensions.Configuration.Abstractions"
#r "Microsoft.Extensions.Configuration.Json"
#r "Microsoft.Extensions.Configuration.EnvironmentVariables"
#r "Microsoft.WindowsAzure.Storage"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

public class TeacherEntity : TableEntity
{
    public TeacherEntity()
    {
    }

    public double HourlyRate { get;set;}
}

public async static Task<IActionResult> Run(HttpRequest req, TraceWriter log, ExecutionContext context)
{
    var config = new ConfigurationBuilder()
        .SetBasePath(context.FunctionAppDirectory)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

    var id = req.Query["teacherid"];
    var mode = req.Query["mode"];

    var storageAccount = CloudStorageAccount.Parse(config.GetConnectionString("Teacher"));
    CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

    CloudTable table = tableClient.GetTableReference("Teachers");
    var retrieveOperation = TableOperation.Retrieve<TeacherEntity>(id, mode);

    var retrieved = await table.ExecuteAsync(retrieveOperation);

    if (retrieved.Result == null)
    {
        return new NotFoundObjectResult("teacher id not found");
    }

    var teacher = ((TeacherEntity)retrieved.Result);
    
    return new OkObjectResult(JsonConvert.SerializeObject(teacher.HourlyRate));
}
