
using Contracts.Application.Queries.Querys;
using Contracts.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Application.Queries.Interfaces
{
    public interface ISellerQueryHandler
    {
        Task<IEnumerable<SellerViewModel>> GetBySearch(SellerQuery query);
        Task<IEnumerable<SellerViewModel>> Search(SellerQuery query);
        Task<IEnumerable<SellerViewModel>> FindAll(SellerQuery query);
    }
}
