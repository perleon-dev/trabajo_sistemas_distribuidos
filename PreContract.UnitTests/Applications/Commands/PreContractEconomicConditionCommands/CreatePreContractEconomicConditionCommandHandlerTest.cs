using PreContracts.Api.Application.Commands.PreContractEconomicConditionCommands;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractEconomicConditionAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Commands.PreContractEconomicConditionCommands
{
    public class CreatePreContractEconomicConditionCommandHandlerTest
    {
        private readonly CreatePreContractEconomicConditionCommandHandler _sut;
        private readonly Mock<IPreContractEconomicConditionRepository> _IPreContractEconomicConditionRepository;
        private readonly Mock<IValuesSettingsApi> _IValuesSettingsApi;

        public CreatePreContractEconomicConditionCommandHandlerTest()
        {
            this._IPreContractEconomicConditionRepository = new Mock<IPreContractEconomicConditionRepository>();
            this._IValuesSettingsApi = new Mock<IValuesSettingsApi>();
            this._sut = new CreatePreContractEconomicConditionCommandHandler(this._IPreContractEconomicConditionRepository.Object, this._IValuesSettingsApi.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettingsApi.Setup(x => x.GetTimeZone()).Returns(timeZone);
            CreatePreContractEconomicConditionCommand command = new CreatePreContractEconomicConditionCommand();

            var result = 0;
            _IPreContractEconomicConditionRepository.Setup(x => x.Register(It.IsAny<PreContractEconomicCondition>())).Returns(Task.FromResult(result));
            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());

            current.Should().Be(result);
            _IPreContractEconomicConditionRepository.VerifyAll();
        }

    }
}
