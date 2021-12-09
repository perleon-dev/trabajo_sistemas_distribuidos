using PreContracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractLogViewModelTest
    {
        [Fact]
        public void PreContractLogViewModelValid()
        {
            var entity = new PreContractLogViewModel
            {
                log_id = 1,
                file_name = string.Empty,
                number_record = 1,
                contract_type = 1,
                state = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1)
            };

            var expected = new PreContractLogViewModel
            {
                log_id = 1,
                file_name = string.Empty,
                number_record = 1,
                contract_type = 1,
                state = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1)
            };

            var actual = new PreContractLogViewModel
            {
                log_id = entity.log_id,
                file_name = entity.file_name,
                number_record = entity.number_record,
                contract_type = entity.contract_type,
                state = entity.state,
                register_user_id = entity.register_user_id,
                register_user_fullname = entity.register_user_fullname,
                register_datetime = entity.register_datetime
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
