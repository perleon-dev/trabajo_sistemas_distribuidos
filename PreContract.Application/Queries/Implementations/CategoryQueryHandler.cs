using Contracts.Api.Application.Queries.Interfaces;
using Contracts.Api.Application.Queries.Mappers;
using Contracts.Api.Application.Queries.Querys;
using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Queries.Implementations
{
    [ExcludeFromCodeCoverage]
    public class CategoryQueryHandler : ICategoryQueryHandler
    {
        private readonly IQueryHandler _queryHandler;
        private readonly ICategoryMapper _iCategoryMapper;

        public CategoryQueryHandler(IQueryHandler IQueryHandler, ICategoryMapper ICategoryMapper)
        {
            this._queryHandler = IQueryHandler;
            this._iCategoryMapper = ICategoryMapper;
        }

        public async Task<IEnumerable<CategoryViewModel>> Search(CategoryQuery categoryQuery)
        {
            var parameters = new Dictionary<string, object>
            {
                {"category_id", categoryQuery.categoryId ?? 0},
                {"type_seller", categoryQuery.typeSeller ?? 0},
                {"category_code", categoryQuery.categoryCode ?? string.Empty}
            };

            var parametersXml = await _queryHandler.BuildParametersXml(parameters);

            var searchResults =
                await _queryHandler.Search<dynamic>("CONTRACT.adv_t_category_search", parametersXml, categoryQuery.sortProperty);
            return searchResults.Select(item => (CategoryViewModel)_iCategoryMapper.MapToCategoryViewModel(item));
        }

        public async Task<IEnumerable<CategoryViewModel>> SearchCategoryLevel(CategoryQuery categoryQuery)
        {
            IEnumerable<CategoryViewModel> categoryThree = new List<CategoryViewModel>();

            var categoryResultSearch = await this.Search(categoryQuery);
            if (categoryQuery.level == 1)
            {
                var categoryResultList = categoryResultSearch.ToList();
                categoryThree = categoryResultList.Where(x => string.IsNullOrEmpty(x.categoryParentCode));
            }
            else if (categoryQuery.level == 2)
                categoryThree = this.GetCategoryLevelTwo(categoryResultSearch);
            else if (categoryQuery.level == 3)
                categoryThree = this.GetCategoryLevelThree(categoryResultSearch);
            else
                categoryThree = GetCategorylevelAll(categoryResultSearch, new List<CategoryViewModel>(), string.Empty, string.Empty, 0);

            return categoryThree;
        }

        #region Methods
        private IEnumerable<CategoryViewModel> GetCategoryLevelTwo(IEnumerable<CategoryViewModel> categoryResultSearch) 
        {
            var categoryLevelTwo = new List<CategoryViewModel>();

            var categoryDad = categoryResultSearch.Where(x => string.IsNullOrEmpty(x.categoryParentCode));
            var categoryOnlyChildren = categoryResultSearch.Where(x => !string.IsNullOrEmpty(x.categoryParentCode));

            foreach (var itemDad in categoryDad)
            {
                var categoryChildrenFound = categoryOnlyChildren.Where(x => x.categoryParentCode == itemDad.categoryCode);
                foreach (var itemChildren in categoryChildrenFound)
                {
                    categoryLevelTwo.Add(new CategoryViewModel()
                    {
                        typeSeller = itemChildren.typeSeller,
                        categoryId = itemChildren.categoryId,
                        categoryCode = itemChildren.categoryCode,
                        categoryParentCode = itemDad.categoryCode,
                        categoryName = itemDad.categoryName + "/" + itemChildren.categoryName
                    });
                }

            }

            return categoryLevelTwo;
        }

        private IEnumerable<CategoryViewModel> GetCategoryLevelThree(IEnumerable<CategoryViewModel> categoryResultSearch)
        {
            var categoryLevelThree = new List<CategoryViewModel>();

            var categoryDad = categoryResultSearch.Where(x => string.IsNullOrEmpty(x.categoryParentCode));
            var categoryOnlyChildren = categoryResultSearch.Where(x => !string.IsNullOrEmpty(x.categoryParentCode));
            foreach (var itemDad in categoryDad)
            {
                var categoryChildrenFound = categoryOnlyChildren.Where(x => x.categoryParentCode == itemDad.categoryCode);
                if (categoryChildrenFound.Any())
                {
                    foreach (var itemChildren in categoryChildrenFound)
                    {
                        var childrenOfChildrenFound = categoryOnlyChildren.Where(x => x.categoryParentCode == itemChildren.categoryCode);
                        foreach (var itemChildrenOfChildren in childrenOfChildrenFound)
                        {
                            categoryLevelThree.Add(new CategoryViewModel()
                            {
                                typeSeller = itemChildrenOfChildren.typeSeller,
                                categoryId = itemChildrenOfChildren.categoryId,
                                categoryCode = itemChildrenOfChildren.categoryCode,
                                categoryParentCode = itemDad.categoryCode,
                                categoryName = itemDad.categoryName + "/" + itemChildren.categoryName + "/" + itemChildrenOfChildren.categoryName
                            });
                        }
                    }
                }                
            }

            return categoryLevelThree;
        }

        private IEnumerable<CategoryViewModel> GetCategorylevelAll(IEnumerable<CategoryViewModel> categoryResultSearch, List<CategoryViewModel> categoryTree, string parentParentCode, string prefix, int level) 
        {
            CategoryViewModel categoryViewModel = null;
            var categoryFoundParentCode = categoryResultSearch.Where(x => x.categoryParentCode == parentParentCode);
            foreach (var category in categoryFoundParentCode)
            {
                var categoryThreeFound = categoryTree.Where(x => x.categoryCode == category.categoryCode);
                if (!categoryThreeFound.Any())
                {
                    if (level == 0) prefix = string.Empty;

                    var categoryDadFound = categoryTree.Where(x => x.categoryCode == category.categoryParentCode);
                    if (categoryDadFound.Any()) prefix = categoryDadFound.FirstOrDefault().categoryName + "/";

                    categoryViewModel = new CategoryViewModel() 
                    {
                        categoryCode = category.categoryCode,
                        categoryId = category.categoryId,
                        categoryName = prefix + category.categoryName,
                        categoryParentCode = category.categoryParentCode,
                        typeSeller = category.typeSeller
                    };

                    prefix += category.categoryName + "/";
                    var nextChildren = categoryResultSearch.Where(x => x.categoryParentCode == category.categoryParentCode);
                    if (!nextChildren.Any())
                    {
                        var indexLastFound = prefix.LastIndexOf("/");
                        var prefixLast = prefix.Substring(0, indexLastFound);

                        indexLastFound = prefixLast.LastIndexOf("/") + 1;
                        prefixLast = prefixLast.Substring(0, indexLastFound);
                        prefix = prefixLast;
                    }

                    categoryTree.Add(categoryViewModel);
                }
                categoryTree = GetCategorylevelAll(categoryResultSearch, categoryTree, category.categoryCode, prefix, 1).ToList();
            }

            return categoryTree;
        }

        #endregion

    }
}
