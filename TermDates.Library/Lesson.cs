namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;
    public class Lesson{

        public DateTime Start { get;set;}
        public TimeSpan Duration { get; set;}
        public bool Shared { get;set;}

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2} mins", 
                Start.ToShortDateString(), 
                Start.ToShortTimeString(), 
                Duration.Minutes);
        }
    }
}