using Volo.Abp.Application.Services;

namespace GerEventos.Services.Estatistica
{
    public interface IEventoEstatisticasAppService : IApplicationService
    {
        Task<List<int>> GetEventosPorMesAsync(int ano);
        Task<Dictionary<string, int>> GetEventosPorTipoAsync();
    }
}
