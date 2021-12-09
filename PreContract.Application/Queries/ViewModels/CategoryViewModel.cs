namespace PreContracts.Api.Application.Queries.ViewModels
{
    public class CategoryViewModel
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string categoryCode { get; set; }
        public int typeSeller { get; set; }
        public string categoryParentCode { get; set; }
        public int? state { get; set; }
    }
}
