namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Scheduler
    {
        public static Schedule Generate(Term term, IEnumerable<LessonDefinition> days)
        {
            return Generate(term, days, new NullBreak());
        }

        public static Schedule Generate(Term term, IEnumerable<LessonDefinition> days, params TermBreak[] breaks)
        {
            var lessons = days
                           .SelectMany( d => GetLessonsScedule(term, d, breaks))
                           //.OrderBy(l => l.Start)
                           ;

            return new Schedule(){ Lessons = lessons };
        }

        private static IEnumerable<Lesson> GetLessonsScedule(Term term, LessonDefinition day, IEnumerable<TermBreak> breaks)
        {
            DateTime firstDate = term.Start;

            while (firstDate.DayOfWeek != day.Day)
            {
                firstDate = firstDate.AddDays(1);
            }

            yield return new Lesson(){/* Start = firstDate,*/ Duration = day.Duration};

            DateTime nextDate = firstDate.AddDays(day.DaysBetweenLessons);
            
            while (nextDate <= term.End ) 
            {
                if ( !(breaks.Any(b => b.WithinBreak(nextDate) )) )
                {
                    yield return new Lesson(){/*Start = nextDate, */ Duration = day.Duration};
                }
                nextDate = nextDate.AddDays(day.DaysBetweenLessons);
            }
        }
    }
}