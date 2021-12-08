
using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.Querys;
using Contracts.Api.Application.Queries.ViewModels;

using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Contracts.Api.Application.Commands.PreContractCommands;
using Contracts.Api.Domain.Util;
using PreContracts.Api.Controllers;
using Contracts.Api.Application.Queries.Implementations;

namespace Contracts.Api.UnitTests.Controllers
{
    public class PreContractControllerTest
    {
        private readonly PreContractController _sut;
        private readonly Mock<IPreContractQuery> _iPreContractQuery;
        private readonly Mock<IMediator> _mediatorMock;

        public PreContractControllerTest()
        {
            _iPreContractQuery = new Mock<IPreContractQuery>();
            _mediatorMock = new Mock<IMediator>();

            _sut = new PreContractController(_iPreContractQuery.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task GetById_VerifyHandlerIsCalled()
        {
            int query = 0;
            var item = new PreContractViewModel();
            var expected = Task.FromResult(item);

            _iPreContractQuery.Setup(x => x.GetById(query)).Returns(expected);
            // Act
            var actual = await _sut.GetById(query);

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)actual).Value.Should().BeEquivalentTo(expected.Result);

            _iPreContractQuery.Verify();
        }

        [Fact]
        public async Task GetBySearch_VerifyHandlerIsCalled()
        {
            var query = new PreContractRequest();
            var items = new List<PreContractViewModel>();
            var expected = Task.FromResult<IEnumerable<PreContractViewModel>>(items);

            _iPreContractQuery.Setup(x => x.GetBySearch(query)).Returns(expected);
            // Act
            var actual = await _sut.GetBySearch(query);

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)actual).Value.Should().BeEquivalentTo(expected.Result);

            _iPreContractQuery.Verify();
        }

        [Fact]
        public async Task GetByFindAll_VerifyHandlerIsCalled()
        {
            // Arrange
            var query = new PreContractRequest();

            var resultGetByFindAll = new PaginatePrecontract();

            _iPreContractQuery
                .Setup(x => x.GetByFindAll(It.IsAny<PreContractRequest>()))
                .Returns(Task.FromResult<PaginatePrecontract> (resultGetByFindAll));

            var expected = new List<PreContractViewModel> { new PreContractViewModel() };

            var resp = new PaginatePrecontract();
            // Act
            var actual = await _sut.GetByFindAll(query);

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)actual).Value.Should().BeEquivalentTo(resp);

            _iPreContractQuery.Verify();
        }

        [Fact]
        public async Task CreatePreContract_VerifyHandlerIsCalled()
        {
            var command = new CreatePreContractCommand();
            var expected = 1;
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<int>>(), default))
                .Returns(Task.FromResult(expected));

            var current = await _sut.CreatePreContract(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.Created);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdatePreContract_VerifyHandlerIsCalled()
        {
            var command = new UpdatePreContractCommand();
            var expected = 1;
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<int>>(), default))
                .Returns(Task.FromResult(expected));

            var current = await _sut.UpdatePreContract(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task DonwloadTemplateContractMarketPlace_VerifyHandlerIsCalled()
        {
            var query = new PreContractRequest();
            var expected = Task.FromResult("1");

            _iPreContractQuery.Setup(x => x.DownloadTemplateContractMarketPlace()).Returns(expected);
            // Act
            var actual = await _sut.DonwloadTemplateContractMarketPlace();
            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);

            _iPreContractQuery.Verify();
        }

        [Fact]
        public async Task DonwloadTemplateContractMarketPlaceVTex_VerifyHandlerIsCalled()
        {
            var query = new PreContractRequest();
            var expected = Task.FromResult("1");

            _iPreContractQuery.Setup(x => x.DownloadTemplateContractMarketPlaceVTex()).Returns(expected);
            // Act
            var actual = await _sut.DonwloadTemplateContractMarketPlaceVTex();

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);

            _iPreContractQuery.Verify();
        }

        [Fact]
        public async Task DonwloadTemplateContractMarketPlaceVTexToVTex_VerifyHandlerIsCalled()
        {
            var query = new PreContractRequest();
            var expected = Task.FromResult("1");

            _iPreContractQuery.Setup(x => x.DownloadTemplateContractMarketPlaceVTexToVTex()).Returns(expected);
            // Act
            var actual = await _sut.DonwloadTemplateContractMarketPlaceVTexToVTex();

            // Assert
            ((ObjectResult)actual).StatusCode.Should().Be((int)HttpStatusCode.OK);

            _iPreContractQuery.Verify();
        }

        [Fact]
        public async Task CreatePreContractMasisve_VerifyHandlerIsCalled()
        {
            var command = new CreatePreContractMasisveCommand();
            var expected = new MessageResponse();
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<MessageResponse>>(), default))
                .Returns(Task.FromResult(expected));

            var current = await _sut.CreatePreContractMasisve(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);

            _mediatorMock.Verify();
        }

        [Fact]
        public async Task SendPreContract_VerifyHandlerIsCalled()
        {
            var command = new SendPreContractCommand();
            var expected = new MessageResponse();
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<MessageResponse>>(), default))
                .Returns(Task.FromResult(expected));

            var current = await _sut.SendPreContract(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.Created);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);

            _mediatorMock.Verify();
        }

        [Fact]
        public async Task UpdateMassiveStatePreContract_VerifyHandlerIsCalled()
        {
            var command = new UpdateMassiveStatePreContractCommand();
            var expected = new MessageResponse();
            _mediatorMock.Setup(x => x.Send(It.IsAny<IRequest<MessageResponse>>(), default))
                .Returns(Task.FromResult(expected));

            var current = await _sut.UpdateMassiveStatePreContract(command);

            ((ObjectResult)current).StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((ObjectResult)current).Value.Should().BeEquivalentTo(expected);

            _mediatorMock.Verify();
        }
    }
}
