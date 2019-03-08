using System;
using System.Threading.Tasks;
using Xunit;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Table;
using aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library.DataAccess;
using Shouldly;
using Moq;

namespace aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library.Tests
{
    public class GivenTeacherId
    {
        private readonly Guid familyId = Guid.Parse("49def112-2efd-41ff-99cf-d905d94aa6d2");

        // [Fact]
        // public async Task All_pupil_lessons_are_returned()
        // {
        //     const string teacherId = "2f7ddf91-fc49-40a6-b089-7839ec1699d5";

        //     var tableReader = new Mock<ITableReader>();
        //     const string pupilId = "49def112-2efd-41ff-99cf-d905d94aa6d2";
        //     tableReader.Setup(r => r.Get<Family>("Lessons", teacherId, pupilId, (p,rr) => TableOperation.Retrieve<Family>(p, rr) ))


        //     var reader = new FamilyReader(tableReader.Object);

        //     var pupils = await reader.GetPupilsAsync(
        //         teacherId,
        //         pupilId);

        //     pupils.ShouldNotBeEmpty();
        // }
    }
}