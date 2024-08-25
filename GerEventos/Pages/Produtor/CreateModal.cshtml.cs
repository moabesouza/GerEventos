using System.Threading.Tasks;
using GerEventos.Services.Produtores;
using GerEventos.Services.Dtos.Produtor;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using GerEventos.Services.Produtores;

namespace GerEventos.Pages.Produtor
{
    public class CreateModalModel : AbpPageModel
    {
        [BindProperty]
        public CreateUpdateProdutorDto Produtor { get; set; }

        private readonly IProdutorAppService _produtorAppService;

        public CreateModalModel(IProdutorAppService produtorAppService)
        {
            _produtorAppService = produtorAppService;
        }

        public void OnGet()
        {
            Produtor = new CreateUpdateProdutorDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (await _produtorAppService.NomeJaExisteAsync(Produtor.Nome))
            //{
            //    ModelState.AddModelError("Produtor.Nome", "O nome do produtor já existe.");
            //    return Page();
            //}

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Cria o produtor se não houver erros
            await _produtorAppService.CreateAsync(Produtor);
            return NoContent();
        }
    }
}
