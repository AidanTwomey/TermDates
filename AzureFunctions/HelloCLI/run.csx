#r "Newtonsoft.Json"
#r "TermDates.Library.dll"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using aidantwomey.src.Azure.Functions.TermDates.Library;

private static Dictionary<int, DayOfWeek> dayMap = new Dictionary<int, DayOfWeek>()
{
    {0, DayOfWeek.Sunday},
    {1, DayOfWeek.Monday},
    {2, DayOfWeek.Tuesday},
    {3, DayOfWeek.Wednesday},
    {4, DayOfWeek.Thursday},
    {5, DayOfWeek.Friday},
    {6, DayOfWeek.Saturday}
};

public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = new StreamReader(req.Body).ReadToEnd();
    var data = JsonConvert.DeserializeObject<ScheduleRequest>(requestBody);

    var term = new Term(){
        Start = data.TermStart, 
        End = data.TermEnd};
    var lessonDefinition = new LessonDefinition(dayMap[data.LessonDay]);

    log.Info(term.Start.ToLongDateString());

    var schedule = Scheduler.Generate(term, new []{lessonDefinition} );

    return new OkObjectResult(JsonConvert.SerializeObject(schedule));
}
