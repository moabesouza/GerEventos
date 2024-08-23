using Volo.Abp.Application.Services;
using GerEventos.Localization;

namespace GerEventos.Services;

/* Inherit your application services from this class. */
public abstract class GerEventosAppService : ApplicationService
{
    protected GerEventosAppService()
    {
        LocalizationResource = typeof(GerEventosResource);
    }
}