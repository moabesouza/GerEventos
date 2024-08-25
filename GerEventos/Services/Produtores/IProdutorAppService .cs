using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using GerEventos.Entities;
using GerEventos.Services.Dtos.Produtor;

namespace GerEventos.Services.Produtores
{
    public interface IProdutorAppService :
        ICrudAppService< // Define os métodos CRUD
            ProdutorDto, // Usado para exibir os produtores
            Guid, // Chave primária da entidade Produtor
            PagedAndSortedResultRequestDto, // Usado para paginação/ordenamento
            CreateUpdateProdutorDto> // Usado para criar/atualizar um Produtor
    {
        Task DeactivateAsync(Guid id);
        Task ActivateAsync(Guid id);
        Task<bool> NomeJaExisteAsync(string nome);
    }
}
