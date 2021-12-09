using PreContracts.Api.Application.Queries.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreContracts.Api.Application.Queries.Interfaces
{
    public interface IQueryHandler
    {
        Task<string> BuildParametersXml(Dictionary<string, object> parameters);
        Task<IEnumerable<T>> Search<T>(string procedure, string parametersXml, string sort);
        Task<(IEnumerable<dynamic>,int)> FindAll<T>(string procedure, string parametersXml, int paginaActual, int cantidadMostrar, string sort);
        Task<IEnumerable<T>> Job<T>(string procedure);
        Task<IEnumerable<T>> Job<T>(string procedure, string parametersXml);
        Task<IEnumerable<T>> GetAllAsync<T>(string procedure, string parametersXml, Pagination pagination);
    }
}
