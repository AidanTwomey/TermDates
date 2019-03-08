namespace aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library.Tests
{
    using System;
    using Newtonsoft.Json;
    using aidantwomey.src.Azure.Functions.TermDates.Library;
    using Xunit;
    using Shouldly;

    public class SerialisationTests
    {
        [Fact]
        public void Lesson_Deserialises()
        {
            string serialised = @"{
                    ""Day"": ""Monday"",
                    ""Time"": ""16:00"",
                    ""Duration"": ""00:20:00"",
                    ""WeeksPerLesson"" : 1
                }";

            var lesson = JsonConvert.DeserializeObject<Lesson>(serialised);

            lesson.Duration.Minutes.ShouldBe(20);
        }
    }
}