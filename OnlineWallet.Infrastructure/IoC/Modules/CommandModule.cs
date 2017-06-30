using System.Reflection;
using Autofac;
using OnlineWallet.Infrastructure.Commands;

namespace OnlineWallet.Infrastructure.IoC.Modules
{
    public sealed class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}