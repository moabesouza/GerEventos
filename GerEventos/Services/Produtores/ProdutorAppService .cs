using System;
using System.Threading.Tasks;
using GerEventos.Permissions;
using GerEventos.Entities;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using GerEventos.Services.Dtos.Produtor;

namespace GerEventos.Services.Produtores
{
    public class ProdutorAppService :
        CrudAppService<
            Produtor, // A entidade Produtor
            ProdutorDto, // Usado para exibir produtores
            Guid, // Chave primária da entidade produtor
            PagedAndSortedResultRequestDto, // Usado para paginação/ordenamento
            CreateUpdateProdutorDto>, // Usado para criar/atualizar um produtor
        IProdutorAppService // Implementa a IProdutorAppService
    {
        public ProdutorAppService(IRepository<Produtor, Guid> repository)
            : base(repository)
        {
            GetPolicyName = GerEventosPermissions.Produtor.Default;
            GetListPolicyName = GerEventosPermissions.Produtor.Default;
            CreatePolicyName = GerEventosPermissions.Produtor.Create;
            UpdatePolicyName = GerEventosPermissions.Produtor.Edit;
        }

        public async Task DeactivateAsync(Guid id)
        {
            var produtor = await Repository.GetAsync(id);
            produtor.Status = StatusEnum.Desativado;
            await Repository.UpdateAsync(produtor);
        }

        public async Task ActivateAsync(Guid id)
        {
            var produtor = await Repository.GetAsync(id);
            produtor.Status = StatusEnum.Ativado;
            await Repository.UpdateAsync(produtor);
        }

        public async Task<bool> NomeJaExisteAsync(string nome)
        {
            return await Repository.AnyAsync(p => p.Nome == nome);
        }
    }
}
