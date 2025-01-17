﻿using Autofac;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Application.Queries.Implementations;
using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.Mappers;
using PreContracts.Api.Application.Queries.Querys;
using PreContracts.Api.Domain.Aggregates.LogContractAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractLogAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using PreContracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using PreContracts.Api.Repository.Repositories;
using Contracts.Aplication.Queries.Implementations;
using Contracts.Aplication.Queries.Interfaces;
using Contracts.Aplication.Queries.Mappers;
using Contracts.Application.Queries.Implementations;
using Contracts.Application.Queries.Interfaces;
using Contracts.Application.Queries.Mappers;
using Contracts.Repository;
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
        private readonly string _addressSender;
        private readonly string _bucketNamePreContractExcel;
        private readonly string _bucketNamePrecontractJson;
        private readonly string _bucketNamePreContractTemplate;

        public ApplicationModule(string connectionString,
                                string timeZone,
                                string addressSender, 
                                 string bucketNamePreContractExcel,
                                string bucketNamePreContractJson,
                                string bucketNamePreContractTemplate) 
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _timeZone = timeZone ?? throw new ArgumentNullException(nameof(timeZone));
            _addressSender = addressSender ?? throw new ArgumentNullException(nameof(addressSender));
            _bucketNamePreContractExcel = bucketNamePreContractExcel ?? throw new ArgumentNullException(nameof(bucketNamePreContractExcel));
            _bucketNamePrecontractJson = bucketNamePreContractJson ?? throw new ArgumentNullException(nameof(bucketNamePreContractJson));
            _bucketNamePreContractTemplate = bucketNamePreContractTemplate ?? throw new ArgumentNullException(nameof(bucketNamePreContractTemplate));

        }

        protected override void Load(ContainerBuilder builder)
        {
            #region Transversal

            builder.Register(c => new Connection(_connectionString)).As<IConnection>().InstancePerLifetimeScope();

            builder.Register(c => new ValuesSettings(_timeZone, _addressSender, _bucketNamePreContractExcel, _bucketNamePrecontractJson, _bucketNamePreContractTemplate)).As<IValuesSettings>().InstancePerLifetimeScope();
            builder.Register(c => new ValuesSettingsApi(_timeZone)).As<IValuesSettingsApi>().InstancePerLifetimeScope();

            builder.Register(c => new ValuesSettingsLambda("", "", "", "")).As<IValuesSettingsLambda>().InstancePerLifetimeScope();
            builder.Register(c => new QueryHandler(_connectionString)).As<IQueryHandler>().InstancePerLifetimeScope();

            #endregion

            #region Queries

            builder.RegisterType<PreContractVariableCommissionRangeQuery>()
    .As<IPreContractVariableCommissionRangeQuery>()
    .InstancePerLifetimeScope();

            builder.RegisterType<PreContractLogDetailQuery>()
      .As<IPreContractLogDetailQuery>()
      .InstancePerLifetimeScope();

            builder.RegisterType<PreContractBankAccountQuery>()
                .As<IPreContractBankAccountQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractFixedCommissionRangeQuery>()
                .As<IPreContractFixedCommissionRangeQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractEconomicConditionQuery>()
    .As<IPreContractEconomicConditionQuery>()
    .InstancePerLifetimeScope();

            builder.RegisterType<CategoryQueryHandler>()
                .As<ICategoryQueryHandler>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerQueryHandler>()
                .As<ICustomerQueryHandler>()
                .InstancePerLifetimeScope();
            

            builder.RegisterType<PreContractQuery>()
                .As<IPreContractQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractLogQuery>()
                .As<IPreContractLogQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractTradenameQuery>()
                .As<IPreContractTradenameQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractLogDetailQuery>()
                    .As<IPreContractLogDetailQuery>()
                    .InstancePerLifetimeScope();


            builder.RegisterType<PreContractVariableCommissionRangeQuery>()
               .As<IPreContractVariableCommissionRangeQuery>()
               .InstancePerLifetimeScope();


            builder.RegisterType<LogContractQuery>()
                .As<ILogContractQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<TradenameQuery>()
                .As<ITradenameQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IdSummaQuery>()
                .As<IIdSummaQuery>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerSummaQuery>()
                .As<ICustomerSummaQuery>()
                .InstancePerLifetimeScope();

            
            #endregion

            #region Mapper

            builder.RegisterType<PreContractTradenameMapper>()
                .As<IPreContractTradenameMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractEconomicConditionMapper>()
                .As<IPreContractEconomicConditionMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractLogMapper>()
                .As<IPreContractLogMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractFixedCommissionRangeMapper>()
                .As<IPreContractFixedCommissionRangeMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractBankAccountMapper>()
                .As<IPreContractBankAccountMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PreContractLogDetailMapper>()
                .As<IPreContractLogDetailMapper>()
                .InstancePerLifetimeScope();



            builder.RegisterType<PreContractMapper>()
               .As<IPreContractMapper>()
               .InstancePerLifetimeScope();

            builder.RegisterType<PreContractVariableCommissionRangeMapper>()
                .As<IPreContractVariableCommissionRangeMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<LogContractMapper>().As<ILogContractMapper>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryMapper>().As<ICategoryMapper>().InstancePerLifetimeScope();
            builder.RegisterType<TradenameMapper>().As<ITradenameMapper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerMapper>().As<ICustomerMapper>().InstancePerLifetimeScope();
            builder.RegisterType<IdSummaMapper>().As<IIdSummaMapper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerSummaMapper>().As<ICustomerSummaMapper>().InstancePerLifetimeScope();
            
            #endregion

            #region Repositoy

            builder.RegisterType<SellerRepository>().As<ISellerRepository>().InstancePerLifetimeScope();


            builder.Register(c => new PreContractRepository(_connectionString))
                .As<IPreContractRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PreContractBankAccountRepository(_connectionString))
                .As<IPreContractBankAccountRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PreContractTradenameRepository(_connectionString))
                .As<IPreContractTradenameRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PreContractVariableCommissionRangeRepository(_connectionString))
                .As<IPreContractVariableCommissionRangeRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PreContractLogRepository(_connectionString))
                .As<IPreContractLogRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PreContractLogDetailRepository(_connectionString))
                .As<IPreContractLogDetailRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new PreContractEconomicConditionRepository(_connectionString))
                .As<IPreContractEconomicConditionRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new LogContractRepository(_connectionString))
                .As<ILogContractRepository>()
                .InstancePerLifetimeScope();

            
            #endregion

            #region Services

            #endregion

            base.Load(builder);
        }

    }
}
