using PreContracts.Api.Application.Commands.PreContractLogCommands;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractLogAggregate;
using PreContracts.Api.Domain.Util;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Commands.PreContractLogCommands
{
    public class CreatePreContractLogCommandHandlerTest
    {
        private readonly CreatePreContractLogCommandHandler _sut;
        private readonly Mock<IPreContractLogRepository> _IPreContractLogRepository;
        private readonly Mock<IValuesSettingsApi> _IValuesSettingsApi;

        public CreatePreContractLogCommandHandlerTest()
        {
            this._IPreContractLogRepository = new Mock<IPreContractLogRepository>();
            this._IValuesSettingsApi = new Mock<IValuesSettingsApi>();
            this._sut = new CreatePreContractLogCommandHandler(this._IPreContractLogRepository.Object, this._IValuesSettingsApi.Object);
        }

        [Fact]
        public async Task Handle_SellerCenter() 
        {
            var command = new CreatePreContractLogCommand() { 
                state = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                contract_type = ContractTypePreContract.SellerCenter,
                createPreContractLogDetailCommands = new List<Api.Application.Commands.PreContractLogDetailCommand.CreatePreContractLogDetailCommand>() { 
                    new Api.Application.Commands.PreContractLogDetailCommand.CreatePreContractLogDetailCommand()
                },
                file_name = string.Empty,
                number_record = 1
            };
            command.createPreContractLogDetailCommands = new List<Api.Application.Commands.PreContractLogDetailCommand.CreatePreContractLogDetailCommand>();
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettingsApi.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractLogRepository.Setup(x => x.Register(It.IsAny<PreContractLog>())).Returns(Task.FromResult(result));

            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractLogRepository.VerifyAll();
        }

        [Fact]
        public async Task Handle_VTex()
        {
            var command = new CreatePreContractLogCommand()
            {
                state = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                contract_type = ContractTypePreContract.VTex,
                createPreContractLogDetailCommands = new List<Api.Application.Commands.PreContractLogDetailCommand.CreatePreContractLogDetailCommand>() {
                    new Api.Application.Commands.PreContractLogDetailCommand.CreatePreContractLogDetailCommand()
                },
                file_name = string.Empty,
                number_record = 1
            };
            command.createPreContractLogDetailCommands = new List<Api.Application.Commands.PreContractLogDetailCommand.CreatePreContractLogDetailCommand>();
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
