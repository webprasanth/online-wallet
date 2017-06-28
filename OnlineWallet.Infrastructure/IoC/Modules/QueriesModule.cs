using Autofac;
using OnlineWallet.Infrastructure.Queries;

namespace OnlineWallet.Infrastructure.IoC.Modules
{
    public class QueriesModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public QueriesModule(string queriesConnectionString)
        {
            QueriesConnectionString = queriesConnectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TransactionQueries(QueriesConnectionString))
                .As<ITransactionQueries>()
                .InstancePerLifetimeScope();
        }
    }
}