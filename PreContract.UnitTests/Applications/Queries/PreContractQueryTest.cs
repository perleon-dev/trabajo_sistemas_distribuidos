using PreContracts.Api.Application.Queries.Implementations;
using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.Mappers;
using PreContracts.Api.Application.Queries.Querys;
using PreContracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Queries
{
    public class PreContractQueryTest
    {
        private readonly PreContractQuery _sut;
        private readonly Mock<IPreContractMapper> _PreContractMapperMock;
        private readonly Mock<IQueryHandler> _queryHandlerMock;
        private readonly Mock<ICategoryQueryHandler> _iCategoryQueryHandler;
        
        public PreContractQueryTest()
        {
            _PreContractMapperMock = new Mock<IPreContractMapper>();
            _queryHandlerMock = new Mock<IQueryHandler>();
            _iCategoryQueryHandler = new Mock<ICategoryQueryHandler>();

            _sut = new PreContractQuery(_queryHandlerMock.Object, _PreContractMapperMock.Object, _iCategoryQueryHandler.Object);
        }

        [Fact]
        public async Task GetById_ShouldCallQueryHandlerAndMapper()
        {
            var query = 0;

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new PreContractViewModel();

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractMapperMock.Setup(x => x.MapToPreContractViewModel(objectsList[0])).Returns(expected);

            var result = await _sut.GetById(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetBySearch_ShouldCallQueryHandlerAndMapper()
        {
            var query = new PreContractRequest()
            {
                business_name = string.Empty,
                contract_end_date = string.Empty,
                contract_id = 1,
                contract_start_date= string.Empty,
                ruc = string.Empty,
                tradename  = string.Empty,
                state = 1,
            };

            var parametersXml = Task.FromResult("xml");
            var objectsList = new List<ExpandoObject> { new ExpandoObject() };
            var searchResults = Task.FromResult<IEnumerable<dynamic>>(objectsList);

            var expected = new List<PreContractViewModel>() { new PreContractViewModel() };

            _queryHandlerMock.Setup(x => x.Search<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(searchResults);
            _PreContractMapperMock.Setup(x => x.MapToPreContractViewModel(objectsList[0])).Returns(new PreContractViewModel());

            var result = await _sut.GetBySearch(query);
            result.Should().BeEquivalentTo(expected);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task GetByFindAll_ShouldCallQueryHandlerAndMapper()
        {
            // Arrange
            var query = new PreContractRequest()
            {
                business_name = string.Empty,
                contract_end_date = string.Empty,
                contract_id = 1,
                contract_start_date = string.Empty,
                ruc = string.Empty,
                tradename = string.Empty,
                PageIndex = 1,
                PageSize = 1,
                SortDirection = string.Empty,
                SortProperty = string.Empty,
                state = 1,
                Total = 1
            };

            var objectsLists = new List<ExpandoObject> { new ExpandoObject() };
            var objectsList = new List<PreContractViewModel> { new PreContractViewModel() };
            var obj = new PaginatePrecontract { items = objectsList, quantity=0 };
            var searchResults = Task.FromResult<(IEnumerable<dynamic>, int)>((objectsList, 0));


            _queryHandlerMock.Setup(x => x.FindAll<dynamic>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(searchResults);

            var expected = new List<PreContractViewModel>() { new PreContractViewModel() };

            var resp = new PaginatePrecontract { items = expected, quantity = 0 };

            _PreContractMapperMock.Setup(x => x.MapToPreContractViewModel(objectsList[0])).Returns(expected[0]);

            // Act
            var result = await _sut.GetByFindAll(query);
            result.Should().BeEquivalentTo(resp);
            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task DownloadTemplateContractMarketPlace_ShouldCallQueryHandlerAndMapper()
        {
            var resultSearchCategory = new List<CategoryViewModel>() { new CategoryViewModel() { categoryParentCode = string.Empty } };
            _iCategoryQueryHandler.Setup(x => x.Search(It.IsAny<CategoryQuery>())).Returns(Task.FromResult<IEnumerable<CategoryViewModel>>(resultSearchCategory));

            var expected = new List<PreContractViewModel>() { new PreContractViewModel() };

            var result = await _sut.DownloadTemplateContractMarketPlace();
            result.Should().NotBeNull();

            _queryHandlerMock.VerifyAll();
            _iCategoryQueryHandler.VerifyAll();
        }

        [Fact]
        public async Task DownloadTemplateContractMarketPlaceVTex_ShouldCallQueryHandlerAndMapper()
        {
            var result = await _sut.DownloadTemplateContractMarketPlaceVTex();
            result.Should().NotBeNull();

            _queryHandlerMock.VerifyAll();
        }

        [Fact]
        public async Task DownloadTemplateContractMarketPlaceVTexToVTex_ShouldCallQueryHandlerAndMapper()
        {
            var resultSearchCategory = new List<CategoryViewModel>() { new CategoryViewModel() {  categoryParentCode = string.Empty} };
            _iCategoryQueryHandler.Setup(x => x.Search(It.IsAny<CategoryQuery>())).Returns(Task.FromResult<IEnumerable<CategoryViewModel>>(resultSearchCategory));

            var expected = new List<PreContractViewModel>() { new PreContractViewModel() };

            var result = await _sut.DownloadTemplateContractMarketPlaceVTexToVTex();
            result.Should().NotBeNull();

            _queryHandlerMock.VerifyAll();
            _iCategoryQueryHandler.VerifyAll();
        }
    }
}
