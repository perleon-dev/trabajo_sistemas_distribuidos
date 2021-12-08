using Contracts.Api.Application.Commands.PreContractVariableCommissionRangeCommand;
using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractVariableCommissionRangeAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.PreContractVariableCommissionRangeCommand
{
    public class CreatePreContractVariableCommissionRangeCommandHandlerTest
    {
        private readonly CreatePreContractVariableCommissionRangeCommandHandler _sut;
        private readonly Mock<IPreContractVariableCommissionRangeRepository> _IPreContractVariableCommissionRangeRepository;
        private readonly Mock<IValuesSettings> _IValuesSettings;

        public CreatePreContractVariableCommissionRangeCommandHandlerTest()
        {
            this._IPreContractVariableCommissionRangeRepository = new Mock<IPreContractVariableCommissionRangeRepository>();
            this._IValuesSettings = new Mock<IValuesSettings>();
            this._sut = new CreatePreContractVariableCommissionRangeCommandHandler(this._IPreContractVariableCommissionRangeRepository.Object, this._IValuesSettings.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            CreatePreContractVariableCommissionRangeCommand command = new CreatePreContractVariableCommissionRangeCommand();
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettings.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractVariableCommissionRangeRepository.Setup(x => x.Register(It.IsAny<PreContractVariableCommissionRange>())).Returns(Task.FromResult(result));

            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractVariableCommissionRangeRepository.VerifyAll();


        }
    }
}
