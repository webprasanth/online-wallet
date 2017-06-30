using System.Reflection;
using Autofac;
using OnlineWallet.Infrastructure.Queries;
using OnlineWallet.Infrastructure.Queries.Transactions;
using OnlineWallet.Infrastructure.Queries.Users;

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

            builder.Register(c => new UserQueries(QueriesConnectionString))
                .As<IUserQueries>()
                .InstancePerLifetimeScope();
        }
    }
}