using PreContracts.Api.Application.Queries.Interfaces;
using PreContracts.Api.Application.Queries.Mappers;
using PreContracts.Api.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Implementations
{
	public class PreContractBankAccountQuery : IPreContractBankAccountQuery
	{
		private readonly IQueryHandler _iGenericQuery;
		private readonly IPreContractBankAccountMapper _iPreContractBankAccountMapper;

		public PreContractBankAccountQuery(IQueryHandler iGenericQuery, IPreContractBankAccountMapper iPreContractBankAccountMapper)
		{
			_iGenericQuery = iGenericQuery ?? throw new ArgumentNullException(nameof(iGenericQuery));
			_iPreContractBankAccountMapper = iPreContractBankAccountMapper ?? throw new ArgumentNullException(nameof(iPreContractBankAccountMapper));
		}

		public async Task<PreContractBankAccountViewModel> GetById(int contract_bank_account_id)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_bank_account_id", contract_bank_account_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_bank_account_search", parametersXml, string.Empty);

			return result.Select(item => (PreContractBankAccountViewModel)_iPreContractBankAccountMapper.MapToPreContractBankAccountViewModel(item)).FirstOrDefault();
		}

		public async Task<IEnumerable<PreContractBankAccountViewModel>> GetBySearch(PreContractBankAccountRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_bank_account_id", request.contract_bank_account_id ?? 0},
				{"state", request.state ?? 0}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var result = await _iGenericQuery.Search<dynamic>(@"CONTRACT.adv_t_pre_contract_bank_account_search", parametersXml, string.Empty);

			var items = result.Select(item => (PreContractBankAccountViewModel)_iPreContractBankAccountMapper.MapToPreContractBankAccountViewModel(item));

			return items;
		}

		public async Task<(IEnumerable<PreContractBankAccountViewModel>, int)> GetByFindAll(PreContractBankAccountRequest request)
		{
			var parameters = new Dictionary<string, object>
			{
				{"contract_bank_account_id", request.contract_bank_account_id}
			};
			var parametersXml = await _iGenericQuery.BuildParametersXml(parameters);
			var (result, quantity) = await _iGenericQuery.FindAll<dynamic>(@"CONTRACT.adv_t_pre_contract_bank_account_find_all", parametersXml, request.PageIndex, request.PageSize, request.SortProperty);

			var items = result.Select(item => (PreContractBankAccountViewModel)_iPreContractBankAccountMapper.MapToPreContractBankAccountViewModel(item));

			return (items, quantity);
		}
	}
}
