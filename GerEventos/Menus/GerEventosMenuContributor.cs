using GerEventos.Permissions;
using GerEventos.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace GerEventos.Menus;

public class GerEventosMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<GerEventosResource>();
        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                GerEventosMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

        //Administration->Tenant Management
        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 2);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 6);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "GerEventos",
                l["Menu:GerEventos"],
                icon: "fa fa-calendar"
            ).AddItem(
                new ApplicationMenuItem(
                    "GerEventos.Eventos",
                    l["Eventos"],
                    url: "/Eventos"
                ).RequirePermissions(GerEventosPermissions.Evento.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "GerEventos.TipoEvento",
                    l["Tipo de evento"],
                    url: "/TipoEvento"
                ).RequirePermissions(GerEventosPermissions.TipoEvento.Default)
            ).AddItem(
                new ApplicationMenuItem(
                    "GerEventos.BalcaoVendas",
                    l["Balcão de vendas"],
                    url: "/BalcaoVendas"
                ).RequirePermissions(GerEventosPermissions.BalcaoVendas.Default)
            )
        );


        return Task.CompletedTask;
    }
}
