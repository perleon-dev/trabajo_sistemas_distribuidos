using PreContracts.Api.Application.Queries.ViewModels;
using FluentAssertions;
using System;
using Xunit;

namespace PreContracts.Api.UnitTests.Application.Queries.ViewModels
{
    public class PreContractTradenameViewModelTest
    {
        [Fact]
        public void PreContractTradenameViewModelValid()
        {
            var entity = new PreContractTradenameViewModel
            {
                contract_tradename_id = 1,
                rubric_id = 1,
                tradename = string.Empty,
                tradename_id  = 1,
                state = 1,
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1),
                id_summa = string.Empty
            };

            var expected = new PreContractTradenameViewModel
            {
                contract_tradename_id = 1,
                rubric_id = 1,
                tradename = string.Empty,
                tradename_id = 1,
                state = 1,
                contract_id = 1,
                contract_version = 1,
                contract_modification = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                register_datetime = new DateTime(2021, 1, 1),
                update_user_id = 1,
                update_user_fullname = string.Empty,
                update_datetime = new DateTime(2021, 1, 1),
                id_summa = string.Empty
            };

            var actual = new PreContractTradenameViewModel
            {
                contract_tradename_id = entity.contract_tradename_id,
                rubric_id = entity.rubric_id,
                tradename = entity.tradename,
                tradename_id = entity.tradename_id,
                state = entity.state,
                contract_id = entity.contract_id,
                contract_version = entity.contract_version,
                contract_modification = entity.contract_modification,
                register_user_id = entity.register_user_id,
                register_user_fullname = entity.register_user_fullname,
                register_datetime = entity.register_datetime,
                update_user_id = entity.update_user_id,
                update_user_fullname = entity.update_user_fullname,
                update_datetime = entity.update_datetime,
                id_summa = entity.id_summa
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void PreContractTradenameRequestValid()
        {
            var entity = new PreContractTradenameRequest
            {
                contract_tradename_id = 1,
                ruc = string.Empty,
                state = 1
            };

            var expected = new PreContractTradenameRequest
            {
                contract_tradename_id = 1,
                ruc = string.Empty,
                state = 1
            };

            var actual = new PreContractTradenameRequest
            {
                contract_tradename_id = entity.contract_tradename_id,
                ruc = entity.ruc,
                state = entity.state
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
