using System;
using Xunit;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;

[assembly: LightBddScope]
namespace TermDates.Functions.Acceptance.Tests
{
    public partial class Invoice_feature : FeatureFixture
    {
        private int lessonsPerWeek;

        private void Given_pupil_has_one_lesson_per_week() { lessonsPerWeek = 1; }

        // private void When_customer_buys_product(string product) { /* ... */ }

        private void Then_an_invoice_should_be_sent_to_the_customer() { /* ... */ }

        private void Then_the_invoice_should_contain_product_with_price_of_AMOUNT(string product, int amount)
        { /* ... */ }
        /* ... */
    }
}

namespace TermDates.Functions.Acceptance.Tests
{
    [FeatureDescription(
    @"In order to automate payments
    As a teacher
    I want to charge a parent for a term f lessons")] //feature description
    [Label("Story-2")]
    public partial class Invoice_feature //feature name
    {
        [Scenario]
        [Label("Ticket-2")]
        public void Creating_invoice_for_family() //scenario name
        {
            Runner.RunScenario(

                _ => Given_pupil_has_one_lesson_per_week(), //steps
                // _ => Given_product_is_available_in_product_storage("wooden shelf"),
                // _ => When_customer_buys_product("wooden desk"),
                // _ => When_customer_buys_product("wooden shelf"),
                // _ => Then_an_invoice_should_be_sent_to_the_customer(),
                // _ => Then_the_invoice_should_contain_product_with_price_of_AMOUNT("wooden desk", 62),
                _ => Then_the_invoice_should_contain_product_with_price_of_AMOUNT("wooden shelf", 37));
        }
    }
}
