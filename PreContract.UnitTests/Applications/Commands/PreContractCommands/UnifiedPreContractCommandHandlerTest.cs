using Contracts.Api.Application.Commands.PreContractCommands;
using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractAggregate;
using Contracts.Api.Domain.Aggregates.PreContractBankAccountAggregate;
using Contracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using Contracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using Contracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.PreContractCommands
{
    public class UnifiedPreContractCommandHandlerTest
    {
        private readonly UnifiedPreContractCommandHandler _sut;
        private readonly Mock<IPreContractRepository> _IPreContractRepository;
        private readonly Mock<IValuesSettingsApi> _IValuesSettingsApi;

        public UnifiedPreContractCommandHandlerTest()
        {
            this._IPreContractRepository = new Mock<IPreContractRepository>();
            this._IValuesSettingsApi = new Mock<IValuesSettingsApi>();
            this._sut = new UnifiedPreContractCommandHandler(this._IPreContractRepository.Object, this._IValuesSettingsApi.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            UnifiedPreContractCommand command = new UnifiedPreContractCommand();
            command.preContract = new CreatePreContractCommand();
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettingsApi.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractRepository.Setup(x => x.RegisterUnified(It.IsAny<Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract>(), It.IsAny<PreContractBankAccount>(), It.IsAny<List<PreContractTradename>>(),
                                                                 It.IsAny<List<PreContractFixedCommissionRange>>(), It.IsAny<List<PreContractVariableCommissionRange>>(),
                                                                 It.IsAny<List<PreContractEconomicCondition>>())).Returns(Task.FromResult(result));

            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);
            _IPreContractRepository.VerifyAll();

        }

    }
}
