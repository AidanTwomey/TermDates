namespace aidantwomey.src.Azure.Functions.TermDates.TermDates.Library
{
    using System;
    using System.Collections.Generic;

    public class Lesson{

        public DateTime Start { get;set;}
        public TimeSpan Duration { get; set;}
        public bool Shared { get;set;}
    }

    public class Schedule
    {
        public IEnumerable<Lesson> Lessons { get;set;}
    }
}