using GerEventos.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace GerEventos.Permissions;

public class GerEventosPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GerEventosPermissions.GroupName);


        AddPermissionWithChildren(myGroup, GerEventosPermissions.Evento.Default, "Permission:Evento", new[]
        {
            GerEventosPermissions.Evento.Create,
            GerEventosPermissions.Evento.Edit,
            GerEventosPermissions.Evento.Delete
        });

        AddPermissionWithChildren(myGroup, GerEventosPermissions.TipoEvento.Default, "Permission:TipoEvento", new[]
        {
            GerEventosPermissions.TipoEvento.Create,
            GerEventosPermissions.TipoEvento.Edit,
            GerEventosPermissions.TipoEvento.Desativar,
            GerEventosPermissions.TipoEvento.Ativar
        });

        AddPermissionWithChildren(myGroup, GerEventosPermissions.BalcaoVendas.Default, "Permission:BalcaoVendas", new[]
        {
            GerEventosPermissions.BalcaoVendas.Create,
            GerEventosPermissions.BalcaoVendas.Edit,
            GerEventosPermissions.BalcaoVendas.Desativar,
            GerEventosPermissions.BalcaoVendas.Ativar
        });

        AddPermissionWithChildren(myGroup, GerEventosPermissions.Produtor.Default, "Permission:Produtor", new[]
        {
            GerEventosPermissions.Produtor.Create,
            GerEventosPermissions.Produtor.Edit,
            GerEventosPermissions.Produtor.Desativar,
            GerEventosPermissions.Produtor.Ativar
        });

        // Define suas próprias permissões aqui, se necessário. Exemplo:
        // myGroup.AddPermission(GerEventosPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static void AddPermissionWithChildren(
        PermissionGroupDefinition group,
        string permissionName,
        string localizedName,
        string[] childPermissions)
    {
        var permission = group.AddPermission(permissionName, L(localizedName));
        foreach (var child in childPermissions)
        {
            permission.AddChild(child, L($"{localizedName}.{child.Split('.').Last()}"));
        }
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GerEventosResource>(name);
    }
}
