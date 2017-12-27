namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Scheduler
    {
        public static Schedule Generate(Term term, IEnumerable<DayOfWeek> days)
        {
            return Generate(term, days, 1, new NullBreak());
        }

        public static Schedule Generate(Term term, IEnumerable<DayOfWeek> days, int weeksPerLesson)
        {
            return Generate(term, days, weeksPerLesson, new NullBreak());
        }

        public static Schedule Generate(Term term, IEnumerable<DayOfWeek> days, params TermBreak[] breaks)
        {
            return Generate(term, days, 1, breaks);
        }

        public static Schedule Generate(Term term, IEnumerable<DayOfWeek> days, int weeksPerLesson, params TermBreak[] breaks)
        {
            return new Schedule(){ Lessons = days.SelectMany( d => GetLessonsScedule(term, d, breaks, weeksPerLesson) ) };
        }

        private static IEnumerable<Lesson> GetLessonsScedule(Term term, DayOfWeek day, IEnumerable<TermBreak> breaks, int weeksPerLesson = 1)
        {
            DateTime firstDate = term.Start;
            int daysBetweenLessons = 7 * weeksPerLesson;

            while (firstDate.DayOfWeek != day)
            {
                firstDate = firstDate.AddDays(1);
            }

            yield return new Lesson(){Start = firstDate};

            DateTime nextDate = firstDate.AddDays(daysBetweenLessons);
            
            while (nextDate <= term.End ) 
            {
                if ( !(breaks.Any(b => b.WithinBreak(nextDate) )) )
                {
                    yield return new Lesson(){Start = nextDate};
                }
                nextDate = nextDate.AddDays(daysBetweenLessons);
            }
        }
    }
}