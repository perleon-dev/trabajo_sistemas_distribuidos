using PreContracts.Api.Application.Commands.PreContractTradenameCommands;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Commands.PreContractTradenameCommands
{
    public class CreatePreContractTradenameCommandHandlerTest
    {
        private readonly CreatePreContractTradenameCommandHandler _sut;
        private readonly Mock<IPreContractTradenameRepository> _IPreContractTradenameRepository;
        private readonly Mock<IValuesSettings> _IValuesSettings;

        public CreatePreContractTradenameCommandHandlerTest()
        {
            this._IPreContractTradenameRepository = new Mock<IPreContractTradenameRepository>();
            this._IValuesSettings = new Mock<IValuesSettings>();
            this._sut = new CreatePreContractTradenameCommandHandler(this._IPreContractTradenameRepository.Object, this._IValuesSettings.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            CreatePreContractTradenameCommand command = new CreatePreContractTradenameCommand();
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
