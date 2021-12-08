using Contracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractLogDetailViewModelTest
    {
        [Fact]
        public void PreContractLogDetailViewModelValid()
        {
            var entity = new PreContractLogDetailViewModel
            {
                log_detail_id = 1,
                document_id = string.Empty,
                business_name = string.Empty,
                id_summa = string.Empty,
                item = string.Empty,
                segment = string.Empty,
                commission_variable = 1,
                category_id = 1,
                category_name = string.Empty,
                validity = string.Empty,
                commisison_type = string.Empty,
                month_range_commission_variable = 1,
                percentage_commission_variable = 1,
                fixed_commission = 1,
                month_range_fixed_commission = 1,
                fixed_commisison_amount = 1,
                start_date_contract = string.Empty,
                bank_name = string.Empty,
                bank_account = string.Empty,
                interbank_account = string.Empty,
                currency_name = string.Empty,
                bank_account_type_name = string.Empty,
                observation = string.Empty,
                state = 1,
                log_id = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1)
            };

            var expected = new PreContractLogDetailViewModel
            {
                log_detail_id = 1,
                document_id = string.Empty,
                business_name = string.Empty,
                id_summa = string.Empty,
                item = string.Empty,
                segment = string.Empty,
                commission_variable = 1,
                category_id = 1,
                category_name = string.Empty,
                validity = string.Empty,
                commisison_type = string.Empty,
                month_range_commission_variable = 1,
                percentage_commission_variable = 1,
                fixed_commission = 1,
                month_range_fixed_commission = 1,
                fixed_commisison_amount = 1,
                start_date_contract = string.Empty,
                bank_name = string.Empty,
                bank_account = string.Empty,
                interbank_account = string.Empty,
                currency_name = string.Empty,
                bank_account_type_name = string.Empty,
                observation = string.Empty,
                state = 1,
                log_id = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1)
            };

            var actual = new PreContractLogDetailViewModel
            {
                log_detail_id = entity.log_detail_id,
                document_id = entity.document_id,
                business_name = entity.business_name,
                id_summa = entity.id_summa,
                item = entity.item,
                segment = entity.segment,
                commission_variable = entity.commission_variable,
                category_id = entity.category_id,
                category_name = entity.category_name,
                validity = entity.validity,
                commisison_type = entity.commisison_type,
                month_range_commission_variable = entity.month_range_commission_variable,
                percentage_commission_variable = entity.percentage_commission_variable,
                fixed_commission = entity.fixed_commission,
                month_range_fixed_commission = entity.month_range_fixed_commission,
                fixed_commisison_amount = entity.fixed_commisison_amount,
                start_date_contract = entity.start_date_contract,
                bank_name = entity.bank_name,
                bank_account = entity.bank_account,
                interbank_account = entity.interbank_account,
                currency_name = entity.currency_name,
                bank_account_type_name = entity.bank_account_type_name,
                observation = entity.observation,
                state = entity.state,
                log_id = entity.log_id,
                register_user_id = entity.register_user_id,
                register_user_fullname = entity.register_user_fullname,
                register_datetime = entity.register_datetime
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
