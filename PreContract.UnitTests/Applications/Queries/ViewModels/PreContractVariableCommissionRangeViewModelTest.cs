using PreContracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractVariableCommissionRangeViewModelTest
    {
        [Fact]
        public void PreContractVariableCommissionRangeViewModelValid()
        {
            var entity = new PreContractVariableCommissionRangeViewModel
            {
                contract_tradename_id = 1,
                contract_variable_com_range_id = 1,
                grade = 1,
                percentage = 1,
                validity_active = true,
                validity_end_date = new DateTime(2021,1,1),
                validity_start_date = new DateTime(2021, 1, 1),
                validity_time = 1,
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

            var expected = new PreContractVariableCommissionRangeViewModel
            {
                contract_tradename_id = 1,
                contract_variable_com_range_id = 1,
                grade = 1,
                percentage = 1,
                validity_active = true,
                validity_end_date = new DateTime(2021, 1, 1),
                validity_start_date = new DateTime(2021, 1, 1),
                validity_time = 1,
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

            var actual = new PreContractVariableCommissionRangeViewModel
            {
                contract_tradename_id = entity.contract_tradename_id,
                contract_variable_com_range_id = entity.contract_variable_com_range_id,
                validity_time = entity.validity_time,
                validity_start_date = entity.validity_start_date,
                validity_end_date = entity.validity_end_date,
                validity_active = entity.validity_active,
                percentage = entity.percentage,
                grade = entity.grade,
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
