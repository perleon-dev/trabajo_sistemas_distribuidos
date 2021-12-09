using PreContracts.Api.Application.Commands.PreContractLogDetailCommand;
using PreContracts.Api.Application.Queries.Generic;
using PreContracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Commands.PreContractLogDetailCommand
{
    public class ChangeStatePreContractLogDetailCommandHandlerTest
    {
        private readonly ChangeStatePreContractLogDetailCommandHandler _sut;
        private readonly Mock<IPreContractLogDetailRepository> _IPreContractLogDetailRepository;
        private readonly Mock<IValuesSettingsApi> _IValuesSettingsApi;

        public ChangeStatePreContractLogDetailCommandHandlerTest()
        {
            this._IPreContractLogDetailRepository = new Mock<IPreContractLogDetailRepository>();
            this._IValuesSettingsApi = new Mock<IValuesSettingsApi>();
            this._sut = new ChangeStatePreContractLogDetailCommandHandler(this._IPreContractLogDetailRepository.Object, this._IValuesSettingsApi.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            var command = new ChangeStatePreContractLogDetailCommand() { 
                log_detail_id = 1,
                observation = string.Empty,
                register_user_fullname = string.Empty,
                register_user_id = 1,
                state= 1
            };
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettingsApi.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractLogDetailRepository.Setup(x => x.UpdateState(It.IsAny<PreContractLogDetail>())).Returns(Task.FromResult(result));

            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractLogDetailRepository.VerifyAll();
        }
    }
}
