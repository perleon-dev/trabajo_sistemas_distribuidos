using PreContracts.Api.Application.Commands.LogContractCommand;
using PreContracts.Api.Domain.Aggregates.LogContractAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Commands.LogContractCommands
{
    public class UpdateLogContractCommandHandlerTest
    {
        private readonly UpdateLogContractCommandHandler _sut;
        private readonly Mock<ILogContractRepository> _ILogContractRepository;

        public UpdateLogContractCommandHandlerTest()
        {
            _ILogContractRepository = new Mock<ILogContractRepository>();
            _sut = new UpdateLogContractCommandHandler(_ILogContractRepository.Object);
        }

        [Fact]
        public async Task Update_LogContract_ShouldCallCommandHandlerAndMapper()
        {
            UpdateLogContractCommand command = new UpdateLogContractCommand()
            {
                logContractId = 1,
                typeProcessId = 1,
                errorMessage = string.Empty,
                fileStorageId = 1,
                registerDatetime = new System.DateTime(2021, 1, 1),
                registerUserFullname = string.Empty,
                registerUserId = 1,
                state = 1,
                updateDatetime = new System.DateTime(2021, 1, 1),
                updateUserFullname = string.Empty,
                updateUserId = 1
            };

            var expected = 0;
            var current = await _sut.Handle(command, new CancellationToken());

            current.Should().Be(expected);
        }
    }
}
