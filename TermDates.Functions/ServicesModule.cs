using System.Reflection;
using Autofac;
// using AutofacOnFunctions.Services.Ioc;

namespace TermDates.Functions
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<MyService>();
        }
    }
}
