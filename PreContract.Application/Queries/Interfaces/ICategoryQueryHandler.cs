using Contracts.Api.Application.Queries.Querys;
using Contracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Api.Application.Queries.Interfaces
{
    public interface ICategoryQueryHandler
    {
        Task<IEnumerable<CategoryViewModel>> Search(CategoryQuery categoryQuery);
        Task<IEnumerable<CategoryViewModel>> SearchCategoryLevel(CategoryQuery categoryQuery);
    }
}
