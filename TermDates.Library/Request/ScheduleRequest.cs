namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;
    public class ScheduleRequest
    {
        public DateTime TermStart { get; set;}
        public DateTime TermEnd { get; set;}
        public LessonRequest[] Lessons { get; set;}
    }
}