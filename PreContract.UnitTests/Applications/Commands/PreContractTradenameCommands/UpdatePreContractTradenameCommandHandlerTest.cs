using Contracts.Api.Application.Commands.PreContractTradenameCommands;
using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.PreContractTradenameCommands
{
    public class UpdatePreContractTradenameCommandHandlerTest
    {
        private readonly UpdatePreContractTradenameCommandHandler _sut;
        private readonly Mock<IPreContractTradenameRepository> _IPreContractTradenameRepository;
        private readonly Mock<IValuesSettings> _IValuesSettings;

        public UpdatePreContractTradenameCommandHandlerTest()
        {
            this._IPreContractTradenameRepository = new Mock<IPreContractTradenameRepository>();
            this._IValuesSettings = new Mock<IValuesSettings>();
            this._sut = new UpdatePreContractTradenameCommandHandler(this._IPreContractTradenameRepository.Object, this._IValuesSettings.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            UpdatePreContractTradenameCommand command = new UpdatePreContractTradenameCommand();
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettings.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractTradenameRepository.Setup(x => x.Register(It.IsAny<PreContractTradename>())).Returns(Task.FromResult(result));

            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractTradenameRepository.VerifyAll();

        }

    }
}
