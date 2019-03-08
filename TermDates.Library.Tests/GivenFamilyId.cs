using System;
using System.Threading.Tasks;
using Xunit;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library.DataAccess;
using Shouldly;

namespace aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library.Tests
{
    public class GivenTeacherId
    {
        private readonly Guid familyId = Guid.Parse("49def112-2efd-41ff-99cf-d905d94aa6d2");

        [Fact]
        public async Task All_pupil_lessons_are_returned()
        {
            var reader = new FamilyReader(new TableReader("UseDevelopmentStorage=true"));

            var pupils = await reader.GetPupilsAsync(
                "2f7ddf91-fc49-40a6-b089-7839ec1699d5", 
                "49def112-2efd-41ff-99cf-d905d94aa6d2");

            pupils.ShouldNotBeEmpty();
        }
    }
}