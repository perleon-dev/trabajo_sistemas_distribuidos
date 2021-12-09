using PreContracts.Api.Application.Queries.Implementations;
using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.Mappers;
using PreContracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Queries
{
    public class PreContractTradenameQueryTest
    {
        private readonly PreContractTradenameQuery _sut;
        private readonly Mock<IPreContractTradenameMapper> _PreContractTradenameMapperMock;
        private readonly Mock<IQueryHandler> _queryHandlerMock;

        public PreContractTradenameQueryTest()
        {
            _PreContractTradenameMapperMock = new Mock<IPreContractTradenameMapper>();
            _queryHandlerMock = new Mock<IQueryHandler>();

            _sut = new PreContractTradenameQuery(_queryHandlerMock.Object, _PreContractTradenameMapperMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldCallQueryHandlerAndMapper()
        {
            var query = 0;

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new PreContractTradenameViewModel();

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractTradenameMapperMock.Setup(x => x.MapToPreContractTradenameViewModel(objectsList[0])).Returns(expected);

            var result = await _sut.GetById(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetBySearch_ShouldCallQueryHandlerAndMapper()
        {
            var query = new PreContractTradenameRequest()
            {
                contract_tradename_id = 1,
                state = 1
            };

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new List<PreContractTradenameViewModel>() { new PreContractTradenameViewModel() };

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractTradenameMapperMock.Setup(x => x.MapToPreContractTradenameViewModel(objectsList[0])).Returns(new PreContractTradenameViewModel());

            var result = await _sut.GetBySearch(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetByFindAll_ShouldCallQueryHandlerAndMapper()
        {
            // Arrange
            var query = new PreContractTradenameRequest()
            {
                contract_tradename_id = 1,
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

            var expected = new List<PreContractTradenameViewModel>() { new PreContractTradenameViewModel() };

            _PreContractTradenameMapperMock.Setup(x => x.MapToPreContractTradenameViewModel(objectsList[0])).Returns(expected[0]);

            // Act
            var result = await _sut.GetByFindAll(query);
            result.Item1.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }
    }
}
