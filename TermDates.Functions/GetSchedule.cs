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

namespace TermDates.Functions
{
    public static class GetSchedule
    {
        [FunctionName("GetSchedule")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "schedule")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
                        
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<ScheduleRequest>(requestBody);

            var term = new Term()
            {
                Start = request.TermStart,
                End = request.TermEnd
            };

            var lessonDefinitions = request.Lessons.Select(
                l => new LessonDefinition(l.LessonDay, l.Duration, l.WeeksPerLesson));

            log.LogInformation(term.Start.ToLongDateString());

            var schedule = Scheduler.Generate(term, lessonDefinitions);

            return new OkObjectResult(JsonConvert.SerializeObject(schedule));
        }
    }
}
