using Contracts.Api.Application.Queries.Implementations;
using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.Mappers;
using Contracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Queries
{
    public class PreContractVariableCommissionRangeQueryTest
    {
        private readonly PreContractVariableCommissionRangeQuery _sut;
        private readonly Mock<IPreContractVariableCommissionRangeMapper> _PreContractVariableCommissionRangeMapperMock;
        private readonly Mock<IQueryHandler> _queryHandlerMock;

        public PreContractVariableCommissionRangeQueryTest()
        {
            _PreContractVariableCommissionRangeMapperMock = new Mock<IPreContractVariableCommissionRangeMapper>();
            _queryHandlerMock = new Mock<IQueryHandler>();

            _sut = new PreContractVariableCommissionRangeQuery(_queryHandlerMock.Object, _PreContractVariableCommissionRangeMapperMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldCallQueryHandlerAndMapper()
        {
            var query = 0;

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new PreContractVariableCommissionRangeViewModel();

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractVariableCommissionRangeMapperMock.Setup(x => x.MapToPreContractVariableCommissionRangeViewModel(objectsList[0])).Returns(expected);

            var result = await _sut.GetById(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetBySearch_ShouldCallQueryHandlerAndMapper()
        {
            var query = new PreContractVariableCommissionRangeRequest()
            {
                contract_variable_com_range_id = 1,
                state = 1
            };

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new List<PreContractVariableCommissionRangeViewModel>() { new PreContractVariableCommissionRangeViewModel() };

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractVariableCommissionRangeMapperMock.Setup(x => x.MapToPreContractVariableCommissionRangeViewModel(objectsList[0])).Returns(new PreContractVariableCommissionRangeViewModel());

            var result = await _sut.GetBySearch(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetByFindAll_ShouldCallQueryHandlerAndMapper()
        {
            // Arrange
            var query = new PreContractVariableCommissionRangeRequest()
            {
                contract_variable_com_range_id = 1,
                PageIndex = 1,
                PageSize = 1,
                SortDirection = string.Empty,
                SortProperty = string.Empty,
                state = 1,
                Total = 1
            };
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<(IEnumerable<dynamic>, int)>((objectsList, 0));

            _queryHandlerMock.Setup(x => x.FindAll<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(searchResults);

            var expected = new List<PreContractVariableCommissionRangeViewModel>() { new PreContractVariableCommissionRangeViewModel() };

            _PreContractVariableCommissionRangeMapperMock.Setup(x => x.MapToPreContractVariableCommissionRangeViewModel(objectsList[0])).Returns(expected[0]);

            // Act
            var result = await _sut.GetByFindAll(query);
            result.Item1.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }
    }
}
