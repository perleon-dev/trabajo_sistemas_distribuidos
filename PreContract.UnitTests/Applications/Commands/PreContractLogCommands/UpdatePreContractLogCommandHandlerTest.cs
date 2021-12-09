using PreContracts.Api.Application.Commands.PreContractLogCommands;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractLogAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Commands.PreContractLogCommands
{
    public class UpdatePreContractLogCommandHandlerTest
    {
        private readonly UpdatePreContractLogCommandHandler _sut;
        private readonly Mock<IPreContractLogRepository> _IPreContractLogRepository;
        private readonly Mock<IValuesSettingsApi> _IValuesSettingsApi;

        public UpdatePreContractLogCommandHandlerTest()
        {
            this._IPreContractLogRepository = new Mock<IPreContractLogRepository>();
            this._IValuesSettingsApi = new Mock<IValuesSettingsApi>();
            this._sut = new UpdatePreContractLogCommandHandler(this._IPreContractLogRepository.Object, this._IValuesSettingsApi.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            var command = new UpdatePreContractLogCommand()
            {
                log_id = 1,
                state = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                contract_type = 1,
                file_name = string.Empty,
                number_record = 1
            };
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettingsApi.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractLogRepository.Setup(x => x.Register(It.IsAny<PreContractLog>())).Returns(Task.FromResult(result));
            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractLogRepository.VerifyAll();

        }
    }
}
