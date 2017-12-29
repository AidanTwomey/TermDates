#r "Newtonsoft.Json"
#r "TermDates.Library.dll"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections.Generic;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using System.Linq;

public static IActionResult Run(HttpRequest req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    string requestBody = new StreamReader(req.Body).ReadToEnd();
    var request = JsonConvert.DeserializeObject<ScheduleRequest>(requestBody);

    var term = new Term(){
        Start = request.TermStart, 
        End = request.TermEnd};
        
    var lessonDefinitions = request.Lessons.Select( 
        l => new LessonDefinition(l.LessonDay, l.Duration, l.WeeksPerLesson ));

    log.Info(term.Start.ToLongDateString());

    var schedule = Scheduler.Generate(term, lessonDefinitions );

    return new OkObjectResult(JsonConvert.SerializeObject(schedule));
}
