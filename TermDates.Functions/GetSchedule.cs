using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using System.Linq;
using AutofacOnFunctions.Services.Ioc;
using System.Collections.Generic;
using System.Net.Http;

namespace TermDates.Functions
{
    public static class IngestMercerDurableFunction
    {
        [FunctionName("ChargeForTermDurableFunctionTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "schedule")] HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            var eventData = await req.Content.ReadAsAsync<ScheduleRequest>();
            string instanceId = await starter.StartNewAsync("ChargeForTerm", eventData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        //[FunctionName("ChargeForTermDurableFunctionTrigger")]
        //public static async Task<IActionResult> RunFromTimer(
        //    [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "schedule")] HttpRequest req,
        //    [OrchestrationClient] DurableOrchestrationClient starter,
        //    [Inject] ILogger log
        //    )
        //{
        //    //loggingContext.CorrelationId = Guid.NewGuid().ToString();

        //    //logger.Info($"Timer trigger function executed at: {DateTime.Now}", loggingContext);

        //    //await starter.StartNewAsync(FunctionNames.MercerIngestOrchestrationFunction, null);

        //    //logger.Info($"Finished MercerOrchestrationFunction", loggingContext);

        //    log.LogInformation("C# HTTP trigger function processed a request.");

        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    var request = JsonConvert.DeserializeObject<ScheduleRequest>(requestBody);

        //    //var schedule = scheduler.Generate(term, lessonDefinitions);

        //    var instance = await starter.StartNewAsync("ChargeForTerm", request);

        //    await starter.GetStatusAsync(instance);

        //    starter.

        //}

        [FunctionName("ChargeForTerm")]
        public static async Task<Schedule> ChargeForTermFunctionAsync([OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var request = context.GetInput<ScheduleRequest>();

            var term = new Term()
            {
                Start = request.TermStart,
                End = request.TermEnd
            };

            var lessonDefinitions = request.Lessons.Select(
                l => new LessonDefinition(l.LessonDay, l.Duration, l.WeeksPerLesson));

            //log.LogInformation(term.Start.ToLongDateString());

            var s = await context.CallActivityAsync<Schedule>("GetSchedule", (term, lessonDefinitions));
            return s;
        }
    }


    public static class GetSchedule
    {

        [FunctionName("GetSchedule")]
        public static async Task<Schedule> GetScheduleAsync(
            [ActivityTrigger] (Term term, IEnumerable<LessonDefinition> lessons) termLessons,
            [Inject] Scheduler scheduler)
        {
            //var s = await Task.Run( () => scheduler.Generate(termLessons.term, termLessons.lessons) );
            //return s;

            //return new Schedule() { Lessons = new List<Lesson>() { new Lesson() { Day = DayOfWeek.Monday, Duration = new TimeSpan(0, 30, 0), Shared = false, WeeksPerLesson = 1 } } };
            return scheduler.Generate(termLessons.term, termLessons.lessons);
        }

        //[FunctionName("GetSchedule")]
        //public static async Task<Schedule> Run(
        //    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "schedule")] HttpRequest req,
        //    [Inject] Scheduler scheduler,
        //    ILogger log)
        //{
        //    log.LogInformation("C# HTTP trigger function processed a request.");
                        
        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        //    var request = JsonConvert.DeserializeObject<ScheduleRequest>(requestBody);

        //    var term = new Term()
        //    {
        //        Start = request.TermStart,
        //        End = request.TermEnd
        //    };

        //    var lessonDefinitions = request.Lessons.Select(
        //        l => new LessonDefinition(l.LessonDay, l.Duration, l.WeeksPerLesson));

        //    log.LogInformation(term.Start.ToLongDateString());

        //    var schedule = scheduler.Generate(term, lessonDefinitions);

        //    return new OkObjectResult(JsonConvert.SerializeObject(schedule));
        //}
    }
}
