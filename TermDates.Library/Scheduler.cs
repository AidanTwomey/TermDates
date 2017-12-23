namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;

    public class Scheduler
    {
        public static Schedule Generate(Term term, DayOfWeek day)
        {
            return new Schedule(){ Lessons = new Lesson[] {
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)},
                new Lesson(){Start = new DateTime(2019,1,6)}
            }};
        }
    }
}