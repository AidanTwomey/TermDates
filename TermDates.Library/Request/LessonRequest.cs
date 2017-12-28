namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;
    public class LessonRequest
    {
        public int WeeksPerLesson { get; set; }
        public int Duration { get; set;}
        public DayOfWeek LessonDay { get; set;}
    }
}