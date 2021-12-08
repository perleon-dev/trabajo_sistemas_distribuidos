using Contracts.Api.Application.Commands.PreContractFixedCommissionRangeCommands;
using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractFixedCommissionRangeAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.PreContractFixedCommissionRangeCommands
{
    public class CreatePreContractFixedCommissionRangeCommandHandlerTest
    {
        private readonly CreatePreContractFixedCommissionRangeCommandHandler _sut;
        private readonly Mock<IPreContractFixedCommissionRangeRepository> _IPreContractFixedCommissionRangeRepository;
        private readonly Mock<IValuesSettings> _IValuesSettings;

        public CreatePreContractFixedCommissionRangeCommandHandlerTest()
        {
            this._IPreContractFixedCommissionRangeRepository = new Mock<IPreContractFixedCommissionRangeRepository>();
            this._IValuesSettings = new Mock<IValuesSettings>();
            this._sut = new CreatePreContractFixedCommissionRangeCommandHandler(this._IPreContractFixedCommissionRangeRepository.Object, this._IValuesSettings.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettings.Setup(x => x.GetTimeZone()).Returns(timeZone);
            CreatePreContractFixedCommissionRangeCommand command = new CreatePreContractFixedCommissionRangeCommand();
            var result = 0;

            _IPreContractFixedCommissionRangeRepository.Setup(x => x.Register(It.IsAny<PreContractFixedCommissionRange>())).Returns(Task.FromResult(result));
            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractFixedCommissionRangeRepository.VerifyAll();
        }

    }
}
