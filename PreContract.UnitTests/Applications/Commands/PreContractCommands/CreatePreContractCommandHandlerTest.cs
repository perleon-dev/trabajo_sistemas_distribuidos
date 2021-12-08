using Contracts.Api.Application.Commands.PreContractCommands;
using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.PreContractCommands
{
    public class CreatePreContractCommandHandlerTest
    {
        private readonly CreatePreContractCommandHandler _sut;
        private readonly Mock<IPreContractRepository> _IPreContractRepository;
        private readonly Mock<IValuesSettings> _IValuesSettings;

        public CreatePreContractCommandHandlerTest()
        {
            this._IPreContractRepository = new Mock<IPreContractRepository>();
            this._IValuesSettings = new Mock<IValuesSettings>();
            this._sut = new CreatePreContractCommandHandler(this._IPreContractRepository.Object, this._IValuesSettings.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            CreatePreContractCommand command = new CreatePreContractCommand();
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettings.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractRepository.Setup(x => x.Register(It.IsAny<Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract>())).Returns(Task.FromResult(result));
            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractRepository.VerifyAll();
        }
    }
}
