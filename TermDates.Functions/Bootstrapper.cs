using System.Reflection;
using Autofac;
using AutofacOnFunctions.Services.Ioc;
using Microsoft.Azure.WebJobs;
// using Microsoft.Rest;
// using Runpath.Logging;
// using Runpath.Logging.TableStorage;
using System;

namespace TermDates.Functions
{
    public class Bootstrapper : IBootstrapper
    {
        // public Module[] CreateModules()
        // {
        //     return new Module[]
        //     {
        //         new ServicesModule()
        //     };
        // }
        public Autofac.Module[] CreateModules()
        {
            return new Autofac.Module[]
            {
                new ServicesModule()
            };
        }
    }
}
