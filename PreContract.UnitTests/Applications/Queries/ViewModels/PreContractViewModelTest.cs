using Contracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractViewModelTest
    {
        [Fact]
        public void PreContractViewModelValid()
        {
            var entity = new PreContractViewModel
            {
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                contract_start_date = new DateTime(2021, 1, 1),
                contract_end_date = new DateTime(2021, 1, 1),
                ruc = string.Empty,
                bank_account = string.Empty,
                cci = string.Empty,
                code_contract = string.Empty,
                type_seller = 1,
                distribution_type = string.Empty,
                product_commission = 1,
                state = 1,
                active = true,
                tradename_id = 1,
                mall_id = 1,
                bank_id = string.Empty,
                type_currency = 1,
                bank_account_type = string.Empty,
                segment_id = 1,
                origin = 1,
                ubigeo = string.Empty,
                commercial_template_id = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1),
                business_name = string.Empty,
                tradename = string.Empty,
                commission_variable = string.Empty,
                commission_fixed = string.Empty
            };

            var expected = new PreContractViewModel
            {
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                contract_start_date = new DateTime(2021, 1, 1),
                contract_end_date = new DateTime(2021, 1, 1),
                ruc = string.Empty,
                bank_account = string.Empty,
                cci = string.Empty,
                code_contract = string.Empty,
                type_seller = 1,
                distribution_type = string.Empty,
                product_commission = 1,
                state = 1,
                active = true,
                tradename_id = 1,
                mall_id = 1,
                bank_id = string.Empty,
                type_currency = 1,
                bank_account_type = string.Empty,
                segment_id = 1,
                origin = 1,
                ubigeo = string.Empty,
                commercial_template_id = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1),
                business_name = string.Empty,
                tradename = string.Empty,
                commission_variable = string.Empty,
                commission_fixed = string.Empty
            };

            var actual = new PreContractViewModel
            {
                contract_id = entity.contract_id,
                contract_version = entity.contract_version,
                contract_modification = entity.contract_modification,
                contract_start_date = entity.contract_start_date,
                contract_end_date = entity.contract_end_date,
                ruc = entity.ruc,
                bank_account = entity.bank_account,
                cci = entity.cci,
                code_contract = entity.code_contract,
                type_seller = entity.type_seller,
                distribution_type = entity.distribution_type,
                product_commission = entity.product_commission,
                state = entity.state,
                active = entity.active,
                tradename_id = entity.tradename_id,
                mall_id = entity.mall_id,
                bank_id = entity.bank_id,
                type_currency = entity.type_currency,
                bank_account_type = entity.bank_account_type,
                segment_id = entity.segment_id,
                origin = entity.origin,
                ubigeo = entity.ubigeo,
                commercial_template_id = entity.commercial_template_id,
                register_user_id = entity.register_user_id,
                register_user_fullname = entity.register_user_fullname,
                register_datetime = entity.register_datetime,
                update_user_id = entity.update_user_id,
                update_user_fullname = entity.update_user_fullname,
                update_datetime = entity.update_datetime,
                business_name = entity.business_name,
                tradename = entity.tradename,
                commission_variable = entity.commission_variable,
                commission_fixed = entity.commission_fixed
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
