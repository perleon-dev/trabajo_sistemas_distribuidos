using Autofac;
using Customer.Domain.Aggregates.ConnectionBase;
using Customer.Domain.Aggregates.SellerAggregate;
using Customer.Repository.Repositories;
using Customer.Repository.Repositories.ConnectionBase;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Api.Infraestructure.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ApplicationModule : Autofac.Module
    {
        private readonly string _connectionString;
        private readonly string _timeZone;

        public ApplicationModule(string connectionString,
                                 string timeZone) 
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _timeZone = timeZone ?? throw new ArgumentNullException(nameof(timeZone));

        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Transversal

            builder.Register(c => new Connection(_connectionString)).As<IConnection>().InstancePerLifetimeScope();

            #endregion

            #region Queries

            #endregion

            #region Mapper

            #endregion

            #region Repositoy

            builder.RegisterType<SellerRepository>().As<ISellerRepository>().InstancePerLifetimeScope();

            #endregion

            #region Services

            #endregion

            base.Load(builder);
        }

    }
}
