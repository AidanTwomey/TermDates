using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using AutofacOnFunctions.Services.Ioc;
using System.Collections.Generic;

namespace TermDates.Functions
{


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
