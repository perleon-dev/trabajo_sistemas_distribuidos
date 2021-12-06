using Contracts.Aplication.Queries.ViewModel;

namespace Contracts.Aplication.Queries.Mappers
{
	public interface ICustomerSummaMapper
	{
		CustomerSummaViewModel MapToCustomerSummaViewModel(dynamic r);
	}

	public class CustomerSummaMapper : ICustomerSummaMapper
	{
		public CustomerSummaViewModel MapToCustomerSummaViewModel(dynamic r)
		{
			CustomerSummaViewModel o = new CustomerSummaViewModel();

			o.customer_summa_id = r.customer_summa_id;
			o.document_id = r.document_id;
			o.id_summa = r.id_summa;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;
			o.update_user_id = r.update_user_id;
			o.update_user_fullname = r.update_user_fullname;
			o.update_datetime = r.update_datetime;
			o.tradename = r.tradename;

            return o;
		}
	}
}
