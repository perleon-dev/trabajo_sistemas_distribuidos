using Contracts.Api.Application.Queries.ViewModels;

namespace Contracts.Api.Application.Queries.Mappers
{
    public interface ICategoryMapper 
    {
        CategoryViewModel MapToCategoryViewModel(dynamic r);
    }
    public class CategoryMapper : ICategoryMapper
    {
        public CategoryViewModel MapToCategoryViewModel(dynamic r)
        {
            CategoryViewModel o = new CategoryViewModel();

            o.categoryId = r.category_id;
            o.categoryName = r.category_name;
            o.categoryCode = r.category_code;
            o.typeSeller = r.type_seller;
            o.categoryParentCode = r.category_parent_code;
            o.state = r.state;

            return o;
        }
    }
}
