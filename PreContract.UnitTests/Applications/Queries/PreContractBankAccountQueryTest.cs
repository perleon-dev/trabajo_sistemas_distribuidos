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
    public class PreContractBankAccountQueryTest
    {
        private readonly PreContractBankAccountQuery _sut;
        private readonly Mock<IPreContractBankAccountMapper> _PreContractBankAccountMapperMock;
        private readonly Mock<IQueryHandler> _queryHandlerMock;

        public PreContractBankAccountQueryTest()
        {
            _PreContractBankAccountMapperMock = new Mock<IPreContractBankAccountMapper>();
            _queryHandlerMock = new Mock<IQueryHandler>();

            _sut = new PreContractBankAccountQuery(_queryHandlerMock.Object, _PreContractBankAccountMapperMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldCallQueryHandlerAndMapper()
        {
            var query = 0;

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new PreContractBankAccountViewModel();

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractBankAccountMapperMock.Setup(x => x.MapToPreContractBankAccountViewModel(objectsList[0])).Returns(expected);

            var result = await _sut.GetById(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetBySearch_ShouldCallQueryHandlerAndMapper()
        {
            var query = new PreContractBankAccountRequest()
            {
                contract_bank_account_id = 1,
                state = 1
            };

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new List<PreContractBankAccountViewModel>() { new PreContractBankAccountViewModel() };

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractBankAccountMapperMock.Setup(x => x.MapToPreContractBankAccountViewModel(objectsList[0])).Returns(new PreContractBankAccountViewModel());

            var result = await _sut.GetBySearch(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetByFindAll_ShouldCallQueryHandlerAndMapper()
        {
            // Arrange
            var query = new PreContractBankAccountRequest()
            {
                contract_bank_account_id = 1,
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

            var expected = new List<PreContractBankAccountViewModel>() { new PreContractBankAccountViewModel() };

            _PreContractBankAccountMapperMock.Setup(x => x.MapToPreContractBankAccountViewModel(objectsList[0])).Returns(expected[0]);

            // Act
            var result = await _sut.GetByFindAll(query);
            result.Item1.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }
    }
}
