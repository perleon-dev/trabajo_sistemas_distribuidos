using Contracts.Aplication.Queries.ViewModel;

namespace Contracts.Aplication.Queries.Mappers
{
	public interface IIdSummaMapper
	{
		IdSummaViewModel MapToIdSummaViewModel(dynamic r);
	}

	public class IdSummaMapper : IIdSummaMapper
	{
		public IdSummaViewModel MapToIdSummaViewModel(dynamic r)
		{
			IdSummaViewModel o = new IdSummaViewModel();

			o.id_summa = r.id_summa;
			o.tradename = r.tradename;
			o.register_user_id = r.register_user_id;
			o.register_user_fullname = r.register_user_fullname;
			o.register_datetime = r.register_datetime;
			o.update_user_id = r.update_user_id;
			o.update_user_fullname = r.update_user_fullname;
			o.update_datetime = r.update_datetime;

			return o;
		}
	}
}
