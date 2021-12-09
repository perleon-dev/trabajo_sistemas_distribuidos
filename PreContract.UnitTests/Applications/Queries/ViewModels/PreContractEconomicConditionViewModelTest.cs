using PreContracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractEconomicConditionViewModelTest
    {
        [Fact]
        public void PreContractEconomicConditionViewModelValid()
        {
            var entity = new PreContractEconomicConditionViewModel
            {
                economic_condition_id = 1,
                commission = 1,
                category_id = 1,
                state = 1,
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1)
            };

            var expected = new PreContractEconomicConditionViewModel
            {
                economic_condition_id = 1,
                commission = 1,
                category_id = 1,
                state = 1,
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1)
            };

            var actual = new PreContractEconomicConditionViewModel
            {
                economic_condition_id = entity.economic_condition_id,
                commission = entity.commission,
                category_id = entity.category_id,
                state = entity.state,
                contract_id = entity.contract_id,
                contract_version = entity.contract_version,
                contract_modification = entity.contract_modification,
                register_user_id = entity.register_user_id,
                register_user_fullname = entity.register_user_fullname,
                register_datetime = entity.register_datetime,
                update_user_id = entity.update_user_id,
                update_user_fullname = entity.update_user_fullname,
                update_datetime = entity.update_datetime
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
