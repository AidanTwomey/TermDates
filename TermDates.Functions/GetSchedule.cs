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
            var eventData = await req.Content.ReadAsAsync<ScheduleRequest>();
            string instanceId = await starter.StartNewAsync("ChargeForTerm", eventData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

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
            return scheduler.Generate(termLessons.term, termLessons.lessons);
        }        
    }
}
