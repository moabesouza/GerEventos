using GerEventos.Services.Produtores;
using GerEventos.Services.Dtos.Produtor;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace GerEventos.Pages.Produtor
{
    public class EditModalModel : AbpPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProdutorDto Produtor { get; set; }

        private readonly IProdutorAppService _produtorAppService;

        public EditModalModel(IProdutorAppService produtorAppService)
        {
            _produtorAppService = produtorAppService;
        }

        public async Task OnGetAsync()
        {
            var produtorDto = await _produtorAppService.GetAsync(Id);
            Produtor = ObjectMapper.Map<ProdutorDto, CreateUpdateProdutorDto>(produtorDto);
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



            await _produtorAppService.UpdateAsync(Id, Produtor);
            return NoContent();
        }
    }
}
