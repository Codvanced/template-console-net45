using Fclp;
using System;
using IOC.FW.ContainerManager.DryIoc;
using IOC.FW.Core.Abstraction.Container.Binding;
using NAME_REPLACE.Binding;
using DryIoc;

namespace NAME_REPLACE.ConsoleApp
{
    class Program
    {
        public static readonly string ExecutableName = AppDomain.CurrentDomain.FriendlyName;
        public static IContainer Container;

        static void ConfigureContainer()
        {
            var adapter = new DryIocAdapter();

            var binders = new IBinding[]{
                new BusinessBinder(),
                new DaoBinder(),
                new SharedBinder(),
                new FrameworkBinder(),
            };

            foreach (var binder in binders)
                binder.SetBinding(adapter);

            Container = adapter._container;
        }
        
        static void Main(string[] args)
        {
            ConfigureContainer();
            var parser = new FluentCommandLineParser();

            parser
                .SetupHelp("?", "help")
                .Callback(text => Console.WriteLine(text))
                .WithHeader("MENU DA APLICAÇÃO");

            parser
                .HelpOption
                .ShowHelp(parser.Options);

            /* SETUP THE PARSING CRITERIA */
            var id = 0;
            parser
                .Setup<int>('r', "record-id")
                .WithDescription("this is a test for record id")
                .Callback(record => id = record)
                .SetDefault(0);

            var parsingResult = parser.Parse(args);

            if (!parsingResult.HasErrors)
            {
                // DO SOMETHING HERE!!
                // var business = Container.Resolve<ISampleBusiness>();
            }
        }
    }
}
