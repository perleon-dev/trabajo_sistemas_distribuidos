using Contracts.Api.Application.Commands.LogContractCommand;
using Contracts.Api.Domain.Aggregates.LogContractAggregate;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.LogContractCommands
{
    public class CreateLogContractCommandHandlerTest
    {
        private readonly CreateLogContractCommandHandler _sut;
        private readonly Mock<ILogContractRepository> _ILogContractRepository;

        public CreateLogContractCommandHandlerTest()
        {
            _ILogContractRepository = new Mock<ILogContractRepository>();
            _sut = new CreateLogContractCommandHandler(_ILogContractRepository.Object);
        }

        [Fact]
        public async Task Create_LogContract_ShouldCallCommandHandlerAndMapper()
        {
            CreateLogContractCommand command = new CreateLogContractCommand()
            {
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
