using Volo.Abp.Application.Services;

namespace GerEventos.Services.Contador
{
    public interface IContadorAppService : IApplicationService
    {
        Task<int> GetEventCountAsync();
        Task<int> GetBalcaoCountAsync();
        Task<int> GetProdutorCountAsync();
    }
}
