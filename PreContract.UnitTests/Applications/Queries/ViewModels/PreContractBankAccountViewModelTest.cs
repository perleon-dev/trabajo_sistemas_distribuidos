using Contracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractBankAccountViewModelTest
    {
        [Fact]
        public void PreContractBankAccountViewModelValid()
        {
            var entity = new PreContractBankAccountViewModel
            {
                account_number = string.Empty,
                state= 1,
                bank_id = 1,
                cci_account_number = string.Empty,
                contract_bank_account_id = 1,
                contract_id = 1,
                contract_modification = 1,
                contract_version = 1,
                currency_id = 1,
                register_datetime = new DateTime(2021,1,1),
                register_user_fullname = string.Empty,
                register_user_id = 1,
                type_account=1,
                update_datetime = new DateTime(2021,1,1),
                update_user_fullname = string.Empty,
                update_user_id = 1
            };

            var expected = new PreContractBankAccountViewModel
            {
                account_number = string.Empty,
                state = 1,
                bank_id = 1,
                cci_account_number = string.Empty,
                contract_bank_account_id = 1,
                contract_id = 1,
                contract_modification = 1,
                contract_version = 1,
                currency_id = 1,
                register_datetime = new DateTime(2021, 1, 1),
                register_user_fullname = string.Empty,
                register_user_id = 1,
                type_account = 1,
                update_datetime = new DateTime(2021, 1, 1),
                update_user_fullname = string.Empty,
                update_user_id = 1
            };

            var actual = new PreContractBankAccountViewModel
            {
                account_number = entity.account_number,
                state = entity.state,
                bank_id = entity.bank_id,
                cci_account_number = entity.cci_account_number,
                contract_bank_account_id = entity.contract_bank_account_id,
                contract_id = entity.contract_id,
                contract_modification = entity.contract_modification,
                contract_version = entity.contract_version,
                currency_id = entity.currency_id,
                register_datetime = entity.register_datetime,
                register_user_fullname = entity.register_user_fullname,
                register_user_id = entity.register_user_id,
                type_account = entity.type_account,
                update_datetime = entity.update_datetime,
                update_user_fullname = entity.update_user_fullname,
                update_user_id = entity.update_user_id,
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
