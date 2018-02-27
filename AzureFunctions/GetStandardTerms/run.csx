#r "Newtonsoft.Json"
#r "TermDates.Library.dll"


using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using aidantwomey.src.Azure.Functions.TermDates.Library;

public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    var terms = new[] {
        new Term(){Start = new DateTime(2018,1,2), End = new DateTime(2018,3,30)},
        new Term(){Start = new DateTime(2018,4,16), End = new DateTime(2018,7,20)}
    };

    return new OkObjectResult(JsonConvert.SerializeObject(terms));
}
