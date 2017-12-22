namespace aidantwomey.src.Azure.Functions.TermDates.TermDates.Library
{
    using System;
    using System.Collections.Generic;

    public class Lesson{

        private TimeSpan _duration;

        public DateTime Start { get;set;}
        public TimeSpan Duration { get { return _duration - TimeSpan.FromMinutes(5);} set { _duration = value;} }
        public bool Shared { get;set;}
    }

    public class Schedule
    {
        public IEnumerable<Lesson> Lessons { get;set;}
    }
}