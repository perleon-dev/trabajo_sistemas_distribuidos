using Contracts.Application.Queries.ViewModels;

namespace Contracts.Application.Queries.Mappers
{
	public interface ITradenameMapper
	{
		TradenameViewModel MapToTradenameViewModel(dynamic r);
	}

	public class TradenameMapper : ITradenameMapper
	{
		public TradenameViewModel MapToTradenameViewModel(dynamic r)
		{
			TradenameViewModel o = new TradenameViewModel();

			o.TradenameId =  r.nomb_com_c_iid;
			o.TradenameName =  r.nomb_com_c_vnomb;

			return o;
		}
	}
}