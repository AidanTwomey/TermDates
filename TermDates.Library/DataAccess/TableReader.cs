using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Threading.Tasks;

namespace aidantwomey.src.Azure.Functions.TermDates.TermDates.TermDates.Library.DataAccess
{
    public interface ITableReader
    {
        Task<T> Get<T>(string tableName, string partitionKey, string rowKey, Func<string, string, TableOperation> operationCreator);
    }

    public class TableReader : ITableReader
    {
        private readonly string connectionString;

        public TableReader(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<T> Get<T>(string tableName, string partitionKey, string rowKey, Func<string, string, TableOperation> operationCreator)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            // TableOperation retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            TableOperation retrieveOperation = operationCreator(partitionKey, rowKey);

            TableResult retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return (T)retrievedResult.Result;
        }
    }
}