using PreContracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractFixedCommissionRangeViewModelTest
    {
        [Fact]
        public void PreContractFixedCommissionRangeViewModelValid()
        {
            var entity = new PreContractFixedCommissionRangeViewModel
            {
                contract_fixed_com_range_id = 1,
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                validity_time = 1,
                amount = 1,
                validity_start_date = new DateTime(2021, 1, 1),
                validity_end_date = new DateTime(2021, 1, 1),
                validity_active = true,
                state = 1,
                grade = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1)
            };

            var expected = new PreContractFixedCommissionRangeViewModel
            {
                contract_fixed_com_range_id = 1,
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                validity_time = 1,
                amount = 1,
                validity_start_date = new DateTime(2021, 1, 1),
                validity_end_date = new DateTime(2021, 1, 1),
                validity_active = true,
                state = 1,
                grade = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1)
            };

            var actual = new PreContractFixedCommissionRangeViewModel
            {
                contract_fixed_com_range_id = entity.contract_fixed_com_range_id,
                contract_id = entity.contract_id,
                contract_version = entity.contract_version,
                contract_modification = entity.contract_modification,
                validity_time = entity.validity_time,
                amount = entity.amount,
                validity_start_date = entity.validity_start_date,
                validity_end_date = entity.validity_end_date,
                validity_active = entity.validity_active,
                state = entity.state,
                grade = entity.grade,
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
