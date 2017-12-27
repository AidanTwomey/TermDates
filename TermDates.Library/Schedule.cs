namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System.Collections.Generic;
    using System.Linq;

    public class Schedule
    {
        public IEnumerable<Lesson> Lessons { get;set;}

        public override string ToString()
        {
            return Lessons.Aggregate( "", (agg,l) => agg + l.ToString() + '\n' );
        }
    }
}