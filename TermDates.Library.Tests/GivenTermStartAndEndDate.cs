using System;
using Xunit;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using System.Linq;

namespace TermDates.Library.Tests
{
    public class GivenTermStartAndEndDate
    {
        private readonly DateTime _start = new DateTime(2018,1,2);
        private readonly DateTime _end = new DateTime(2018,3,29);
        private readonly TermBreak _halfTerm = new TermBreak(new DateTime(2018,2,12), new TimeSpan(7,0,0,0));
        private readonly int _duration = 30;
            
        [Theory]
        [InlineData(11, DayOfWeek.Saturday)]
        [InlineData(23, DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        public void A_schedule_is_generated_excluding_half_term(int expectedLessons, params DayOfWeek[] days)
        {
            var lessons = Scheduler.Generate(new Term(){Start =_start, End = _end}, days.Select(d => new LessonDefinition(d)) , _halfTerm).Lessons;
            Assert.Equal(expectedLessons, lessons.Count() );
        }

        [Theory]
        [InlineData(12, 1, DayOfWeek.Saturday)]
        [InlineData(6, 2, DayOfWeek.Saturday)]
        public void A_schedule_is_generated(int expectedLessons, int weeksPerLesson, params DayOfWeek[] days)
        {
            var lessons = Scheduler.Generate(new Term(){Start =_start, End = _end}, days.Select(d => new LessonDefinition(d, _duration, weeksPerLesson))).Lessons;
            Assert.Equal(expectedLessons, lessons.Count() );
        }

        [Theory]
        [InlineData(10, 1, DayOfWeek.Saturday)]
        [InlineData(10, 2,  DayOfWeek.Thursday, DayOfWeek.Saturday)]
        [InlineData(22, 1,  DayOfWeek.Tuesday, DayOfWeek.Saturday)]
        public void A_schedule_is_generated_when_teacher_takes_a_break(int expectedLessons, int weeksPerLesson, params DayOfWeek[] days)
        {
            var holiday1 = new TermBreak(new DateTime(2018,1,20), new TimeSpan(2,0,0,0));
            var holiday2 = new TermBreak(new DateTime(2018,2,12), new TimeSpan(7,0,0,0));

            var lessons = Scheduler.Generate(new Term(){Start =_start, End = _end}, days.Select(d => new LessonDefinition(d, _duration, weeksPerLesson)), holiday1, holiday2);
            Assert.Equal(expectedLessons, lessons.Lessons.Count() );
            Assert.True( lessons.Lessons.All(l => l.Duration.Minutes == _duration));

            Console.WriteLine(lessons);
        }
    }
}
