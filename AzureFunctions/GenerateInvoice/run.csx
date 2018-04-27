#r "Newtonsoft.Json"
#r "TermDates.Library.dll"
#r "Microsoft.Extensions.Configuration"
#r "Microsoft.Extensions.Configuration.FileExtensions"
#r "Microsoft.Extensions.Configuration.Abstractions"
#r "Microsoft.Extensions.Configuration.Json"
#r "Microsoft.Extensions.Configuration.EnvironmentVariables"
#r "Microsoft.Extensions.Configuration.Binder"

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using aidantwomey.src.Azure.Functions.TermDates.Library;

public async static Task<IActionResult> Run(HttpRequest req, TraceWriter log, ExecutionContext context)
{
    var config = new ConfigurationBuilder()
        .SetBasePath(context.FunctionAppDirectory)
        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

    var teacherId = req.Query["teacherId"];
    var mode = req.Query["mode"];

    Dictionary<string, string> values = new Dictionary<string, string>();
    config.GetSection("Values").Bind(values);
    
    log.Info( String.Format("Creating invoice for {0}/{1}.", teacherId, mode));

    using (var client = new HttpClient())
    {
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var uriBuilder = new UriBuilder(values["APP_URL"]);
        uriBuilder.Path = "api/GetTeacherDetails";
        uriBuilder.Query = String.Format("?teacherId={0}&mode={1}", teacherId, mode);

        log.Info(uriBuilder.Uri.AbsoluteUri);

        var teacherDetails = await client.GetAsync(uriBuilder.Uri);
        Teacher teacher = await teacherDetails.Content.ReadAsAsync<Teacher>();

        return new OkObjectResult(teacher);
    }
}
