using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library.DataAccess;

namespace aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library
{
    public class Family : TableEntity
    {
        public IEnumerable<Pupil> Pupils { get;set;}
    }

    public class FamilyReader
    {
        private readonly ITableReader tableReader;

        public FamilyReader(ITableReader tableReader)
        {
            this.tableReader = tableReader;
        }

        public async Task<IEnumerable<Pupil>> GetPupilsAsync(string teacherId, string familyId)
        {
            var family = await tableReader.Get<Family>("Lessons", teacherId, familyId, (p,r) => TableOperation.Retrieve<Family>(p, r));

            return family.Pupils;
        }
    }
}