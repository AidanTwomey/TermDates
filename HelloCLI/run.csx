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
    // name = name ?? data?.name;

    // return name != null
    //     ? (ActionResult)new OkObjectResult($"Hello, {name}")
    //     : new BadRequestObjectResult("Please pass a name on the query string or in the request body");

    var schedule = new Schedule(){
        Lessons = new[]{ 
            new Lesson(){ Start =  new DateTime(2018,1,3), Duration = 30, Shared = true},
            new Lesson(){ Start =  new DateTime(2018,1,10), Duration = 30, Shared = false}}};

    var jsonToReturn = JsonConvert.SerializeObject(schedule);

    // return new HttpResponseMessage(HttpStatusCode.OK) {
    //     Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
    // };

    return new OkObjectResult(jsonToReturn);
}
