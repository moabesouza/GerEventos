using GerEventos.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace GerEventos.Permissions;

public class GerEventosPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(GerEventosPermissions.GroupName);


        // Permiss�es para Eventos
        var eventosPermission = myGroup.AddPermission(GerEventosPermissions.Evento.Default, L("Permission:Evento"));
        eventosPermission.AddChild(GerEventosPermissions.Evento.Create, L("Permission:Evento.Create"));
        eventosPermission.AddChild(GerEventosPermissions.Evento.Edit, L("Permission:Evento.Edit"));
        eventosPermission.AddChild(GerEventosPermissions.Evento.Delete, L("Permission:Evento.Delete"));

        // Permiss�es para TipoEventos
        var tipoEventosPermission = myGroup.AddPermission(GerEventosPermissions.TipoEvento.Default, L("Permission:TipoEvento"));
        tipoEventosPermission.AddChild(GerEventosPermissions.TipoEvento.Create, L("Permission:TipoEvento.Create"));
        tipoEventosPermission.AddChild(GerEventosPermissions.TipoEvento.Edit, L("Permission:TipoEvento.Edit"));
        tipoEventosPermission.AddChild(GerEventosPermissions.TipoEvento.Desativar, L("Permission:TipoEvento.Desativar"));
        tipoEventosPermission.AddChild(GerEventosPermissions.TipoEvento.Ativar, L("Permission:TipoEvento.Ativar"));

        // Permiss�es para BalcaoVendas
        var balcaoVendasPermission = myGroup.AddPermission(GerEventosPermissions.BalcaoVendas.Default, L("Permission:BalcaoVendas"));
        balcaoVendasPermission.AddChild(GerEventosPermissions.BalcaoVendas.Create, L("Permission:BalcaoVendas.Create"));
        balcaoVendasPermission.AddChild(GerEventosPermissions.BalcaoVendas.Edit, L("Permission:BalcaoVendas.Edit"));
        balcaoVendasPermission.AddChild(GerEventosPermissions.BalcaoVendas.Delete, L("Permission:BalcaoVendas.Delete"));
        balcaoVendasPermission.AddChild(GerEventosPermissions.TipoEvento.Desativar, L("Permission:BalcaoVendas.Desativar"));
        balcaoVendasPermission.AddChild(GerEventosPermissions.TipoEvento.Ativar, L("Permission:BalcaoVendas.Ativar"));


        // Define suas pr�prias permiss�es aqui, se necess�rio. Exemplo:
        // myGroup.AddPermission(GerEventosPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GerEventosResource>(name);
    }
}
