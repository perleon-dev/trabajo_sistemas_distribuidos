using Contracts.Api.Application.Commands.PreContractLogDetailCommand;
using Contracts.Api.Application.Queries.Generic;
using Contracts.Api.Domain.Aggregates.PreContractLogDetailAggregate;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Contracts.Api.UnitTests.Application.Commands.PreContractLogDetailCommand
{
    public class CreatePreContractLogDetailCommandHandlerTest
    {
        private readonly CreatePreContractLogDetailCommandHandler _sut;
        private readonly Mock<IPreContractLogDetailRepository> _IPreContractLogDetailRepository;
        private readonly Mock<IValuesSettingsApi> _IValuesSettingsApi;

        public CreatePreContractLogDetailCommandHandlerTest()
        {
            this._IPreContractLogDetailRepository = new Mock<IPreContractLogDetailRepository>();
            this._IValuesSettingsApi = new Mock<IValuesSettingsApi>();
            this._sut = new CreatePreContractLogDetailCommandHandler(this._IPreContractLogDetailRepository.Object, this._IValuesSettingsApi.Object);
        }

        [Fact]
        public async Task Handle() 
        {
            var command = new CreatePreContractLogDetailCommand() { 
                state = 1,
                register_user_id = 1,
                register_user_fullname = string.Empty,
                observation = string.Empty,
                bank_account = string.Empty,
                bank_account_type_name = string.Empty,
                bank_name = string.Empty,
                business_name = string.Empty,
                category_id = 1,
                category_name = string.Empty,
                commisison_type = string.Empty,
                commission_variable = 1,
                currency_name = string.Empty,
                document_id = string.Empty,
                fixed_commisison_amount = 1,
                fixed_commission = 1,
                id_summa = string.Empty,
                interbank_account = string.Empty,
                item = string.Empty,
                log_id = 1,
                month_range_commission_variable = 1,
                month_range_fixed_commission = 1,
                percentage_commission_variable = 1,
                segment = string.Empty,
                start_date_contract = string.Empty,
                validity = string.Empty
            };
            string timeZone = TimeZoneInfo.Local.Id;
            _IValuesSettingsApi.Setup(x => x.GetTimeZone()).Returns(timeZone);

            var result = 0;
            _IPreContractLogDetailRepository.Setup(x => x.Register(It.IsAny<PreContractLogDetail>())).Returns(Task.FromResult(result));

            var current = await this._sut.Handle(command, new System.Threading.CancellationToken());
            current.Should().Be(result);

            _IPreContractLogDetailRepository.VerifyAll();

        }
    }
}
