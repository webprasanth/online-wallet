using System.Reflection;
using Autofac;
using OnlineWallet.Infrastructure.Queries;

namespace OnlineWallet.Infrastructure.IoC.Modules
{
    public sealed class QueriesModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public QueriesModule(string queriesConnectionString)
        {
            QueriesConnectionString = queriesConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(QueriesModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .InstancePerLifetimeScope();

            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>()
                .InstancePerLifetimeScope();

            builder.Register(c => new TransactionQueries(QueriesConnectionString))
                .As<ITransactionQueries>()
                .InstancePerLifetimeScope();
        }
    }
}