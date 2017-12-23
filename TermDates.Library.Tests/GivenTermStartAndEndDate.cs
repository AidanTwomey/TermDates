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

        [Theory]
        [InlineData(DayOfWeek.Saturday, 1)]
        public void A_schedule_is_generated(DayOfWeek day, int frequency)
        {
            var lessons = Scheduler.Generate(new Term(){Start =_start, End = _end}, day).Lessons;
            Assert.Equal(11, lessons.Count() );
            Assert.Equal(6, lessons.First().Start.Day);
        }
    }
}
