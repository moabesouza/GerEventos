using AutoMapper;
using GerEventos.Entities;
using GerEventos.Services.Dtos.BalcaoVendas;
using GerEventos.Services.Dtos.Eventos;
using GerEventos.Services.Dtos.Produtor;
using GerEventos.Services.Dtos.TipoEvento;
using GerEventos.Services.Dtos.TipoEventos;

public class GerEventosAutoMapperProfile : Profile
{
    public GerEventosAutoMapperProfile()
    {

        // Mapeamento para Evento
        CreateMap<Evento, EventoDto>()
            .ForMember(dest => dest.NomeTipoEvento, opt => opt.MapFrom(src => src.TipoEvento.Nome))
            .ForMember(dest => dest.NomeBalcaoVendas, opt => opt.MapFrom(src => src.BalcaoVendas.Nome))
             .ForMember(dest => dest.NomeProdutor, opt => opt.MapFrom(src => src.Produtor.Nome))
            .ForMember(dest => dest.DataInicio, opt => opt.MapFrom(src => src.DataInicio.ToString("dd/MM/yyyy")))
            .ForMember(dest => dest.DataFim, opt => opt.MapFrom(src => src.DataFim.ToString("dd/MM/yyyy")));

        CreateMap<CreateUpdateEventoDto, Evento>()
            .ForMember(dest => dest.TipoEvento, opt => opt.Ignore())
            .ForMember(dest => dest.BalcaoVendas, opt => opt.Ignore())
            .ForMember(dest => dest.Produtor, opt => opt.Ignore());

        CreateMap<EventoDto, CreateUpdateEventoDto>();

        CreateMap<FilterEventoDto, Evento>();
          


        // Mapeamento para TipoEvento
        CreateMap<TipoEvento, TipoEventoDto>();
        CreateMap<CreateUpdateTipoEventoDto, TipoEvento>();
        CreateMap<TipoEventoDto, CreateUpdateTipoEventoDto>();

        // Mapeamento para BalcaoVendas
        CreateMap<BalcaoVendas, BalcaoVendasDto>();
        CreateMap<CreateUpdateBalcaoVendasDto, BalcaoVendas>();
        CreateMap<BalcaoVendasDto, CreateUpdateBalcaoVendasDto>();

        // Mapeamento para Produtor
        CreateMap<Produtor, ProdutorDto>();
        CreateMap<CreateUpdateProdutorDto, Produtor>();
        CreateMap<ProdutorDto, CreateUpdateProdutorDto>();

    }
}
