using GerEventos.Entities;
using GerEventos.Services.Contador;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

public class ContadorAppService : ApplicationService, IContadorAppService
{
    private readonly IRepository<Evento, Guid> _eventoRepository;
    private readonly IRepository<BalcaoVendas, Guid> _balcaoRepository;
    private readonly IRepository<Produtor, Guid> _produtorRepository;

    public ContadorAppService(
        IRepository<Evento, Guid> eventoRepository,
        IRepository<BalcaoVendas, Guid> balcaoRepository,
        IRepository<Produtor, Guid> produtorRepository)
    {
        _eventoRepository = eventoRepository;
        _balcaoRepository = balcaoRepository;
        _produtorRepository = produtorRepository;
    }

    public async Task<int> GetEventCountAsync()
    {
        return await _eventoRepository.CountAsync();
    }

    public async Task<int> GetBalcaoCountAsync()
    {
        return await _balcaoRepository.CountAsync();
    }

    public async Task<int> GetProdutorCountAsync()
    {
        return await _produtorRepository.CountAsync();
    }
}
