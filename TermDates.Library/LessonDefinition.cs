namespace aidantwomey.src.Azure.Functions.TermDates.Library
{
    using System;
    public struct LessonDefinition
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan Duration { get; set; }
        public int WeeksPerLesson { get; set; }

        public LessonDefinition(DayOfWeek day)
            :this(day, 0, 1)
        {}

        public LessonDefinition(DayOfWeek day, int duration)
            :this(day, duration, 1)
        {}
        public LessonDefinition(DayOfWeek day, int duration, int weeksPerLesson)
        {
            Day = day;
            Duration = new TimeSpan(0, duration, 0);
            WeeksPerLesson = weeksPerLesson;
        }

        public int DaysBetweenLessons { get { return 7 * WeeksPerLesson;}}
    }
}