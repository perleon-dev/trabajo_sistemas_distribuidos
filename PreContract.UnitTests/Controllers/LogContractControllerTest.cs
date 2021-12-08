using Contracts.Api.Application.Commands.LogContractCommand;
using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using PreContracts.API.Controllers;
using FluentAssertions;

namespace Contracts.Api.UnitTests.Controllers
{
    public class LogContractControllerTest
    {
        private readonly LogContractController _sut;
        private readonly Mock<ILogContractQuery> _iLogContractQueryMock;
        private readonly Mock<IMediator> _mediatorMock;

        public LogContractControllerTest()
        {
            _iLogContractQueryMock = new Mock<ILogContractQuery>();
            _mediatorMock = new Mock<IMediator>();

            _sut = new LogContractController(_iLogContractQueryMock.Object,
                _mediatorMock.Object);
        }

        [Fact]
        public async Task GetById_VerifyHandlerIsCalled()
        {
            int query = 0;
            var item = new LogContractViewModel();
            var expected = Task.FromResult(item);

            _iLogContractQueryMock.Setup(x => x.GetById(query)).Returns(expected);
            // Act
            var actual = await _sut.GetById(query);

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)actual).Value.Should().BeEquivalentTo(expected.Result);

            _iLogContractQueryMock.Verify();
        }

        [Fact]
        public async Task GetBySearch_VerifyHandlerIsCalled()
        {
            var query = new LogContractRequest();
            var items = new List<LogContractViewModel> { new LogContractViewModel() };
            var expected = Task.FromResult<IEnumerable<LogContractViewModel>>(items);

            _iLogContractQueryMock.Setup(x => x.GetBySearch(query)).Returns(expected);
            // Act
            var actual = await _sut.GetBySearch(query);

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)actual).Value.Should().BeEquivalentTo(expected.Result);

            _iLogContractQueryMock.Verify();
        }

        [Fact]
        public async Task GetByFindAll_VerifyHandlerIsCalled()
        {
            // Arrange
            var query = new LogContractRequest();

            _iLogContractQueryMock
                .Setup(x => x.GetByFindAll(It.IsAny<LogContractRequest>()))
                .Returns(Task.FromResult<(IEnumerable<LogContractViewModel>, int)>((new List<LogContractViewModel>(), 0)));

            var expected = new { Item1 = new List<LogContractViewModel>(), Item2 = 0 };

            // Act
            var actual = await _sut.GetByFindAll(query);

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)actual).Value.Should().BeEquivalentTo(expected);

            _iLogContractQueryMock.Verify();
        }

        [Fact]
        public async Task CreateLogContract_VerifyHandlerIsCalled()
        {
            var command = new CreateLogContractCommand();
            var expected = 1;
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<int>>(), default(System.Threading.CancellationToken)))
                .Returns(Task.FromResult(expected));

            var current = await _sut.CreateLogContract(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.Created);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateLogContract()
        {
            var command = new UpdateLogContractCommand();
            var expected = 1;
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<int>>(), default(System.Threading.CancellationToken)))
                .Returns(Task.FromResult(expected));

            var current = await _sut.UpdateLogContract(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);
        }
    }
}
