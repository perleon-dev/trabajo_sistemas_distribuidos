
using Contracts.Application.Queries.Interfaces;
using Contracts.Application.Queries.Querys;
using Contracts.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Application.Queries.Implementations
{
    public class SellerQueryHandler : ISellerQueryHandler
    {
        public SellerQueryHandler() 
        {
        
        }


        public Task<IEnumerable<SellerViewModel>> FindAll(SellerQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SellerViewModel>> GetBySearch(SellerQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SellerViewModel>> Search(SellerQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
