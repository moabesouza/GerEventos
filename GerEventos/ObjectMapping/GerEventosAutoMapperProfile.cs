using AutoMapper;
using GerEventos.Entities;
using GerEventos.Services.Dtos.BalcaoVendas;
using GerEventos.Services.Dtos.Eventos;
using GerEventos.Services.Dtos.TipoEvento;
using GerEventos.Services.Dtos.TipoEventos;

public class GerEventosAutoMapperProfile : Profile
{
    public GerEventosAutoMapperProfile()
    {

        // Mapeamento para Evento
        CreateMap<Evento, EventoDto>();
        CreateMap<CreateUpdateEventoDto, Evento>();
        CreateMap<EventoDto, CreateUpdateEventoDto>();

        // Mapeamento para TipoEvento
        CreateMap<TipoEvento, TipoEventoDto>();
        CreateMap<CreateUpdateTipoEventoDto, TipoEvento>();
        CreateMap<TipoEventoDto, CreateUpdateTipoEventoDto>();

        // Mapeamento para BalcaoVendas
        CreateMap<BalcaoVendas, BalcaoVendasDto>();
        CreateMap<CreateUpdateBalcaoVendasDto, BalcaoVendas>();
        CreateMap<BalcaoVendasDto, CreateUpdateBalcaoVendasDto>();
    }
}
