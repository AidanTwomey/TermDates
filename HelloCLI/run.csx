#r "Newtonsoft.Json"
#r "TermDates.Library.dll"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using aidantwomey.src.Azure.Functions.TermDates.TermDates.Library;


public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = new StreamReader(req.Body).ReadToEnd();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    
    var schedule = new Schedule(){
        Lessons = new[]{ 
            new Lesson(){ Start =  new DateTime(2018,1,3), Duration = 30, Shared = true},
            new Lesson(){ Start =  new DateTime(2018,1,10), Duration = 30, Shared = false},
            new Lesson(){ Start =  new DateTime(2018,1,17), Duration = 30, Shared = false},
            new Lesson(){ Start =  new DateTime(2018,1,24), Duration = 30, Shared = false}
        }
    };

    return new OkObjectResult(JsonConvert.SerializeObject(schedule));
}
