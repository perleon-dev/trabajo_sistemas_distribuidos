
namespace Contracts.Api.Application.Queries.ViewModels
{
    public class Pagination
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortDirection { get; set; }
        public string SortProperty { get; set; }        
        public int Total { get; set; }
    }
}
