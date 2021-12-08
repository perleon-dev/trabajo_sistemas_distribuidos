using Contracts.Api.Application.Commands.PreContractCommands;
using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
using Contracts.Api.Domain.Aggregates.PreContractAggregate;
using Contracts.Api.Domain.Aggregates.PreContractTradenameAggregate;
using Contracts.Api.Domain.Util;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.PreContractCommands
{
    public class UpdateMassiveStatePreContractCommandHandlerTest
    {
        private readonly UpdateMassiveStatePreContractCommandHandler _sut;
        private readonly Mock<IPreContractRepository> _IPreContractRepositoryMock;
        private readonly Mock<IPreContractTradenameQuery> _iPreContractTradenameQueryMock;

        public UpdateMassiveStatePreContractCommandHandlerTest()
        {
            this._IPreContractRepositoryMock = new Mock<IPreContractRepository>();
            this._iPreContractTradenameQueryMock = new Mock<IPreContractTradenameQuery>();
            this._sut = new UpdateMassiveStatePreContractCommandHandler(this._IPreContractRepositoryMock.Object, this._iPreContractTradenameQueryMock.Object);
        }

        [Fact]
        public async Task UpdateMassive_StatePreContract_ShouldCallCommandHandlerAndMapper()
        {
            UpdateMassiveStatePreContractCommand command = new UpdateMassiveStatePreContractCommand() { 
                preContractList = new List<UpdateMassiveStatePreContract> { new UpdateMassiveStatePreContract() },
                state = 1,
                updateUserFullname = string.Empty,
                updateUserId = 1
            };

            var tradenamesResult = new List<PreContractTradenameViewModel>() { new PreContractTradenameViewModel() { tradename_id = 1 } };
            _iPreContractTradenameQueryMock.Setup(x => x.GetBySearch(It.IsAny<PreContractTradenameRequest>())).Returns(Task.FromResult<IEnumerable<PreContractTradenameViewModel>>( tradenamesResult));

            var resultUpdateStateJson = 1;
            _IPreContractRepositoryMock.Setup(x => x.UpdateStateJson(It.IsAny<List<Contracts.Api.Domain.Aggregates.PreContractAggregate.PreContract>>(), It.IsAny<List<PreContractTradename>>())).Returns(Task.FromResult(resultUpdateStateJson));

            var expected = new MessageResponse()
            {
                codeStatus = CodeStatus.Create,
                message = "Actualización satisfactoria"
            };

            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().BeEquivalentTo(expected);

            _IPreContractRepositoryMock.VerifyAll();
        }
    }
}
