using System;
using Xunit;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Moq;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using Microsoft.Azure.WebJobs;
using System.Threading;

[assembly: LightBddScope]
namespace TermDates.Functions.Acceptance.Tests
{
    public class MockDurableOrchestrationContext : DurableOrchestrationContextBase
    {
        public override ScheduleRequest GetInput<ScheduleRequest>()
        {
            var scedule = new ScheduleRequest()
            {
                // TermStart = new DateTime(2019, 1, 4),
                // TermEnd = new DateTime(2019, 4, 5)
            };
            
            return scedule;
        }

        public override DateTime CurrentUtcDateTime { get; }
        public override Guid NewGuid() { throw new NotImplementedException(); }
        public override Task<TResult> CallActivityWithRetryAsync<TResult>(string functionName, RetryOptions retryOptions, object input)
        { throw new NotImplementedException(); }
        public override Task<TResult> CallSubOrchestratorAsync<TResult>(string functionName, string instanceId, object input)
        { throw new NotImplementedException(); }
        public override Task<TResult> CallSubOrchestratorWithRetryAsync<TResult>(string functionName, RetryOptions retryOptions, string instanceId, object input)
        { throw new NotImplementedException(); }
        public override Task<T> CreateTimer<T>(DateTime fireAt, T state, CancellationToken cancelToken)
        { throw new NotImplementedException(); }
        public override Task<T> WaitForExternalEvent<T>(string name)
        { throw new NotImplementedException(); }
        public override Task<T> WaitForExternalEvent<T>(string name, TimeSpan timeout)
        { throw new NotImplementedException(); }
        public override Task<T> WaitForExternalEvent<T>(string name, TimeSpan timeout, T defaultValue)
        { throw new NotImplementedException(); }
        public override void ContinueAsNew(object input)
        { throw new NotImplementedException(); }
        public override void SetCustomStatus(object customStatusObject)
        { throw new NotImplementedException(); }
        public override Task<TResult> CallActivityAsync<TResult>(string functionName, object input)
        { throw new NotImplementedException(); }
    }
    public partial class Invoice_feature : FeatureFixture
    {
        private int lessonsPerWeek;

        private async Task Given_pupil_has_one_lesson_per_week() { lessonsPerWeek = 1; }

        private async Task When_customer_buys_product(string product)
        {
            var scheduleRequest = new ScheduleRequest()
            {
                TermStart = new DateTime(2019, 1, 4),
                TermEnd = new DateTime(2019, 4, 5)
            };

            var context = new MockDurableOrchestrationContext();

            // var logger = Mock.Of<ILogger>();
            // var request = new HttpRequestMessage(){Content = new StringContent(JsonConvert.SerializeObject(scheduleRequest))};
            // var response = await IngestMercerDurableFunction.Run(request, new DurableOrchestrationClient(), null);

            var schedule = await IngestMercerDurableFunction.ChargeForTermFunctionAsync(context);
            System.Console.WriteLine(schedule);
        }

        private async Task Then_an_invoice_should_be_sent_to_the_customer() { /* ... */ }

        private async Task Then_the_invoice_should_contain_product_with_price_of_AMOUNT(string product, int amount)
        { /* ... */ }
        /* ... */
    }
}

namespace TermDates.Functions.Acceptance.Tests
{
    [FeatureDescription(
    @"In order to automate payments
    As a teacher
    I want to charge a parent for a term of lessons")] //feature description
    [Label("Story-2")]
    public partial class Invoice_feature //feature name
    {
        [Scenario]
        [Label("Ticket-2")]
        public void Creating_invoice_for_family() //scenario name
        {
            Runner.RunScenarioAsync(

                _ => Given_pupil_has_one_lesson_per_week(), //steps
                                                            // _ => Given_product_is_available_in_product_storage("wooden shelf"),
                                                            // _ => When_customer_buys_product("wooden desk"),
                _ => When_customer_buys_product("wooden shelf"),
                // _ => Then_an_invoice_should_be_sent_to_the_customer(),
                // _ => Then_the_invoice_should_contain_product_with_price_of_AMOUNT("wooden desk", 62),
                _ => Then_the_invoice_should_contain_product_with_price_of_AMOUNT("wooden shelf", 37));
        }
    }
}
