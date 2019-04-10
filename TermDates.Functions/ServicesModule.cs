using System.Reflection;
using aidantwomey.src.Azure.Functions.TermDates.Library;
using Autofac;
// using AutofacOnFunctions.Services.Ioc;

namespace TermDates.Functions
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new Scheduler()).SingleInstance();
        }
    }
}
